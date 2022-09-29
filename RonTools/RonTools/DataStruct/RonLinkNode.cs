namespace RonTools.DataStruct
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
}
