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
         * ------------
         * 設計一個指標，
         * current，指出目前頂部的元素，
         * 在新元素加入時，將新元素的Next指標指向current，set current to 新元素。
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
                dStruct.Next = current;
                current = dStruct;
            }
        }
        public T Pop()
        {
            var c = current;
            current = c.Next;//若沒有資料了，會出exception
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
        * ----------
        * 設計兩個指標，head & tail
        * head 指向目前隊伍的頭一個，tail，指向目前隊伍最後一個
        * 在新元素加入時，將tail.Next 指向 新元素，並set tail to 新元素
        * 拿出元素時，set head to head.Next，並取出前一個head的元素
        */

        RonLinkNode<T> head;
        RonLinkNode<T> tail;
        public void EnQueue(T data)
        {
            if (tail == null)
            {
                tail = new RonLinkNode<T>(data);
                head = tail;//最早頭尾為同一元素
            }
            else
            {
                var dStruct = new RonLinkNode<T>(data);
                tail.Next = dStruct;
                tail = dStruct;
            }

            //Console.WriteLine($"{data} append on Queue");
        }

        public T DeQueue(out bool notFinish)
        {
            notFinish = head != tail;//所以傳出一個參數，讓外界判斷要不要繼續調用 DeQueue
            var target = head;
            if (notFinish)
            {
                head = target.Next; //若沒有資料了，會出exception
                target.Next = null;
            }
            else
            {
                head = null;
                tail = null;
            }

            //Console.WriteLine($"{target.Value} remove from Queue");
            return target.Value;
        }
    }
}
