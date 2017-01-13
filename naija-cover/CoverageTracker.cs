using System.Collections.Generic;

namespace naija_cover
{
    public class CoverageTracker
    {
        private static Dictionary<string, Dictionary<int, bool>> coverage = new Dictionary<string, Dictionary<int, bool>>();

        public static void WriteCoverageToFile() {}

        public static void MarkExecuted(string filName, int line)
        {
            if (!coverage.ContainsKey(filName))
            {
                coverage.Add(filName, new Dictionary<int, bool>());
            }
            coverage[filName].Add(line, true);
        }
    }
}
