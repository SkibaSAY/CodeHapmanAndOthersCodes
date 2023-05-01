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

        public void Code(string inputText, out string outputText, out string resourses)
        {
            var result = Coding(inputText);
            outputText = result;

            var jsonResourse = JsonConvert.SerializeObject(alphabetOfProbabilities);
            resourses = jsonResourse;
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
                var lengthOfInterval = currentCodeEnd - currentCodeStart;
                foreach (var letter in alphabetOfProbabilities)
                {
                    if (letter.Key == symbol) break;
                    currentCodeStart += letter.Value * lengthOfInterval / countOfLetters;
                }
                currentCodeEnd = currentCodeStart + alphabetOfProbabilities[symbol] * lengthOfInterval / countOfLetters;

                stringCodeStart = NumberToStringStart(currentCodeStart, stringCodeStart);
                stringCodeEnd = NumberToStringEnd(currentCodeEnd, stringCodeStart);
                stringCodeEnd = TrimByStart(stringCodeEnd, stringCodeStart);

                while (stringCodeStart.Length > 0 && stringCodeEnd.Length > 0 && stringCodeStart[0] == stringCodeEnd[0])
                {
                    if (stringCodeStart[0] != ',')
                    {
                        res += stringCodeStart[0];
                    }
                    else if(!res.Contains(","))
                    {
                        res += ",";
                    }
                    stringCodeStart = stringCodeStart.Remove(0, 1);
                    stringCodeEnd = stringCodeEnd.Remove(0, 1);
                }

                stringCodeEnd = TrimByStart(stringCodeEnd, stringCodeStart);
                stringCodeEnd = AddZerosToEnd(stringCodeEnd, stringCodeStart);


                currentCodeStart = double.Parse(stringCodeStart);
                currentCodeEnd = double.Parse(stringCodeEnd);
            }

            return res + stringCodeEnd[0];

        }
        public decimal CompressionRate(string inputText, string outputText, string resourses)
        {
            var beforeCodingSize = inputText.Length;
            var afterCodingSize = outputText.Length + resourses.Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            var jsonResourse = resourses;

            alphabetOfProbabilities = JsonConvert.DeserializeObject<SortedList<char, uint>>(jsonResourse);

            var decodedText = Decoding(inputText);
            outputText = decodedText;
        }

        public string Decoding(string inputNumber)
        {
            ulong countOfLetters = 0;
            var codeNumber = inputNumber;
            codeNumber += "0";

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
                double curNumber;
                if (codeNumber.Contains(","))
                {
                    curNumber = double.Parse(codeNumber.ToString());
                }
                else
                {
                    curNumber = -1;
                }
                var lengthOfInterval = currentCodeEnd - currentCodeStart;
                double predNum = currentCodeStart;
                foreach (var letter in alphabetOfProbabilities)
                {
                    currentCodeStart += letter.Value * lengthOfInterval / countOfLetters;
                    stringCodeStart = NumberToStringStart(currentCodeStart, stringCodeStart);
                    var stringCodeStartForCompare = stringCodeStart;
                    if (!codeNumber.Contains(","))
                    {
                        stringCodeStartForCompare = stringCodeStart.Replace(",", "");
                    }
                    if (curNumber == -1 && stringCodeStartForCompare.CompareTo(codeNumber) > 0 || currentCodeStart > curNumber && curNumber != -1)
                    {
                        currentCodeEnd = currentCodeStart;
                        currentCodeStart = predNum;
                        res += letter.Key;
                        break;
                    }
                    predNum = currentCodeStart;
                }

                stringCodeStart = NumberToStringStart(currentCodeStart, stringCodeStart);
                stringCodeEnd = NumberToStringEnd(currentCodeEnd, stringCodeStart);
                stringCodeEnd = TrimByStart(stringCodeEnd, stringCodeStart);

                while (stringCodeStart.Length > 0 && stringCodeEnd.Length > 0 && stringCodeStart[0] == stringCodeEnd[0])
                {
                    if (stringCodeStart[0] != ',' || codeNumber[0] == ',' && stringCodeStart[0] == ',')
                    { 
                        codeNumber = codeNumber.Remove(0, 1);
                    }
                    stringCodeStart = stringCodeStart.Remove(0, 1);
                    stringCodeEnd = stringCodeEnd.Remove(0, 1);
                }
                stringCodeEnd = TrimByStart(stringCodeEnd, stringCodeStart);
                stringCodeEnd = AddZerosToEnd(stringCodeEnd, stringCodeStart);

                currentCodeStart = double.Parse(stringCodeStart);
                currentCodeEnd = double.Parse(stringCodeEnd);
            }

            return res;

        }
        private string NumberToStringStart(double number, string stringBefore)
        {
            var lengthOfWholePart = WholePartOfNumLength(stringBefore);
            stringBefore = number.ToString();
            var newLengthOfWholePart = WholePartOfNumLength(stringBefore);
            while (newLengthOfWholePart < lengthOfWholePart)
            {
                stringBefore = "0" + stringBefore;
                newLengthOfWholePart = WholePartOfNumLength(stringBefore);
            }
            return stringBefore;
        }
        private string NumberToStringEnd(double number, string startOfInterval)
        {
            var stringBefore = number.ToString();
            var lengthOfWholePart = WholePartOfNumLength(stringBefore);

            var newLengthOfWholePart = WholePartOfNumLength(startOfInterval);

            while (newLengthOfWholePart > lengthOfWholePart)
            {
                stringBefore = "0" + stringBefore;
                lengthOfWholePart = WholePartOfNumLength(stringBefore);
            }
            return stringBefore;
        }
        
        private string AddZerosToEnd(string stringCodeEnd,string stringCodeStart)
        { 
            var wholePartStart = WholePartOfNumLength(stringCodeStart);
            var wholePartEnd = WholePartOfNumLength(stringCodeEnd);
            while (wholePartStart > wholePartEnd)
            {
                stringCodeEnd += "0";
                wholePartEnd = WholePartOfNumLength(stringCodeEnd);
            }
            return stringCodeEnd;
        }
        private int WholePartOfNumLength(string num)
        {
            var lengthOfWholePart = num.IndexOf(",");
            if (lengthOfWholePart < 0) lengthOfWholePart = num.Length;
            return lengthOfWholePart;
        }
        private string TrimByStart(string stringCodeEnd, string stringCodeStart)
        {
            var doublePartStart = DoublePartOfNumLength(stringCodeStart);
            var doublePartEnd = DoublePartOfNumLength(stringCodeEnd);
            while (doublePartStart < doublePartEnd)
            {
                stringCodeEnd = stringCodeEnd.Remove(stringCodeEnd.Length-1);
                doublePartEnd = DoublePartOfNumLength(stringCodeEnd);
            }
            return stringCodeEnd;
        }
        private int DoublePartOfNumLength(string num)
        {
            var lengthOfDoublePart = num.IndexOf(",");
            if (lengthOfDoublePart < 0)
            {
                lengthOfDoublePart = 0;
            }
            else
            {
                lengthOfDoublePart = num.Length - lengthOfDoublePart - 1;
            }
            return lengthOfDoublePart;
        }
    }
}


