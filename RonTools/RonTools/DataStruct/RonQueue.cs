using System;

namespace RonTools.DataStruct
{
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
