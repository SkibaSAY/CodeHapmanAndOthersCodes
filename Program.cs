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
            string fileInput = "input.txt";
            string fileOut = "output.txt";
            string fileResources = "resources.txt";
            var code =  new LZ_77_Code();

            code.Code(fileInput, fileOut, fileResources);
        }
    }
}
