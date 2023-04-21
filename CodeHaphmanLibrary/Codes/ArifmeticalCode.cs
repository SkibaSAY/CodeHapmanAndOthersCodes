using CodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CodesLibrary
{
    public class ArifmeticalCode
    {
        public SortedList<char, uint> alphabetOfProbabilities = new SortedList<char, uint>();

        public void Code(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputText = File.ReadAllText(inputFilePath);
            var result = Coding(inputText);
            File.WriteAllText(outputFilePath, result);

            var jsonResourse = JsonConvert.SerializeObject(alphabetOfProbabilities);
            File.WriteAllText(resoursesPath, jsonResourse);
        }

        public string Coding(string inputText)
        {
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
                var startOfInterval = currentCodeStart;
                currentCodeStart = 0;
                foreach (var letter in alphabetOfProbabilities)
                {
                    if (letter.Key == symbol) break;
                    currentCodeStart += letter.Value;
                }

                currentCodeStart *= lengthOfInterval / countOfLetters;
                currentCodeStart += startOfInterval;
                currentCodeEnd = currentCodeStart + alphabetOfProbabilities[symbol] * lengthOfInterval / countOfLetters;

                stringCodeStart = currentCodeStart.ToString();
                stringCodeEnd = currentCodeEnd.ToString();

                while(stringCodeStart.Length > stringCodeEnd.Length)
                {
                    stringCodeEnd += "0";
                }

                while(stringCodeStart.Length > 0 && stringCodeEnd.Length > 0 && stringCodeStart[0] == stringCodeEnd[0] &&
                    stringCodeStart[1]!='0' && stringCodeEnd[1] != '0')
                {
                    if (stringCodeStart[0] != ',')
                    {
                        res += stringCodeStart[0];
                    }
                    else if(!res.Contains("."))
                    {
                        res += ",";
                    }
                    stringCodeStart = stringCodeStart.Remove(0, 1);
                    stringCodeEnd = stringCodeEnd.Remove(0, 1);
                }
                currentCodeStart = double.Parse(stringCodeStart);
                currentCodeEnd = double.Parse(stringCodeEnd);
            }

            return res + stringCodeEnd[0];

        }
        public decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var beforeCodingSize = new FileInfo(inputFilePath).Length;
            var afterCodingSize = new FileInfo(outputFilePath).Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputNumber = File.ReadAllText(inputFilePath);

            var jsonResourse = File.ReadAllText(resoursesPath);

            alphabetOfProbabilities = JsonConvert.DeserializeObject<SortedList<char, uint>>(jsonResourse);

            var decodedText = Decoding(inputNumber);
            File.WriteAllText(outputFilePath, decodedText);
        }

        public string Decoding(string inputNumber)
        {
            ulong countOfLetters = 0;
            var codeNumber = new StringBuilder(inputNumber);

            //заполняем алфавит
            foreach (var symbol in alphabetOfProbabilities)
            {
                countOfLetters += symbol.Value;
            }

            double currentCodeStart = 0;
            double currentCodeEnd = countOfLetters;
            string stringCodeStart = currentCodeStart.ToString();
            string stringCodeEnd = currentCodeEnd.ToString();
            var res = "";

            for (uint i = 0; i < countOfLetters;i++)
            {
                var curNumber = double.Parse(codeNumber.ToString());
                var lengthOfInterval = currentCodeEnd - currentCodeStart;

                foreach (var letter in alphabetOfProbabilities)
                {
                    currentCodeStart += letter.Value * lengthOfInterval / countOfLetters;
                    stringCodeStart = currentCodeStart.ToString();
                    if (currentCodeStart > curNumber)
                    {
                        currentCodeEnd = currentCodeStart;
                        currentCodeStart -= letter.Value * lengthOfInterval / countOfLetters;
                        res += letter.Key;
                        break;
                    }
                }

                stringCodeStart = currentCodeStart.ToString();
                stringCodeEnd = currentCodeEnd.ToString();
                while(stringCodeStart.Length > stringCodeEnd.Length)
                {
                    stringCodeEnd += "0";
                }
                while (stringCodeStart.Length > 0 && stringCodeEnd.Length > 0 && stringCodeStart[0] == stringCodeEnd[0] &&
                    stringCodeStart[1] != '0' && stringCodeEnd[1] != '0')
                {
                    codeNumber.Remove(0, 1);
                    stringCodeStart = stringCodeStart.Remove(0, 1);
                    stringCodeEnd = stringCodeEnd.Remove(0, 1);
                }
                if(stringCodeStart.Length > codeNumber.Length && (stringCodeStart.IndexOf(",") == -1 || stringCodeStart.IndexOf(",") > codeNumber.Length))
                {
                    codeNumber.Append("0");
                    stringCodeStart = stringCodeStart.Substring(0, codeNumber.Length);
                    stringCodeEnd   = stringCodeEnd.Substring(0, codeNumber.Length);
                }
                currentCodeStart = double.Parse(stringCodeStart);
                currentCodeEnd = double.Parse(stringCodeEnd);
            }

            return res;

        }
    }
}
