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
        public LZ_77_Code(int searchBufferSize = 256)
        {
            this._searchBufferSize = searchBufferSize;
        }

        public void Code(string inputText, out string outputText, out string resourses)
        {
            LzBuffer searchBuffer = new LzBuffer(size: _searchBufferSize);
            List<char> sourceBuffer = inputText.Select(ch=>ch).ToList();
            List<Lz77Mark> marks = new List<Lz77Mark>();

            var sourceIterator = 0;
            while (sourceBuffer.Count > sourceIterator)
            {
                var offSet = 0;
                var offSetLength = 0;
                char nextCh = '$';

                var set = new List<char>();

                for (; sourceBuffer.Count > sourceIterator; offSetLength++)
                {
                    nextCh = sourceBuffer[sourceIterator];
                    sourceIterator++;

                    set.Add(nextCh);

                    var index = searchBuffer.FindFirstIndex(set);
                    if (index == -1) break;

                    offSet = searchBuffer.Count-index-1;
                }

                marks.Add(new Lz77Mark
                {
                    OffSet = offSet,
                    OfSetLength = offSetLength,
                    Next = nextCh
                });
                searchBuffer.Append(set.ToArray());
            }

            resourses = JsonConvert.SerializeObject(marks);
            outputText = null;
        }

        public double CompressionRate(string inputText, string outputText, string resourses)
        {
            var marks = JsonConvert.DeserializeObject<List<Lz77Mark>>(resourses);
            var compressionRate = inputText.Length * 8.0 / (marks.Count * 8 * 3);
            return compressionRate;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            throw new NotImplementedException();
        }
    }
}
