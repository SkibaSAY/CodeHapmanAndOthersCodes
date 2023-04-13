using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes.LZ_77_Code
{
    public interface IBuffer<T>
    {
        int Size { get; set; }
        int Count { get; }
        T Next();
        void Clear();
        int FindFirstIndex(IEnumerable<T> searchedItems);
        void Append(params T[] newItems);
        bool Contains(params T[] searchedItems);
    }
}
