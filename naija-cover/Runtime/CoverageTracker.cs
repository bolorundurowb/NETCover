using System.Collections.Generic;
using System.Text;

namespace naija_cover.Runtime
{
    public class CoverageTracker
    {
        private static Dictionary<string, Dictionary<int, bool>> coverage = new Dictionary<string, Dictionary<int, bool>>();

        public static void WriteCoverageToFile() {}

        public static string GenerateLcov()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string fileName in coverage.Keys)
            {
                sb.Append("SF:" + fileName + "\n");
                foreach (KeyValuePair<int,bool> line in coverage[fileName])
                {
                    sb.Append(string.Format("DA:{0},{1}\n", line.Key, line.Value ? 1 : 0));
                }
                sb.Append("end_of_record\n");
            }
            return sb.ToString();
        }

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
