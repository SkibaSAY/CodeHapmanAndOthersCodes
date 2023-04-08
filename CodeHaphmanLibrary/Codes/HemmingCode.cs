using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLibrary;
using System.IO;
using Newtonsoft.Json;

namespace HemmingCodeLibrary
{
    public class HemmingCode : ICode
    {
        //будем кодировать блоками по 16 бит
        private int sizeOfBlock = 16;

        //для 16 должно быть 5
        private int sizeOfCodingMatrix;
        private int widthOfCodeMatrix;

        private bool[,] codeMatrix;

        private List<long> stepsOf2 = new List<long>();
        public HemmingCode()
        {
            sizeOfCodingMatrix = 0;
            widthOfCodeMatrix = sizeOfBlock;
            int stepOf2 = 1;
            while(stepOf2 <= widthOfCodeMatrix)
            {
                stepsOf2.Add(stepOf2);
                stepOf2 *= 2;
                sizeOfCodingMatrix++;
                widthOfCodeMatrix++;
            }

            //заполняем матрицу Хэмминга
            codeMatrix = new bool[sizeOfCodingMatrix, widthOfCodeMatrix];
            bool currentBit = true;
            for(int i = 0; i < sizeOfCodingMatrix; i++)
            {
                currentBit = true;
                for (int j = 0; j < widthOfCodeMatrix; j++)
                {
                    if (j % stepsOf2[i] == 0)
                    {
                        currentBit = !currentBit;
                    }
                    codeMatrix[i, j] = currentBit;
                }
            }
        }


        public void Code(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            var inputText = File.ReadAllText(inputFilePath);

            var codedText = Coding(inputText);

            File.WriteAllText(outputFilePath, codedText);

            var jsonResourse = JsonConvert.SerializeObject(codedText);
            File.WriteAllText(resoursesPath, jsonResourse);
        }

        public string Coding(string inputText)
        {
            var input = new StringBuilder(inputText);

            var codedText = "";
            var currentInputBlock = "";
            while (input.Length > 0)
            {
                currentInputBlock = inputText.Substring(0, Math.Min(input.Length, 16));
                codedText += CodeOneBlock(currentInputBlock);
                input = input.Remove(0, Math.Min(input.Length, 16));
            }
            return codedText;
        }

        private string CodeOneBlock(string inputBlock)
        {
            int lengthOfResBlock = widthOfCodeMatrix;
            if (inputBlock.Length < sizeOfBlock)
            {
                var countOfControlBytes = 0;
                lengthOfResBlock = inputBlock.Length;
                while (stepsOf2[countOfControlBytes] <= lengthOfResBlock)
                {
                    countOfControlBytes++;
                    lengthOfResBlock++;
                }
            }
            var codedBlock = new bool[lengthOfResBlock];

            //расставляем информационные биты по местам, которые
            //не являются степенями двойки
            int countOfFilledBits = 0;
            
            for(int i = 0; i < lengthOfResBlock; i++)
            {
                if (!stepsOf2.Contains(i+1))
                {
                    if (!"01".Contains(inputBlock[countOfFilledBits]))
                    {
                        throw new ArgumentException("Для кода Хэмминга требуется двоичная последовательность");
                    }
                    codedBlock[i] = inputBlock[countOfFilledBits] =='1';
                    countOfFilledBits++;
                }
            }

            //заполняем контрольные биты
            for (int i = 0; i < stepsOf2.Count; i++)
            {
                if (stepsOf2[i] - 1 >= lengthOfResBlock) break;
                bool controlBit = false;
                for(int j = 0; j < lengthOfResBlock; j++)
                {
                    if (!stepsOf2.Contains(j + 1) && codeMatrix[i, j])
                    {
                        controlBit = controlBit ^ codedBlock[j];
                    }
                }
                codedBlock[stepsOf2[i]-1] = controlBit;
            }

            //получаем строку из битов
            var res = "";
            for(int i =0; i< lengthOfResBlock; i++)
            {
                if (codedBlock[i]) res += "1";
                else res += "0";
            }
            return res;
        }


        public decimal CompressionRate(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            throw new NotImplementedException();
        }

        public void Decode(string inputFilePath, string outputFilePath, string resoursesPath)
        {
            throw new NotImplementedException();
        }
    }
}
