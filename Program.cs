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
            //HaphmanCodeTest();
            //Lz77CodeTest();
            PreobrazBArrouzeYileraCode();
        }
        static void PreobrazBArrouzeYileraCode()
        {
            //var str = "abcabcabca";
            var str = "abcabcabcaasdasdfgarqdfsacsdfawsewasxdasdasdad";
            var bwt = new BWTCode();
            var result = bwt.Coding(str);
        }
        static void HaphmanCodeTest()
        {
            string file = "input1.txt";
            var text = File.ReadAllText(file);
            var a = HaphmanCode.BuildCode(text);
            var codingText = HaphmanCode.Coding(text, a);
            //Console.WriteLine(codingText);
            //Console.WriteLine("-----------------------------------");
            var decodingText = HaphmanCode.Decoding(codingText, a);
        }
        static void Lz77CodeTest()
        {
            var fileInput = "input.txt";
            var input = File.ReadAllText(fileInput);
            var code =  new LZ_77_Code(10000);

            code.Code(input, out string output,out string resources);

            var k = code.CompressionRate(input, null, resources);

            code.Decode(null,out string outPut, resources);
        }
    }
}
