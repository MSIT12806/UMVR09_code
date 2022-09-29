using System;
using System.Collections.Generic;
using System.Text;

namespace RonTools.DataStruct
{
    public class RonTreeNode<T>
    {
        public readonly RonTreeNode<T> Parent;
        public readonly List<RonTreeNode<T>> Children = new List<RonTreeNode<T>>();
        public readonly T Value;
        public RonTreeNode(T v) { Value = v; }
        public RonTreeNode(T v, RonTreeNode<T> parent)
        {
            Value = v;
            Parent = parent;
            parent.Children.Add(this);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
