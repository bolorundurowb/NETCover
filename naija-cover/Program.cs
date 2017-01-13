using System;
using naija_cover.Runtime;

namespace naija_cover
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            //
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => CoverageTracker.WriteCoverageToFile();
        }
    }
}
