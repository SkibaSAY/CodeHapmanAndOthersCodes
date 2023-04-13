using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHaphmanLibrary.Codes.LZ_77_Code
{
    public class LzBuffer : IBuffer<char>
    {
        private StringBuilder _items = new StringBuilder();
        public LzBuffer(string inputText ="",int size = 1024)
        {
            this.Size = size;
            Append(inputText.ToArray());
        }
        public int Size { get; set; }

        public int Count => _items.Length;

        public void Append(params char[] newItems)
        {
            _items.Insert(0, new string(newItems));
            BalanceSize();
        }

        private void BalanceSize()
        {
            if(_items.Length > Size)
            {
                _items.Remove(Size, _items.Length - Size);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public int FindFirstIndex(IEnumerable<char> searchedItems)
        {
            var  index = BrutForce<char>.FindFirst(_items.ToString(), searchedItems, Comparer<char>.Default);
            return index;
        }

        public bool Contains(params char[] searchedItems)
        {
            var res = FindFirstIndex(searchedItems);
            return res != -1 ? true : false;
        }

        public char Next()
        {
            if (_items.Length == 0) throw new Exception("Buffer is Empty");           

            var next = _items[_items.Length - 1];
            _items.Remove(_items.Length - 1,1);

            return next;
        }
    }
}
