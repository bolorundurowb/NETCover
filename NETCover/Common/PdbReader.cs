//
// PdbReader.cs
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
using System.Linq;
using Microsoft.Cci.Pdb;
using Mono.Cecil;
using System.IO;

namespace NETCover.Common
{
	/// <summary>
	/// Reads Microsoft pdb files using internal classes of Pdb2Mdb converter tool
	/// </summary>
	public class PdbReader : IProgramDatabaseReader
	{
		private Dictionary<uint, PdbFunction> _pdbFunctions;

		public PdbReader(string assemblyFilePath)
		{
			Initialize(assemblyFilePath);
		}

		public void Initialize(string assemblyFilePath)
		{
			using (var pdbFileStream = File.OpenRead(Path.ChangeExtension(assemblyFilePath, "pdb")))
			{
				_pdbFunctions = PdbFile.LoadFunctions(pdbFileStream, true).ToDictionary(func => func.token, func => func);
			}
		}

		/// <summary>
		/// Retrieves source code segment locations with corresponding offsets in compiled assembly
		/// </summary>
		/// <param name="methodDef">Method definition</param>
		/// <returns>Dictionary: Key is an instruction offset, Value - source code segment location</returns>
		public IDictionary<uint, CodeSegment> GetSegmentsByMethod(MethodDefinition methodDef)
		{
			var res = new Dictionary<uint, CodeSegment>();
			var methToken = methodDef.MetadataToken.ToUInt();
			if (!_pdbFunctions.ContainsKey(methToken))
				return res;

			var pdbFunction = _pdbFunctions[methToken];
			if(pdbFunction.lines == null)
				return res;

			for (var i = 0; i < pdbFunction.lines.Length; i++)
			{
				for (var j = 0; j < pdbFunction.lines[i].lines.Length; j++)
				{
					var offset = pdbFunction.lines[i].lines[j].offset;
					var startColumn = pdbFunction.lines[i].lines[j].colBegin;
					var endColumn = pdbFunction.lines[i].lines[j].colEnd;
					var startRow = pdbFunction.lines[i].lines[j].lineBegin;
					var endRow = pdbFunction.lines[i].lines[j].lineEnd;
					var codeFile = pdbFunction.lines[i].file.name;

					res.Add(offset, new CodeSegment(startColumn, endColumn, startRow, endRow, codeFile));
				}
			}

			return res;
		}
	}
}
