using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class Lz77Mark
    {
        public int OffSet { get; set; }
        public int OfSetLength { get; set; }
        public char Next { get; set; }

        public override string ToString()
        {
            return $"({OffSet}, {OfSetLength}, {Next})";
        }
    }
}
