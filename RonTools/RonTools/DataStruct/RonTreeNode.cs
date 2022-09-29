using System;
using System.Collections.Generic;
using System.Text;

namespace RonTools.DataStruct
{
    public class RonTreeNode<T>
    {
        public RonTreeNode<T> Parent;
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
    public class RonBinraryTreeNode<T>
    {
        public readonly T Value;
        public RonBinraryTreeNode<T> Parent;
        public RonBinraryTreeNode<T> Left;
        public RonBinraryTreeNode<T> Right;
        public RonBinraryTreeNode(T value)
        {
            Value = value;
        }

        public bool IsFull { get { return Left == null || Right == null; } }
    }
    /*
     * 一個堆積樹必定為完整二元樹(complete binary tree), 且通常會用陣列來實作.
     */

    /*
     * 加入一個元素(12)
     * 首先, 新加入的元素會被放到最後的葉節點
     * 然後新加入的節點會跟父節點做比對, 若小於父節點則往上升, 直至滿足堆積樹的條件為止才停下來
     * 堆積樹取極值是O(1)，加入 & 取出 O(logN)
     */
    public class RonNode<T>
    {
        public readonly T Value;
        public RonNode(T v)
        {
            Value = v;
        }
    }
    public class RonHeapTree<T> where T : IComparable
    {
        private List<RonNode<T>> _tree = new List<RonNode<T>>();
        public int Tail { get { return _tree.Count - 1; } }
        public RonBinraryTreeNode<T> Head;
        public int Add(T newValue)
        {
            RonNode<T> newNode = new RonNode<T>(newValue);
            _tree.Add(newNode);
            int doSwapIdx = Tail;
            while (true)
            {
                if (!Swap(doSwapIdx, out doSwapIdx))
                {
                    break;
                }
            }            
            return Tail;
        }
        bool Swap(int nowNodeIndex, out int pi)
        {
            var nowNode = _tree[nowNodeIndex];
            (int _pi, RonNode<T> p) = GetParent(nowNodeIndex);
            pi = _pi;
            if(p == null) return false;
            int r = nowNode.Value.CompareTo(p.Value);
            if (r > 0)
            {
                _tree[pi] = _tree[nowNodeIndex];
                _tree[nowNodeIndex] = p;
                return true;
            }
            return false;
                
        }
        public (int index, RonNode<T> item) GetParent(int nodeIndex)
        {

            if (nodeIndex > 0)
            {
                int i = (int)((Tail + 1) / 2) - 1;
                return (i, _tree[i]);
            }
            else
            {
                return (-1, null);
            }
        }

    }
}
