using System.Collections.Generic;

namespace naija_cover.Runtime
{
    public class CoverageTracker
    {
        private static Dictionary<string, Dictionary<int, bool>> coverage = new Dictionary<string, Dictionary<int, bool>>();

        public static void WriteCoverageToFile() {}

        public static void MarkExecuted(string fileName, int line)
        {
            if (!coverage.ContainsKey(fileName))
            {
                coverage.Add(fileName, new Dictionary<int, bool>());
            }
            coverage[fileName].Add(line, true);
        }
    }
}
