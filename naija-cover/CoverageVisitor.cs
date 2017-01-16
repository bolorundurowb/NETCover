using Microsoft.CodeAnalysis.CSharp;

namespace naija_cover
{
    public class CoverageVisitor : CSharpSyntaxWalker
    {
        private string fileName;

        public CoverageVisitor(string filename)
        {
            fileName = filename;
        }
    }
}
