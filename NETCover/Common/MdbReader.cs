using System.Collections.Generic;
using Mono.Cecil;

namespace NETCover.Common
{
    public class MdbReader : IProgramDatabaseReader
    {
        public void Initialize(string assemblyFilePath)
        {
            throw new System.NotImplementedException();
        }

        public IDictionary<uint, CodeSegment> GetSegmentsByMethod(MethodDefinition methodDef)
        {
            throw new System.NotImplementedException();
        }
    }
}