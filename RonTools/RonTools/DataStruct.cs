using System;

namespace RonTools
{
    public class RonLinkNode<T>
    {
        public T Current; 
        public T Next;
        public RonLinkNode(T head)
        {
            Current = head;
        }
    }
    public class RonStack<T>
    {
        /*
         * 堆疊：後進先出
         * 1. X Push(T): 將物件存入結構中。
         * 2. T Pop(): 將物件取出。
         */
        RonLinkNode<T> link;
        public void Push(T data)
        {
            if(link == null)
            {
                link = new RonLinkNode<T>(data);
            }
            else
            {
                var dStruct = new RonLinkNode<T>(data);
                dStruct.Next = link.Current;
                link.Current = dStruct.Current;
            }
        }
         public T Pop()
        {
            var r = link.Current;
            link.Current = link.Next;
            return r;
        }
    }
    public class RonQueue<T>
    {
        /*
        * 排序：先進先出
        * 1. X EnQueue(T): 將物件存入結構中。
        * 2. T DeQueue(): 將物件取出。
        */
        RonLinkNode<T> link;
        public void EnQueue(T data)
        {
            if (link == null)
            {
                link = new RonLinkNode<T>(data);
            }
            else
            {
                var dStruct = new RonLinkNode<T>(data);
                link.Next = dStruct.Current;
            }
        }

        public T DeQueue()
        {
            var r = link.Current;
            link.Current = link.Next;
            
            return r;
        }
    }
}
