using System;
using System.Collections.Generic;
using System.Text;

namespace RonTools.DataStruct
{
    public class RonMinimaxNode<T>
    {
        public int Weight;
        public bool expanded;
        public bool assigned;
        public T Value;

        public RonMinimaxNode(T v)
        {
            Value = v;
        }
    }
}
