using CodeHaphmanLibrary.Codes;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Decode(string inputText, out string outputText, string resourses)
        {
            throw new NotImplementedException();
        }
    }
}
