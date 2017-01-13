using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Hello
{
    public class CustomWalker : CSharpSyntaxWalker
    {
//        private static int Tabs = 0;
//
//        public override void Visit(SyntaxNode node)
//        {
//            Tabs++;
//            var indents = new string('\t', Tabs);
//            Console.WriteLine(indents + node.Kind());
//            base.Visit(node);
//            Tabs--;
//        }

        public override void Visit(SyntaxNode node)
        {
            if (node.Kind() == SyntaxKind.InvocationExpression)
            {
                Console.WriteLine("found method call: {0}", node.ToFullString());
            }
            base.Visit(node);
        }
    }
}
