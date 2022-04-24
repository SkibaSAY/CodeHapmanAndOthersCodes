﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaphmanCodeLibrary;

namespace HapmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "input.txt";
            var a = HaphmanCode.BuildCode(file);
            var text = File.ReadAllText(file);
            var codingText = HaphmanCode.Coding(text, a);
            Console.WriteLine(codingText);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(HaphmanCode.Decoding(codingText, a));
        }
    }
}
