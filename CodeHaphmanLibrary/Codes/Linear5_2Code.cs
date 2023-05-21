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
            var sindromArr = list_v_e.Select(v_e => String.Join("", (v_e * H_Trans).GetItems())).ToArray();
            


            outputText = sundrom;
        }
        private string GetFrom
    }
}
