using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using naija_cover.Runtime;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace naija_cover
{
    public class CoverageVisitor : CSharpSyntaxRewriter
    {
        private string fileName;

        public CoverageVisitor(string filename)
        {
            fileName = filename;
        }

        private SyntaxNode MakeCoverageTrackingCall(TypeDeclarationSyntax node)
        {
            return MakeCoverageTrackingCall(node, true);
        }

        private SyntaxNode MakeCoverageTrackingCall(TypeDeclarationSyntax node, bool passInNode)
        {
            var lineNumber = node.GetLocation().GetLineSpan().StartLinePosition.Line;
            CoverageTracker.MarkExecutable(fileName, lineNumber);
            //
            var method = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("naija_cover"), IdentifierName("Runtime")),
                    IdentifierName("CoverageTracker")),
                IdentifierName("MarkExecuted"));
            var filePathArg = Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(fileName)));
            var lineNumberArg = Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(lineNumber)));
            SeparatedSyntaxList<ArgumentSyntax> argumentList;
            if (passInNode)
            {
//                var nodeArg = Argument();
                argumentList = SeparatedList(new[] {filePathArg, lineNumberArg, /*nodeArg*/});
            }
            else
            {
                argumentList = SeparatedList(new[] {filePathArg, lineNumberArg});
            }
            var methodCallExpr = ExpressionStatement(
                InvocationExpression(method, ArgumentList(argumentList)));
            return methodCallExpr;
        }
    }
}
