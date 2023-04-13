using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes.LZ_77_Code
{
    public static class BrutForce<T>
    {
        public static int FindFirst(IEnumerable<T> arr, IEnumerable<T> subArr,Comparer<T> comparer)
        {
            var arrCount = arr.Count();
            var subArrCount = subArr.Count();

            var first = -1;
            for (var i = 0; i < arrCount; i++)
            {
                if(i+subArrCount >= arrCount)
                {
                    break;
                }
                var j = 0;
                for(; j < subArrCount; j++)
                {
                    if (comparer.Compare(arr.ElementAt(i+j), subArr.ElementAt(j)) != 0)
                    {
                        break;
                    }  
                }
                if(j == subArrCount - 1)
                {
                    first = i;
                    break;
                }
            }

            return first;
        }
    }
}
