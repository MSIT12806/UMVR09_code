using System;

namespace RonTools
{
    public class RonLinkNode<T>
    {
        RonLinkNode<T> _next;

        public T Value
        {
            get;
            set;
        }
        public RonLinkNode<T> Next
        {
            get { return _next; }
            set { _next = value; }
        }
        public RonLinkNode(T value)
        {
            Value = value;
        }
    }
    public class RonStack<T>
    {
        /*
         * 堆疊：後進先出
         * 1. X Push(T): 將物件存入結構中。
         * 2. T Pop(): 將物件取出。
         */
        RonLinkNode<T> current;
        RonLinkNode<T> link;
        public void Push(T data)
        {
            if (link == null)
            {
                link = new RonLinkNode<T>(data);
                current = link;
            }
            else
            {
                var dStruct = new RonLinkNode<T>(data);
                dStruct.Next = link;
                current = dStruct;
            }
        }
        public T Pop()
        {
            var c = current;
            current = c.Next;
            c.Next = null;
            return c.Value;
        }
    }
    public class RonQueue<T>
    {
        /*
        * 排序：先進先出
        * 1. X EnQueue(T): 將物件存入結構中。
        * 2. T DeQueue(): 將物件取出。
        */

        RonLinkNode<T> current;
        RonLinkNode<T> nextLink;
        public void EnQueue(T data)
        {
            if (nextLink == null)
            {
                nextLink = new RonLinkNode<T>(data);
                current = nextLink;
            }
            else
            {
                var dStruct = new RonLinkNode<T>(data);
                nextLink.Next = dStruct;
                current = dStruct;
            }
        }

        public T DeQueue()
        {
            var c = current;
            current = c.Next;
            c.Next = null;
            return c.Value;
        }
    }
}
