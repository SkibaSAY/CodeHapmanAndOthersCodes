using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes.Linear5_2
{
    public class Matrix
    {
        private int[,] value;
        public int Wight { get; set; }
        public int Height { get; set; }

        public int this[int h,int w]
        {
            get
            {
                return value[h, w];
            }
            set
            {
                this.value[h, w] = value;
            }
        }

        public Matrix(int[,] args,int h,int w)
        {
            value = args;
            this.Wight = w;
            this.Height = h;
        }
        public List<int> GetItems()
        {
            var result = new List<int>();

            var w = Wight;
            var h = Height;
            for (var i = 0; i < h; i++)
            {
                for(var j = 0; j < w; j++)
                {
                    result.Add(value[i, j]);
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix matrixA, Matrix matrixB)
        {
            if (matrixA.Wight != matrixB.Height) throw new ArgumentException("");

            var temp = new int[matrixA.Height, matrixB.Wight];

            for (int i = 0; i < matrixA.Height; i++)
            {
                for (int j = 0; j < matrixB.Wight; j++)
                {
                    var sum = 0;
                    for(var c = 0; c < matrixB.Height; c++)
                    {
                        sum += matrixA[i, c] * matrixB[c, j];
                    }
                    temp[i, j] = sum % 2;
                }
            }

            var result = new Matrix(temp, matrixA.Height, matrixB.Wight);
            return result;
        }
    }
    public static class MatrixExt
    {
        public static Matrix Tranzponent(this Matrix currMatrix)
        {
            var temp = new int[currMatrix.Wight, currMatrix.Height];

            for (var i = 0; i < currMatrix.Wight; i++)
            {
                for (var j = 0; j < currMatrix.Height; j++)
                {
                    temp[i, j] = currMatrix[j, i];
                }
            }

            var result = new Matrix(temp, currMatrix.Wight, currMatrix.Height);
            return result;
        }
    }
}
