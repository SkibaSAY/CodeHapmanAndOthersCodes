using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes.BWT
{
    public class ListComparer<T> : Comparer<T>
        where T : List<char>
    {
        public override int Compare(T x, T y)
        {
            var xStr = String.Join("", x.Select(t=>t.ToString()).ToArray());
            var yStr = String.Join("", y.Select(t => t.ToString()).ToArray());
            return xStr.CompareTo(yStr);
        }
    }
}
