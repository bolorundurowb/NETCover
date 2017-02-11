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

        private SyntaxNode MakeCoverageTrackingCall(SyntaxNode node)
        {
            return MakeCoverageTrackingCall(node, true);
        }

        private SyntaxNode MakeCoverageTrackingCall(SyntaxNode node, bool passInNode)
        {
            var lineNumber = node.GetLocation().GetLineSpan().StartLinePosition.Line;
            CoverageTracker.MarkExecutable(fileName, lineNumber);

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
                //var nodeArg = Argument();
                argumentList = SeparatedList(new[] {filePathArg, lineNumberArg/*, nodeArg*/});
            }
            else
            {
                argumentList = SeparatedList(new[] {filePathArg, lineNumberArg});
            }
            var methodCallExpr = ExpressionStatement(
                InvocationExpression(method, ArgumentList(argumentList)));
            return methodCallExpr;
        }

        public override SyntaxNode VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitThisExpression(ThisExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitBaseExpression(BaseExpressionSyntax node)
        {
            return MakeCoverageTrackingCall(node);
        }

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if (InsideStatement(node))
            {
                return MakeCoverageTrackingCall(node);
            }
            return node;
        }

        private bool InsideStatement(SyntaxNode node)
        {
            while (node != null && !(node.GetType() == typeof(StatementSyntax)))
            {
                node = node.Parent;
            }
            return node.GetType() == typeof(StatementSyntax);
        }
    }
}
