using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using naija_cover.Runtime;

namespace naija_cover
{
    internal class Program
    {
        private static SyntaxTree Parse(string fileName)
        {
            StreamReader streamReader = new StreamReader(fileName);
            string codeText = streamReader.ReadToEnd();
            return CSharpSyntaxTree.ParseText(codeText);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Naija Cover 1.0.0");
            Console.WriteLine("by Bolorunduro Winner-Timothy .B (c)2017");
            //
            List<string> arguments = args.ToList();
            int directoryFlag = arguments.IndexOf("-d");
            if (directoryFlag == -1 || arguments.Count <= directoryFlag - 1)
            {
                Console.WriteLine("Usage: naija-cover.exe -d output-dir <csharp code files seperated by space>\n");
                return;
            }

            string outputDirectory = arguments[directoryFlag + 1];
            foreach (string argument in arguments)
            {
                if (!argument.EndsWith(".cs"))
                {
                    continue;
                }
                SyntaxTree syntaxTree = Parse(argument);
                CoverageVisitor coverageVisitor = new CoverageVisitor(argument);
                coverageVisitor.Visit(syntaxTree.GetRoot());
                //
                string fileNameWithoutPath = Path.GetFileName(argument);
                string fileOutputPath = Path.Combine(outputDirectory, fileNameWithoutPath);
                File.WriteAllText(fileOutputPath, syntaxTree.ToString());
            }
            // Make sure coverage is written before closing
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => CoverageTracker.WriteCoverageToFile();
        }
    }
}
