using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary
{
    public interface ICode
    {
        void Code(string inputText, out string outputText, out string resourses);
        double CompressionRate(string inputText, string outputText, string resourses);
        void Decode(string inputText, out string outputText, string resourses);
    }
}
