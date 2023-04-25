using CodeLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodesLibrary
{
    public class HaphmanCode
    {
        public HaphmanCode()
        {

        }

        public void Code(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputText = File.ReadAllText(inputFilePath);

            var code = BuildCode(inputText);
            var codedText = HaphmanCode.Coding(inputText, code);
            File.WriteAllText(outputFilePath, codedText);

            var jsonResourse = JsonConvert.SerializeObject(code);
            File.WriteAllText(resoursesPath, jsonResourse);
        }

        public decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var beforeCodingSize = new FileInfo(inputFilePath).Length;
            var afterCodingSize = new FileInfo(outputFilePath).Length + new FileInfo(resoursesPath).Length;
            return beforeCodingSize / afterCodingSize;
        }

        public void Decode(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputText = File.ReadAllText(inputFilePath);

            var jsonResourse = File.ReadAllText(resoursesPath);

            var code = JsonConvert.DeserializeObject<Dictionary<char,string>>(jsonResourse);

            var decodedText = HaphmanCode.Decoding(inputText, code);
            File.WriteAllText(outputFilePath, decodedText);
        }

        #region Static Methods
        /// <summary>
        /// Построения кода
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Dictionary<char, string> BuildCode(string inputText)
        {
            var frequencies = GetFrequencies(inputText);
            var codes = GetCodeHaphman(frequencies);
            return codes;
        }

        /// <summary>
        /// Получения частот по входному тексту
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private static Dictionary<char, int> GetFrequencies(string inputText)
        {
            var frequencies = new Dictionary<char, int>();

            //var reg = new Regex(@"[\n,\r]");
            //text = reg.Replace(text, "");

            foreach (var ch in inputText)
            {
                if (frequencies.ContainsKey(ch)) frequencies[ch]++;
                else frequencies.Add(ch, 1);
            }
            return frequencies;
        }

        /// <summary>
        /// Построение кода на основе частот
        /// </summary>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        private static Dictionary<char, string> GetCodeHaphman(IEnumerable<KeyValuePair<char, int>> frequencies)
        {
            var result = new Dictionary<char, string>();
            var set = new SortedSet<TreeNode>();

            foreach (var key_value in frequencies)
            {
                set.Add(
                    new TreeNode(name: key_value.Key.ToString(), key_value.Value)
                );
            }

            //построили дерево
            while (set.Count > 1)
            {
                var first = set.First();
                set.Remove(first);
                var second = set.First();
                set.Remove(second);

                var node = new TreeNode(first.name + second.name);
                node.value = first.value + second.value;
                node.children.Add(first);
                node.children.Add(second);
                set.Add(node);
            }


            var root = set.First();
            set.Remove(root);

            //обратный ход по дереву
            var queue = new Queue<TreeNode>();
            root.name = "";
            queue.Enqueue(root);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                var a = 0;
                foreach (var child in node.children)
                {
                    if (child.name.Length != 1)
                    {
                        child.name = node.name + a;
                        queue.Enqueue(child);
                    }
                    else result.Add(child.name[0], node.name + a);
                    a++;
                }
            }
            return result;
        }

        /// <summary>
        /// Кодирование входной строки
        /// </summary>
        /// <param name="text"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static string Coding(string text, Dictionary<char, string> codes)
        {
            var sb = new StringBuilder();
            foreach (var ch in text)
            {
                if (codes.ContainsKey(ch)) sb.Append(codes[ch]);
                else throw new ArgumentException("Заданный текст и словарь кодов несовместимы");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Декодирование
        /// </summary>
        /// <param name="text"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public static string Decoding(string text, Dictionary<char, string> codes)
        {
            var currentCodes = new Dictionary<string, char>();
            foreach (var key_val in codes)
            {
                currentCodes.Add(key_val.Value, key_val.Key);
            }

            var result = new StringBuilder();
            var currentCode = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (currentCodes.ContainsKey(currentCode.ToString()))
                {
                    result.Append(currentCodes[currentCode.ToString()]);
                    currentCode = new StringBuilder();
                }
                currentCode.Append(text[i]);
            }
            if (currentCodes.ContainsKey(currentCode.ToString()))
            {
                result.Append(currentCodes[currentCode.ToString()]);
            }
            return result.ToString();
        }
        #endregion
    }

    
}
