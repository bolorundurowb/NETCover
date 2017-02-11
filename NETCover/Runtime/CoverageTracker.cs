using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NETCover.Runtime
{
    public class CoverageTracker
    {
        private static Dictionary<string, Dictionary<int, bool>> coverage =
            new Dictionary<string, Dictionary<int, bool>>();

        public static void WriteCoverageToFile()
        {
            string lcovcoverage = GenerateLcov();
            string coverageReportPath = Environment.GetEnvironmentVariable("coverage.report.path") ??
                                        "coverage_report.lcov";
            try
            {
                File.WriteAllText(coverageReportPath, lcovcoverage);
            }
            catch (IOException ex)
            {
                throw new Exception("Error while writing coverage", ex);
            }
        }

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

        public static void MarkExecutable(string fileName, int line)
        {
            if (!coverage.ContainsKey(fileName))
            {
                coverage.Add(fileName, new Dictionary<int, bool>());
            }
            coverage[fileName].Add(line, false);
        }

        public static void MarkExecuted(string fileName, int line)
        {
            if (!coverage.ContainsKey(fileName))
            {
                coverage.Add(fileName, new Dictionary<int, bool>());
            }
            coverage[fileName].Add(line, true);
        }

        public static byte MarkExecuted(string fileName, int line, byte expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static char MarkExecuted(string fileName, int line, char expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static int MarkExecuted(string fileName, int line, int expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static long MarkExecuted(string fileName, int line, long expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static bool MarkExecuted(string fileName, int line, bool expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static float MarkExecuted(string fileName, int line, float expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static double MarkExecuted(string fileName, int line, double expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }

        public static T MarkExecuted<T>(string fileName, int line, T expression)
        {
            MarkExecuted(fileName, line);
            return expression;
        }
    }
}
