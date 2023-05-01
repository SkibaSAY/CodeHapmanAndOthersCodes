using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes
{
    public static class RLECode
    {
        public static string Coding(string inputStr)
        {
            var sb = new StringBuilder();
            var currentCh = inputStr.First();
            var currentCount = 0;
            foreach (var ch in inputStr)
            {
                if(ch.CompareTo(currentCh) == 0)
                {
                    currentCount++;
                }
                else
                {
                    sb.Append($"{currentCh}{currentCount}");
                    currentCount = 1;
                    currentCh = ch;
                }
            }
            sb.Append($"{currentCh}{currentCount}");
            return sb.ToString();
        }

        private static Regex rleRegex = new Regex(@"(?<char>\D+?)(?<count>\d+?)");
        public static string Decoding(string inputStr)
        {
            var sb = new StringBuilder();
            var matches = rleRegex.Matches(inputStr);
            foreach(Match m in matches)
            {
                var ch = m.Groups["char"].Value;
                var count = int.Parse(m.Groups["count"].Value);
                var str = String.Join("",Enumerable.Repeat(ch, count));
                sb.Append(str);
            }
            return sb.ToString();
        }
    }
}
