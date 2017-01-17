using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using naija_cover.Runtime;

namespace naija_cover
{
    public class CoverageVisitor : CSharpSyntaxRewriter
    {
        private string fileName;

        public CoverageVisitor(string filename)
        {
            fileName = filename;
        }

        private void MakeCoverageTrackingCall(TypeDeclarationSyntax node)
        {
            MakeCoverageTrackingCall(node, true);
        }

        private void MakeCoverageTrackingCall(TypeDeclarationSyntax node, bool passInNode)
        {
            var lineNumber = node.GetLocation().GetLineSpan().StartLinePosition.Line;
            CoverageTracker.MarkExecutable(fileName, lineNumber);
        }
    }
}
