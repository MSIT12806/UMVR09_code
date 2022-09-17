using RonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Node
    {
        public readonly Node Parent;
        public readonly List<Node> Children;

        public Node() { }
        public Node(Node parent) { Parent = parent; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //測試樹狀結構
            //1. 深度優先
            //  測試資料：
            var n = new Node();
            n.Children.Add(new Node(n));
            n.Children.Add(new Node(n));
            n.Children.Add(new Node(n));

            n.Children[0].Children.Add(new Node(n.Children[0]));
            n.Children[0].Children.Add(new Node(n.Children[0]));

            n.Children[0].Children[0].Children.Add(new Node(n.Children[0].Children[0]));
            n.Children[0].Children[0].Children.Add(new Node(n.Children[0].Children[0]));
            n.Children[0].Children[0].Children.Add(new Node(n.Children[0].Children[0]));

            n.Children[2].Children.Add(new Node(n.Children[2]));
            n.Children[2].Children.Add(new Node(n.Children[2]));

            n.Children[2].Children[0].Children.Add(new Node(n.Children[2].Children[0]));

            DepthFirstSearch(n, n.Children[2].Children[0].Children[0]);
        }

        static string DepthFirstSearch(Node head, Node ans)
        {
            int thisNodeNumber = 0;
            StringBuilder sb = new StringBuilder();
            RonStack<Node> stack = new RonStack<Node>();
            Node n = head;
            stack.Push(n);
            while (n != ans)
            {

                n = stack.Pop();//s2
                if (n == ans)
                {
                    return Match(n);
                }
                else
                {
                    for (int i = 0; i < n.Children.Count; i++)
                    {
                        thisNodeNumber = i;
                        stack.Push(n.Children[i]);
                    }
                }
            }
        }

        private static string Match(Node n)
        {
            Node head;
            Node now = n;
            StringBuilder sb = new StringBuilder();
            while (now.Parent != null)
            {
                now = now.Parent;
                for (int i = 0; i < now.Children.Count; i++)
                {
                    if(now.Children[i]==n)
                }
            }
        }
    }
}
