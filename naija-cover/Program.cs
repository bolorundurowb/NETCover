using System;
using System.Collections.Generic;
using System.Linq;
using naija_cover.Runtime;

namespace naija_cover
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Naija Cover 1.0.0");
            //
            List<string> arguments = args.ToList();
            int directoryFlag = arguments.IndexOf("-d");

            // Make sure coverage is written before closing
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => CoverageTracker.WriteCoverageToFile();
        }
    }
}
