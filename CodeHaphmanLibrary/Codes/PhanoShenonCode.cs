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
    public class PhanoShenonCode
    {
        private Dictionary<char, string> dictionary = new Dictionary<char, string>();
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

            //fiil the tree
            var tree = new ArrTreeNode();
            tree.value = alphabetOfProbabilities.OrderBy(x => -x.Value).ToList();
            var queue = new Queue<ArrTreeNode>();
            queue.Enqueue(tree);
            var currentNode = tree;
            while (queue.Count() > 0)
            {
                currentNode = queue.Dequeue();
                var pair = DivideHeap(currentNode);
                if (pair.Item1.Count > 0 && pair.Item2.Count != 0)
                {
                    currentNode.leftChildren = new ArrTreeNode();
                    currentNode.leftChildren.value = pair.Item1;
                    currentNode.leftChildren.code = currentNode.code + "0";
                    queue.Enqueue(currentNode.leftChildren);
                }
                if (pair.Item2.Count > 0)
                {
                    currentNode.rightChildren = new ArrTreeNode();
                    currentNode.rightChildren.value = pair.Item2;
                    currentNode.rightChildren.code = currentNode.code + "1";
                    queue.Enqueue(currentNode.rightChildren);
                }
                else
                {
                    dictionary.Add(currentNode.value[0].Key, currentNode.code);
                }
                
            }

            //build the code
            foreach(var symbol in inputText)
            {
                res += dictionary[symbol];
            }


            return res;
        }

        private (List<KeyValuePair<char, int>>, List<KeyValuePair<char, int>>) DivideHeap(ArrTreeNode node)
        {
            var arrForDividing = node.value;
            int sum = arrForDividing.Sum(x=> x.Value);
            int count = arrForDividing.Count();
            var res1 = new List<KeyValuePair<char, int>>();
            var res2 = new List<KeyValuePair<char, int>>();
            int sum1 = 0;
            var half = Math.Ceiling((decimal)(sum * 1.0 / 2));

            for (int i = 0;i < count;i++)
            {
                var elem = arrForDividing[i];
                if(sum1 + elem.Value <= half)
                {
                    res1.Add(elem);
                    sum1 += elem.Value;
                }
                else
                {
                    res2.Add(elem);
                }
            }
            if(res1.Count() == 0)
            {
                return (res2, res1);
            }
            return (res1,res2);
        }

        public double CompressionRate(string inputText, string outputText)
        {
            var beforeCodingSize = inputText.Length*8*1.0;
            var resoursesSize = dictionary.Sum(x => x.Value.Length + 8);
            var afterCodingSize = outputText.Length + resoursesSize;
            return Math.Round(beforeCodingSize / afterCodingSize, 2);
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            var jsonResourse = resourses;

            dictionary = JsonConvert.DeserializeObject<Dictionary<char, string>>(jsonResourse);

            var decodedText = Decoding(inputText);
            outputText = decodedText;
        }

        public string Decoding(string outputText)
        {
            var res = "";

            var codedText = outputText;
            while(codedText != "")
            {
                var currentCode = codedText[0].ToString();
                int i = 0;
                while (!dictionary.ContainsValue(currentCode))
                {
                    i++;
                    currentCode += codedText[i].ToString();
                }
                var letter = dictionary.First(x => x.Value == currentCode);
                res += letter.Key;
                codedText = codedText.Remove(0, currentCode.Length);
            }

            return res;
        }
    }
    class ArrTreeNode
    {
        public ArrTreeNode leftChildren;
        public ArrTreeNode rightChildren;

        //public string name;
        public List<KeyValuePair<char, int>> value;
        public string code;
        public ArrTreeNode()
        {
            //this.name = name;
            value = new List<KeyValuePair<char, int>>();
            code = "";
        }


        //public int CompareTo(TreeNode other)
        //{
        //    if (other.value == this.value) return this.name.CompareTo(other.name);
        //    return this.value.CompareTo(other.value);
        //}
    }
}


