using CodeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodesLibrary
{
    public class PhanoShenonCode : ICode
    {
        private Dictionary<char, int> dictionary = new Dictionary<char, int>();
        public void Code(string inputText, out string outputText, out string resourses)
        {
            var result = Coding(inputText);
            outputText = result;

            var jsonResourse = JsonConvert.SerializeObject(dictionary);
            resourses = jsonResourse;
        }

        public string Coding(string inputText)
        {
            var alphabetOfProbabilities = new Dictionary<char, int>();
            var res = "";
            foreach(var symbol in inputText)
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

            var listOfProbabilities = new TreeNode[alphabetOfProbabilities.Count()];
            int i = 0;
            foreach (var pair in alphabetOfProbabilities)
            {
                listOfProbabilities[i] = new TreeNode(pair.Key.ToString(), pair.Value);
                i++;
            }
            Array.Sort(listOfProbabilities);

            //var nameOfNode = 
            //var set = new TreeNode(String.Join("", alphabetOfProbabilities.Keys), alphabetOfProbabilities.Values.Sum());
            //while ()
            //{
            //    var 
            //}
            

            return res;
        }

        //private int DivideHeap()
        //{

        //}

        public double CompressionRate(string inputText, string outputText, string resourses)
        {
            var beforeCodingSize = inputText.Length;
            var afterCodingSize = outputText.Length + resourses.Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            var jsonResourse = resourses;

            dictionary = JsonConvert.DeserializeObject<Dictionary<char, int>>(jsonResourse);

            var decodedText = Decoding(inputText);
            outputText = decodedText;
        }

        public string Decoding(string outputText)
        {
            throw new NotImplementedException();
        }
    }

}
