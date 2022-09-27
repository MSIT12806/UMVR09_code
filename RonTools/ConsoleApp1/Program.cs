using RonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class Node<T>
    {
        public readonly Node<T> Parent;
        public readonly List<Node<T>> Children = new List<Node<T>>();
        public readonly T Value;
        public Node(T v) { Value = v; }
        public Node(T v, Node<T> parent)
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
    public class MinimaxNode<T>
    {
        public int Weight;
        public bool expanded;
        public bool assigned;
        public T Value;

        public MinimaxNode(T v)
        {
            Value = v;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MinimaxSearchCase();
        }
        static void DepthFirstSearchCase()
        {
            //測試樹狀結構
            //1. 深度優先
            //  測試資料：
            var n = new Node<int>(1);
            new Node<int>(2, n);
            new Node<int>(8, n);
            new Node<int>(9, n);

            new Node<int>(3, n.Children[0]);
            new Node<int>(7, n.Children[0]);

            new Node<int>(4, n.Children[0].Children[0]);
            new Node<int>(5, n.Children[0].Children[0]);
            new Node<int>(6, n.Children[0].Children[0]);

            new Node<int>(10, n.Children[2]);
            new Node<int>(12, n.Children[2]);

            new Node<int>(11, n.Children[2].Children[0]);//this is answer.

            var r = DepthFirstSearch(n, i => i == n.Children[2].Children[0].Children[0]);
            //print result
            bool notFinish = true;
            while (notFinish)
            {
                Console.Write(r.DeQueue(out notFinish).Value);
                if (notFinish) Console.Write("→");
            }
        }

        static void MinimaxSearchCase()
        {
            Dictionary<string, Node<MinimaxNode<string>>> dic = new Dictionary<string, Node<MinimaxNode<string>>>();
            dic.Add("a", new Node<MinimaxNode<string>>(new MinimaxNode<string>("a")));
            dic.Add("b", new Node<MinimaxNode<string>>(new MinimaxNode<string>("b"), dic["a"]));
            dic.Add("c", new Node<MinimaxNode<string>>(new MinimaxNode<string>("c"), dic["a"]));
            dic.Add("d", new Node<MinimaxNode<string>>(new MinimaxNode<string>("d"), dic["a"]));
            dic.Add("e", new Node<MinimaxNode<string>>(new MinimaxNode<string>("e"), dic["b"]));
            dic.Add("f", new Node<MinimaxNode<string>>(new MinimaxNode<string>("f"), dic["c"]));
            dic.Add("g", new Node<MinimaxNode<string>>(new MinimaxNode<string>("g"), dic["c"]));
            dic.Add("h", new Node<MinimaxNode<string>>(new MinimaxNode<string>("h"), dic["e"]));
            dic.Add("i", new Node<MinimaxNode<string>>(new MinimaxNode<string>("i"), dic["e"]));
            dic.Add("j", new Node<MinimaxNode<string>>(new MinimaxNode<string>("j"), dic["g"]));
            dic.Add("k", new Node<MinimaxNode<string>>(new MinimaxNode<string>("k"), dic["g"]));
            dic.Add("l", new Node<MinimaxNode<string>>(new MinimaxNode<string>("l"), dic["i"]));
            dic.Add("m", new Node<MinimaxNode<string>>(new MinimaxNode<string>("m"), dic["i"]));
            dic.Add("n", new Node<MinimaxNode<string>>(new MinimaxNode<string>("n"), dic["k"]));
            dic.Add("o", new Node<MinimaxNode<string>>(new MinimaxNode<string>("o"), dic["k"]));
            dic.Add("p", new Node<MinimaxNode<string>>(new MinimaxNode<string>("p"), dic["o"]));
            dic.Add("q", new Node<MinimaxNode<string>>(new MinimaxNode<string>("q"), dic["o"]));
            dic.Add("r", new Node<MinimaxNode<string>>(new MinimaxNode<string>("r"), dic["o"]));


            MinimaxSearch(dic["a"], true, EvaluateWeight);

            int EvaluateWeight(Node<MinimaxNode<string>> node)
            {
                switch (node.Value.Value)
                {
                    case "d":
                    case "h":
                    case "m":
                    case "p":
                    case "r":
                        return -1;
                    case "f":
                    case "l":
                    case "j":
                    case "n":
                    case "q":
                        return 1;
                    default:
                        throw new InvalidCastException("錯誤的node");
                }
            }

        }
        static void RonStackTest()
        {
            int a = 1, b = 2, c = 3;
            //var @struct = new RonStack<int>();
            var @struct = new RonQueue<int>();
            @struct.EnQueue(a);
            @struct.EnQueue(b);
            @struct.EnQueue(c);

            bool notFinish = true;
            while (notFinish)
            {
                Console.Write(@struct.DeQueue(out notFinish));
                if (notFinish) Console.Write("→");
            }
            //Console.Write(@struct.DeQueue(out notFinish)); exception
        }

        static RonQueue<Node<T>> DepthFirstSearch<T>(Node<T> head, Func<Node<T>, bool> ansFunc)
        {
            /**
             * 深度優先設計：
             *一、思路：
             *探詢一個節點下面的子節點，
             *一路探詢到葉子，
             *再往葉子的兄弟節點探詢。
             *二：虛擬code：
             *1. Initial step:
             *1.1 set arr is a list.
             *1.2 insert initial node to the front of arr.
             *2. loop step:
             *2.1 set n is first node on arr.
             *2.2 n is target node? :
             *2.21 then return n and path from the initial node to n.
             *2.22 otherwise remove n from arr and add n's children to the front of arr. labeling each with its path and continue loop.
             *三、實作概念補充
             *1. 資料為 first-in-last-out ，可用 stack
             *2. 回傳一個從起點到終點的Queue的資料結構，所以初始化階段也要一個Queue
             *3. 將驗證終點的規則包成委派讓調用者決定
             */

            //init.
            RonQueue<Node<T>> result = new RonQueue<Node<T>>();
            RonStack<Node<T>> list = new RonStack<Node<T>>(); //1
            list.Push(head);
            Node<T> nParent = null;
            //loop
            while (true)
            {
                Node<T> n = list.Pop();//s2
                if (n.Parent != nParent)
                {
                    //若 取出的節點的父節點 不是 標記的父節點，則意味著已經搜索到別的路徑了。
                    //就要更換路徑紀錄。
                    bool noUse;
                    result.DeQueue(out noUse);
                    nParent = n.Parent;
                }
                if (ansFunc(n))
                {
                    result.EnQueue(n);
                    return result;
                }
                else
                {
                    if (n.Children == null || n.Children.Count == 0) continue;
                    result.EnQueue(n);
                    nParent = n;
                    for (int i = 0; i < n.Children.Count; i++)
                    {
                        list.Push(n.Children[i]);
                    }
                }
            }
        }
        enum SearchType
        {
            Maximum,
            Minumum,
        }
        static void MinimaxSearch<T>(Node<MinimaxNode<T>> head, bool ismMaximizingNode, Func<Node<MinimaxNode<T>>, int> evaluationFunc, int limitLayer = 99)
        {
            /*
             * 概念解說：
             * 如何評價眼前這一步的好壞？由自己的下一步的勝率來總和評估。依此類推......
             * 已知勝率的節點可以傳 勝率 給父節點，若父節點為計算最大勝率，則父節點的勝率應為 子節點中勝率最大的，反之反之。
             * 
             */
            /*
             * 1. Set L = { a }, the unexpanded nodes in the tree 
             * 2. Let x be the 1st node on L. If x = a and there is a value assigned to it, return this value. 
             * 3. If x has been assigned a value vx, let p be the parent of x and vp the value currently assigned to p. If p is a minimizing node, set vp = min(vp, vx). If p is a maximizing node, set vp = max(vp, vx). Remove x from L and return to step 2. 
             * 4. If x has not been assigned a value and either x is a terminal node or we have decided not to expand the tree further, compute its value using the evaluation function. Leave x on L and return to step 2. 
             * 5. Otherwise, set vx to be –∞ if x is a maximizing node and + ∞ if x is a 
             *    minimizing node. Add the children of x to the front of L and return to step 2.
             */

            /*
             * Init:
             *   Add the children of x to the front of L → Use Stack Store
             *   Push head in L
             *   need count layer to compute the weight.
             * recursion:(因為無法正確判斷層數，所以無法使用loop)
             *   
             */

            //init
            RonStack<Node<MinimaxNode<T>>> list = new RonStack<Node<MinimaxNode<T>>>();
            list.Push(head);
            int layer = 0;
            //loop
            recursion(ismMaximizingNode);

            void recursion(bool ismMaximizing)
            {
                Node<MinimaxNode<T>> x = list.Pop();
                if (x.Value.assigned && x == head)
                {
                    Console.WriteLine($"node: {x.Value.Value}, weight:{x.Value.Weight}");
                    return;
                }
                else if (x.Value.assigned)
                {
                    if (ismMaximizing)
                    {
                        x.Parent.Value.Weight = Math.Max(x.Parent.Value.Weight, x.Value.Weight);
                    }
                    else
                    {
                        x.Parent.Value.Weight = Math.Min(x.Parent.Value.Weight, x.Value.Weight);
                    }
                    x.Parent.Value.assigned = true;
                    Console.WriteLine($"node: {x.Value.Value}, weight:{x.Value.Weight}");
                    recursion(!ismMaximizing);
                }
                else
                {
                    list.Push(x);//還沒傳給父母? 給我加回去！
                    if (x.Children.Count == 0 || layer >= limitLayer)
                    {
                        x.Value.Weight = evaluationFunc(x);
                        x.Value.assigned = true;

                        recursion(!ismMaximizing);
                    }
                    else
                    {
                        if (ismMaximizing)
                            x.Value.Weight = int.MinValue;
                        else
                            x.Value.Weight = int.MaxValue;
                        foreach (var item in x.Children)
                        {
                            list.Push(item);
                            layer++;
                        }
                        recursion(!ismMaximizing);
                    }
                }
            }
        }
    }
}
