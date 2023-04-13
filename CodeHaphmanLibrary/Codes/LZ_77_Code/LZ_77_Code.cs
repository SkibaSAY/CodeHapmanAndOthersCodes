using CodeHaphmanLibrary.Codes.LZ_77_Code;
using CodeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class LZ_77_Code : ICode
    {
        public readonly int _searchBufferSize;
        public LZ_77_Code(int searchBufferSize = 1024)
        {
            this._searchBufferSize = searchBufferSize;
        }

        public void Code(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var text = File.ReadAllText(inputFilePath);

            LzBuffer searchBuffer = new LzBuffer(size: _searchBufferSize);
            LzBuffer sourceBuffer = new LzBuffer(text, text.Length);
            List<Lz77Mark> marks = new List<Lz77Mark>();        

            while(sourceBuffer.Count > 0)
            {
                var offSet = 0;
                var offSetLength = 0;
                char nextCh='$';

                var set = new List<char>();

                for (; offSetLength < sourceBuffer.Count; offSetLength++)
                {
                    nextCh = sourceBuffer.Next();
                    set.Add(nextCh);

                    var index = searchBuffer.FindFirstIndex(set);
                    if (index == -1) break;
                    offSet = index;
                }

                marks.Add(new Lz77Mark {
                    OffSet = offSet,
                    OfSetLength = offSetLength,
                    Next = nextCh
                });
            }

            var resourceAsJson = JsonConvert.SerializeObject(marks);
            File.WriteAllText(resoursesPath, resourceAsJson);
        }

        public decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            throw new NotImplementedException();
        }

        public void Decode(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            throw new NotImplementedException();
        }
    }
}
