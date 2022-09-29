namespace RonTools.DataStruct
{
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
}
