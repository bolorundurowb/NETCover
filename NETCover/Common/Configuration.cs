//
// Configuration.cs
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

using System.Collections.Generic;
namespace NETCover.Common
{
	public static class Configuration
	{
		public enum NamingModes
		{
			/// <summary>
			/// All instrumented assembly files will have ".Instrumented" suffix
			/// </summary>
			MarkInstrumented,
			/// <summary>
			/// Instrumented assemblies will replace original ones.
			/// Original dll, exe, pdb 's will be backed up
			/// </summary>
			BackupOriginals
		}

		public static void Initialize()
		{
			CoverageFile = "coverage.xml";
			NamingMode = NamingModes.MarkInstrumented;
			NameFilters = new List<NameFilter>();
		}

		/// <summary>
		/// Generated XML coverage file path
		/// </summary>
		public static string CoverageFile{ get; set; }
		
		/// <summary>
		/// Specifies file naming mode for instrumented assemblies
		/// </summary>
		public static NamingModes NamingMode { get; set; }

		/// <summary>
		/// List of member name filters
		/// </summary>
		public static IList<NameFilter> NameFilters { get; set; }
	}
}
