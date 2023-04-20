using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    sb.Append($"{currentCount}{currentCh}");
                    currentCount = 1;
                    currentCh = ch;
                }
            }
            sb.Append($"{currentCount}{currentCh}");
            return sb.ToString();
        }
    }
}
