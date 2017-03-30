//
// Runner.cs
//
// Author:
//   Sergiy Sakharov (sakharov@gmail.com)
//
// (c) 2017 NET Cover Contributors
// (C) 2009 Sergiy Sakharov
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using NETCover.Common;
using NETCover.Instrument;
using NETCover.Report;

namespace NETCover
{
	static class Runner
	{
		public static void Main(string[] args)
		{
			Configuration.Initialize();

			var assemblies = new List<string>();
			var filters = new List<NameFilter>();
			var optionsOnly = false;

			for (var i = 0; i < args.Length; i++)
			{
			    switch (args[i])
			    {
			        case "/x": //next argument is a coverage file name
			            optionsOnly = true;
			            if (i < args.Length - 1)
			                Configuration.CoverageFile = args[i + 1];
			            break;
			        case "/r":
			            optionsOnly = true;
			            Configuration.NamingMode = Configuration.NamingModes.BackupOriginals;
			            break;
			        case "/h":
			            ShowHelp();
			            return;
			        default:
			            if (optionsOnly)
			                break;

			            if (args[i][0] != '-')
			                assemblies.AddRange(ResolveFilesByMask(args[i]));
			            else
			                Configuration.NameFilters.Add(ParseFilter(args[i].Substring(1)));

			            break;
			    }
			}

			var executor = new Executor(assemblies.ToArray());
			executor.Execute(new CompositeVisitor(new ReportVisitor(), new InstrumentorVisitor()));
		}

		private static NameFilter ParseFilter(string filterStr)
		{
			//default type of filter is Attribute filter
			if (filterStr[1] != ':')
				return new NameFilter { Type = NameFilter.FilterTypes.AttributeFilter, FilteredName = filterStr };

		    var filter = new NameFilter
		    {
		        Type = (NameFilter.FilterTypes) char.ToLower(filterStr[0]),
		        FilteredName = filterStr.Substring(2)
		    };

		    return filter;
		}

		private static IEnumerable<string> ResolveFilesByMask(string filePathMask)
		{
			var files = Directory.GetFiles(Path.GetDirectoryName(filePathMask), Path.GetFileName(filePathMask));

			return files.Where(
					file =>
					(Path.GetExtension(file) == ".dll" || Path.GetExtension(file) == ".exe") && File.Exists(file)
				);
		}

		private static void ShowHelp()
		{
			Console.WriteLine(
            @"Usage: coverage.exe {<assemblyPaths>} [{-[<ExclusionType>:]NameFilter}] [<commands> [<commandArgs>]]

            Exclusion Types:
                f:  filter files by name

                s:  filter assemblies by name
                       This could be useful if strong name for some
                       assembly should be weakened, however, coverage
                       report is redundant for it

                t:  filter types by full name

                m:  filter methods by full name

                default (a:)  filter members by their custom attribute names

            Commands:
                /r  If this command is selected - instrumented assemblies replace existing ones.
                    Old assemblies are backed up along with corresponding pdb files

                /x  Path to a coverage file

            Example command line to launch instrumenting:
                NETCover myapp.exe myapp.lib.dll -CodeGeneratedAttribute -t:Test /r /x coverage2.xml

                This will generate instrumented myapp.exe and myapp.lib.dll, moving old assemblies into
                myapp.bak.exe and myapp.lib.bak.dll respectively. Members marked by attributes that contain
                'CodeGeneratedAttribute' in their name as well as types that contain 'Test' in their full names
                will be excluded from report");
		}
	}
}
