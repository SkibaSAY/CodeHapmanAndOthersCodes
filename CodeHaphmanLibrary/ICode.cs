using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary
{
    public interface ICode
    {
        void Code(string inputFilePath, string outputFilePath, string resoursesPath);
        decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath);
        void Decode(string inputFilePath, string outputFilePath, string resoursesPath);
    }
}
