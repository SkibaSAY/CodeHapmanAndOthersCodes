using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodesLibrary
{
    public class TreeNode : IComparable<TreeNode>
    {
        public List<TreeNode> children;

        public string name;
        public int value;
        public TreeNode(string name, int value = 0)
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
