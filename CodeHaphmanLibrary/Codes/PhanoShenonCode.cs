using CodeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class PhanoShenonCode : ICode
    {
        public void Code(string inputText, out string outputText, out string resourses)
        {
            throw new NotImplementedException();
        }

        public string Coding(string inputText)
        {
            throw new NotImplementedException();
        }

        public double CompressionRate(string inputText, string outputText, string resourses)
        {
            var beforeCodingSize = inputText.Length;
            var afterCodingSize = outputText.Length + resourses.Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            throw new NotImplementedException();
        }

        public string Decoding(string outputText, string resourses)
        {
            throw new NotImplementedException();
        }
    }
}
