using RonTools;
using RonTools.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class Coordinate
    {
        public int X; public int Y;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public IEnumerable<Coordinate> GetNeighbor()
        {
            var l = new List<Coordinate>();
            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i != X || j != Y)
                        l.Add(new Coordinate(i, j));
                }
            }

            return l;
        }
        public float GetDistance(Coordinate point)
        {
            var dX = point.X - this.X;
            var dY = point.Y - this.Y;
            var distanceSquare = (float)Math.Pow(dX, 2) + (float)Math.Pow(dY, 2);
            return (float)Math.Pow(distanceSquare, 0.5);
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Coordinate))
                return base.Equals(obj);
            else
                return ((Coordinate)obj).X == this.X && ((Coordinate)obj).Y == this.Y;
        }
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !a.Equals(b);
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // MinimaxSearchCase();
            AStarSearchCase(); //(6,6), (5,6), (4,5), (3,5), (2,4), (2,3),
            //(6,6), (5,6), (4,5), (3,4), (2,3),
        }

        private static void HeapTreeCase()
        {
            RonHeapTree<int> heapTree = new RonHeapTree<int>(CompareAB);
            heapTree.Add(9);
            heapTree.Add(5);
            heapTree.Add(1);
            heapTree.Add(7);
            heapTree.Add(5);
            heapTree.Add(3);
            heapTree.Add(10);

            Console.WriteLine(heapTree.ToString());//10, 7, 9, 5, 5, 1, 3 :: 1, 5, 3, 9, 7, 5, 10
            Console.WriteLine(heapTree.TakeAway());//10 :: 1
            Console.WriteLine(heapTree.ToString());//9, 7, 3, 5, 5, 1 :: 3, 5, 5, 9, 7, 10
            Console.WriteLine(heapTree.TakeAway(1));//7 :: 5
            Console.WriteLine(heapTree.ToString());//9, 5, 3, 5, 1 :: 3, 7, 5, 9, 10

            //改成最小堆積
            int CompareAB(int a, int b)
            {
                return a.CompareTo(b);
            }
        }

        static void AStarSearchCase()
        {
            //set issue.
            var Obstacle = new Coordinate[16]
            {
                new Coordinate(7,1),
                new Coordinate(7,2),
                new Coordinate(7,3),
                new Coordinate(7,4),
                new Coordinate(7,5),
                new Coordinate(6,2),
                new Coordinate(6,3),
                new Coordinate(6,4),
                new Coordinate(6,5),
                new Coordinate(5,2),
                new Coordinate(5,3),
                new Coordinate(5,4),
                new Coordinate(4,3),
                new Coordinate(4,4),
                new Coordinate(3,3),
                new Coordinate(3,4),
            };
            var starPoint = new Coordinate(2, 3);
            var goal = new Coordinate(6, 6);
            Func<Coordinate, Coordinate, float> calculateWeight = (p, g) =>
            {
                if (Obstacle.Contains(p) || Obstacle.Contains(g))
                    return float.MaxValue;
                else
                    return p.GetDistance(g);
            };
            Func<Coordinate, IEnumerable<Coordinate>> getSuccessor = (p) => p.GetNeighbor();
            var st = AStarNode<Coordinate>.AStarSearch(starPoint, goal, calculateWeight, getSuccessor);
            Console.WriteLine(st.ToString());
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
