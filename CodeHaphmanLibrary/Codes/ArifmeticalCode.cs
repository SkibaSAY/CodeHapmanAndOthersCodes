using CodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodesLibrary
{
    public class ArifmeticalCode : ICode
    {
        public SortedList<char, uint> alphabetOfProbabilities = new SortedList<char, uint>();

        public void Code(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputText = File.ReadAllText(inputFilePath);
        }

        public string Coding(string inputText)
        {
            //тут пока наивный алгоритм. Без оптимизации под длинный текст
            //заполняем алфавит
            foreach (var symbol in inputText)
            {
                if (alphabetOfProbabilities.ContainsKey(symbol))
                {
                    alphabetOfProbabilities[symbol]++;
                }
                else
                {
                    alphabetOfProbabilities.Add(symbol, 1);
                }
            }
            var countOfLetters = (ulong)inputText.Length;

            double currentCodeStart = 0;
            double currentCodeEnd = countOfLetters;
            string stringCodeStart = currentCodeStart.ToString();
            string stringCodeEnd = currentCodeEnd.ToString();
            var res = "";

            foreach (var symbol in inputText)
            {
                var curIndexInAlph = alphabetOfProbabilities.IndexOfKey(symbol);
                var lengthOfInterval = currentCodeEnd - currentCodeStart;
                currentCodeStart = 0;

                foreach (var letter in alphabetOfProbabilities)
                {
                    if (letter.Key == symbol) break;
                    currentCodeStart += letter.Value;
                }

                currentCodeStart *= lengthOfInterval / countOfLetters;
                currentCodeEnd = currentCodeStart + alphabetOfProbabilities[symbol] * lengthOfInterval / countOfLetters;

                stringCodeStart = currentCodeStart.ToString();
                stringCodeEnd = currentCodeEnd.ToString();
                int i = 0;
                while(stringCodeStart[i] == stringCodeEnd[i])
                {
                    res += stringCodeStart[i];
                    stringCodeStart.Remove(0, 1);
                    stringCodeEnd.Remove(0, 1);
                }
                currentCodeStart = int.Parse(stringCodeStart);
                currentCodeEnd = int.Parse(stringCodeEnd);
            }

            return res;

        }
        public decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var beforeCodingSize = new FileInfo(inputFilePath).Length;
            var afterCodingSize = new FileInfo(outputFilePath).Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            throw new NotImplementedException();
        }
    }
}
