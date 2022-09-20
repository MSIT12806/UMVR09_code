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
            /**
             * 深度優先設計：
             *一、思路：
             *探詢一個節點下面的子節點，
             *一路探詢到葉子，
             *再往葉子的兄弟節點探詢。
             *二：虛擬code：
             *1. set arr is list.
             *2. insert initial node to the front of arr.
             *3. set n is first node on arr.
             *4. if n is target node,
             *4.1 then return n and path from the initial node to n.
             *4.2 else remove n from arr and add n's children to the front of arr. and go to step3.
             */
            int thisNodeNumber = 0;
            RonLinkNode<Node> ansLink = new RonLinkNode<Node>();
            RonStack<Node> list = new RonStack<Node>(); //1
            list.Push(head);
            bool isFind = false;
            while (!isFind)
            {
                var n = list.Pop();//s2
                if (n == ans)
                {
                    return Match(n);
                }
                else
                {
                    ansLink.Next = n;
                    for (int i = 0; i < n.Children.Count; i++)
                    {
                        list.Push(n.Children[i]);
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
