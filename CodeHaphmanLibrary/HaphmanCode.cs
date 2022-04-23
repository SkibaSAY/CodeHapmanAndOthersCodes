using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaphmanCodeLibrary
{
    public static class HaphmanCode
    {
        /// <summary>
        /// Оболочка , запускающая алгоритм построения и отлавливающая любую ошибку в ходе построения кода Хафмана
        /// </summary>
        /// <returns>
        ///     0-ошибка
        ///     1-успешно
        /// </returns>
        public static int BuildCode(string file)
        {
            try
            {
                Run(file);
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 1;
        }

        private static void Run(string file)
        {
            var frequencies = GetFrequencies(file);
            var codes = GetCodeHaphman(frequencies);
        }

        private static Dictionary<char, int> GetFrequencies(string file)
        {
            var frequencies = new Dictionary<char, int>();
            using (var sr = new StreamReader(file))
            {
                foreach (var ch in sr.ReadToEnd().Replace(" ",""))
                {
                    if (frequencies.ContainsKey(ch)) frequencies[ch]++;
                    else frequencies.Add(ch, 1);
                }
            }
            return frequencies;
        }
        private static Dictionary<char, string> GetCodeHaphman(IEnumerable<KeyValuePair<char, int>> frequencies)
        {
            var result = new Dictionary<char, string>();
            var set = new SortedSet<TreeNode>();

            foreach (var key_value in frequencies)
            {
                set.Add(
                    new TreeNode(name: key_value.Key.ToString(),key_value.Value)
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
                    if (child.name.Length!=1)
                    {
                        child.name = node.name + a;
                        queue.Enqueue(child);
                    }
                    else result.Add(child.name[0],node.name+a);
                    a++;
                }
            }
            return result;
        }
    }

    internal class TreeNode:IComparable<TreeNode>
    {
        public List<TreeNode> children;

        public string name;
        public int value;
        public TreeNode(string name = "",int value = 0)
        {
            this.name = name;
            this.value = value;
            children = new List<TreeNode>();
        }

        public int CompareTo(TreeNode other)
        {
            if (other.value == this.value) return this.name.CompareTo(other.name);
            return this.value.CompareTo(other.value);
        }
    }
}
