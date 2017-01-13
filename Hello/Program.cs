using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Hello
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("namespace hello\n");
            stringBuilder.Append("{");
            stringBuilder.Append("    public class Hello");
            stringBuilder.Append("    {");
            stringBuilder.Append("        public static void Main(string[] args)");
            stringBuilder.Append("        {");
            stringBuilder.Append("            Console.WriteLine(\"Hello World\");");
            stringBuilder.Append("        }");
            stringBuilder.Append("    }");
            stringBuilder.Append("}");

            var code = stringBuilder.ToString();
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var root = (CompilationUnitSyntax) tree.GetRoot();

            //create a new line
            var console = SyntaxFactory.IdentifierName("Console");
            var writeline = SyntaxFactory.IdentifierName("WriteLine");
            var memberaccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, console, writeline);

            var argument = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal("hello, csharpparser")));
            var argumentList = SyntaxFactory.SeparatedList(new[] { argument });

            var writeLineCall =
                SyntaxFactory.ExpressionStatement(
                    SyntaxFactory.InvocationExpression(memberaccess,
                        SyntaxFactory.ArgumentList(argumentList)));

            //add to main method
//            root.Members.Add(writeLineCall);

            var walker = new CustomWalker();
            walker.Visit(root);
        }
    }
}
