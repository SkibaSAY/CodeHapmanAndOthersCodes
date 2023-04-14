using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodesLibrary;

namespace HapmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Lz77CodeTest();
        }
        static void HaphmanCodeTest()
        {
            string file = "input.txt";
            var a = HaphmanCode.BuildCode(file);
            var text = File.ReadAllText(file);
            var codingText = HaphmanCode.Coding(text, a);
            Console.WriteLine(codingText);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(HaphmanCode.Decoding(codingText, a));
        }
        static void Lz77CodeTest()
        {
            var fileInput = "input.txt";
            var code =  new LZ_77_Code();

            var input = File.ReadAllText(fileInput);

            code.Code(input, out string output,out string resources);

            var k = code.CompressionRate(input, null, resources);
        }
    }
}
