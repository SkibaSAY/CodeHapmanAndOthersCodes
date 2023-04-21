using CodeHaphmanLibrary.Codes;
using CodeHaphmanLibrary.Codes.BWT;
using CodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class BWTCode : ICode
    {
        public void Code(string inputText, out string outputText, out string resourses)
        {
            resourses = null;
            var result = Coding(inputText);
            outputText = $"{result.lastColumn},{result.rowNumber}";
        }
        public (string lastColumn, int rowNumber) Coding(string inputText)
        {
            var result = (lastColumn: "", rowNumber: 0);

            var len = inputText.Length;
            var rows = new char[len][];
            rows[0] = inputText.ToArray();
            for(var i = 1; i < len; i++)
            {
                var temp = new char[len];
                temp[0] = rows[i - 1][len - 1];
                Array.Copy(rows[i - 1], 0, temp, 1, len - 1);
                rows[i] = temp;
            }

            var rowsAsStrings = rows.Select(row => new String(row)).ToList();
            rowsAsStrings.Sort();

            result.lastColumn = new String(rowsAsStrings.Select(row => row[len - 1]).ToArray());
            result.rowNumber = rowsAsStrings.ToList().IndexOf(inputText);

            result.lastColumn = RLECode.Coding(result.lastColumn);
            return result;
        }

        public double CompressionRate(string inputText, string outputText, string resourses)
        {
            var rate = inputText.Length * 8.0 / ((outputText.Length - 1) * 8);
            return rate;
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            var arr = inputText.Split(new char[] { ',' });
            var code = (lastColumn: arr[0], rowNumber: int.Parse(arr[1]));
            outputText = Decoding(code);
        }

        private ListComparer<List<char>> _listComparer = new ListComparer<List<char>>();
        public string Decoding((string lastColumn, int rowNumber) code)
        {
            code.lastColumn = RLECode.Decoding(code.lastColumn);
            var lastColumn = code.lastColumn;
            var len = code.lastColumn.Length;
            var rows = new Char[len].Select(l=>new List<char>()).ToArray();

            for (var i = 0;i < len; i++)
            {
                for(var j = 0; j < len; j++)
                {
                    rows[j].Insert(0, lastColumn[j]);
                }
                Array.Sort(rows, _listComparer);
            }

            var result = new String(rows[code.rowNumber].ToArray());
            return result;
        }
    }
}
