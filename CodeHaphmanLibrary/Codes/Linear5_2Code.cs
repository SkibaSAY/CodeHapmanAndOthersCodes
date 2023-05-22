using CodeHaphmanLibrary.Codes.Linear5_2;
using CodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class Linear5_2Code : ICode
    {
        public static readonly int n = 5;
        public static readonly int k = 2;
        public static readonly int d = n-k;

        /// <summary>
        /// порождающая матрица
        /// </summary>
        private Matrix G = new Matrix(new int[,]
        {
            { 1,0,   1,1,1 },
            { 0,1,   0,1,1 }
        },2,5);

        /// <summary>
        /// проверочная матрица
        /// </summary>
        private Matrix H = new Matrix(new int[,]
        {
            { 1,0,   1,0,0 },
            { 1,1,   0,1,0 },
            { 1,1,   0,0,1 }
        },3, 5);

        /// <summary>
        /// Таблица восстанавления ошибок
        /// key:v+e, value:v
        /// Я отказался от синдрома, он тут не обязателен, дерево вполне справляется с поискать исходного сообщения
        /// </summary>
        private Dictionary<string, string> TableOfErors = new Dictionary<string, string>
        {
            { "00000","00000" },
            { "10111","10111" },
            { "01011","01011" },
            { "11100","11100" },

            { "10000","00000" },
            { "01000","00000" },
            { "00100","00000" },
            { "00010","00000" },
            { "00001","00000" },
            { "10001","00000" },
            { "10010","00000" },

            { "00111","10111" },
            { "11111","10111" },
            { "10011","10111" },
            { "10101","10111" },
            { "10110","10111" },
            { "00110","10111" },
            { "00101","10111" },

            { "11011","01011" },
            { "00011","01011" },
            { "01111","01011" },
            { "01001","01011" },
            { "01010","01011" },
            { "11010","01011" },
            { "11001","01011" },

            { "01100","11100" },
            { "10100","11100" },
            { "11000","11100" },
            { "11110","11100" },
            { "11101","11100" },
            { "01101","11100" },
            { "01110","11100" },
        };

        private Matrix H_Trans;

        public Linear5_2Code()
        {
            H_Trans = H.Tranzponent();
        }

        public void Code(string inputText, out string outputText, out string resourses)
        {
            //потребуется для восстановления
            resourses = inputText.Length.ToString();

            if (inputText.Length % 2 != 0) inputText += '0';
            var list_u = new List<Matrix>();
            for(var i=0; i < inputText.Length; i += 2)
            {
                var current = new Matrix(new int[1, 2] 
                {
                    {int.Parse(inputText[i].ToString()),int.Parse(inputText[i+1].ToString()) }                  
                },1,2);

                list_u.Add(current);
            }

            var all_v = list_u.SelectMany(u => (u*G).GetItems());
            outputText = String.Join("", all_v);
        }

        public double CompressionRate(string inputText, string outputText, string resourses)
        {
            return d*1.0/n;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            var list_v_e = new List<Matrix>();
            for (var i = 0; i < inputText.Length; i += 5)
            {
                var current = new Matrix(new int[1, 5]
                {
                    {
                        int.Parse(inputText[i].ToString()),
                        int.Parse(inputText[i+1].ToString()),
                        int.Parse(inputText[i+2].ToString()),
                        int.Parse(inputText[i+3].ToString()),
                        int.Parse(inputText[i+4].ToString())
                    }
                }, 1, 5);

                list_v_e.Add(current);
            }

            //var sindromArr = list_v_e.Select(v_e => String.Join("", (v_e * H_Trans).GetItems())).ToArray();
            var decodedMessages = list_v_e.Select(v_e => String.Join("", v_e.GetItems())).Select(v_e => TableOfErors[v_e].Substring(0, k)).ToArray();
            var message = String.Join("", decodedMessages);

            //в ресурсы мы положили число элементов в исходной сообщении
            //это важно, тк исходное сообщение могло быть нечётной длины
            message = message.Substring(0, int.Parse(resourses));
            outputText = message;
        }
    }
}
