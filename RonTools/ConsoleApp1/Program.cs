using RonTools;
using RonTools.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{


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
            var n = new RonTreeNode<int>(1);
            new RonTreeNode<int>(2, n);
            new RonTreeNode<int>(8, n);
            new RonTreeNode<int>(9, n);

            new RonTreeNode<int>(3, n.Children[0]);
            new RonTreeNode<int>(7, n.Children[0]);

            new RonTreeNode<int>(4, n.Children[0].Children[0]);
            new RonTreeNode<int>(5, n.Children[0].Children[0]);
            new RonTreeNode<int>(6, n.Children[0].Children[0]);

            new RonTreeNode<int>(10, n.Children[2]);
            new RonTreeNode<int>(12, n.Children[2]);

            new RonTreeNode<int>(11, n.Children[2].Children[0]);//this is answer.

            var r = Algorithm.DepthFirstSearch(n, i => i == n.Children[2].Children[0].Children[0]);
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
            Dictionary<string, RonTreeNode<RonMinimaxNode<string>>> dic = new Dictionary<string, RonTreeNode<RonMinimaxNode<string>>>();
            dic.Add("a", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("a")));
            dic.Add("b", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("b"), dic["a"]));
            dic.Add("c", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("c"), dic["a"]));
            dic.Add("d", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("d"), dic["a"]));
            dic.Add("e", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("e"), dic["b"]));
            dic.Add("f", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("f"), dic["c"]));
            dic.Add("g", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("g"), dic["c"]));
            dic.Add("h", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("h"), dic["e"]));
            dic.Add("i", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("i"), dic["e"]));
            dic.Add("j", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("j"), dic["g"]));
            dic.Add("k", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("k"), dic["g"]));
            dic.Add("l", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("l"), dic["i"]));
            dic.Add("m", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("m"), dic["i"]));
            dic.Add("n", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("n"), dic["k"]));
            dic.Add("o", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("o"), dic["k"]));
            dic.Add("p", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("p"), dic["o"]));
            dic.Add("q", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("q"), dic["o"]));
            dic.Add("r", new RonTreeNode<RonMinimaxNode<string>>(new RonMinimaxNode<string>("r"), dic["o"]));


            Algorithm.MinimaxSearch(dic["a"], true, EvaluateWeight);

            int EvaluateWeight(RonTreeNode<RonMinimaxNode<string>> node)
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

    }
}
