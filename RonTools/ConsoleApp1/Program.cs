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
    public class Coordinate
    {
        public int X; public int Y;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
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
        public override string ToString()
        {
            return $"({X},{Y})";
        }       
    }
    public class AStarNode<T>
    {
        public T Location;
        public T Parent;
        public float CostFromStart;
        public float CostToGoal;
        public float TotalCost;

    }
    class Program
    {
        static void Main(string[] args)
        {
            // MinimaxSearchCase();
            HeapTreeCase();
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

            Console.WriteLine(heapTree.ToString());
            Console.WriteLine(heapTree.TakeAway());
            Console.WriteLine(heapTree.ToString());

            //改成最小堆積
            int CompareAB(int a, int b)
            {
                return -a.CompareTo(b);
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
            Func<Coordinate, bool> canGo = (p) => !Obstacle.Contains(p);
            Func<Coordinate, float> calculateWeight = (p) => p.GetDistance(goal);
            var st = AStarSearch(starPoint, goal, canGo, calculateWeight);

        }

        private static object AStarSearch(Coordinate starPoint, Coordinate goal, Func<Coordinate, bool> canGo, Func<Coordinate, float> calculateWeight)
        {
            /*
 AStarSearch( location StartLoc, location GoalLoc, agenttype Agent)
{
clear Open & Closed
// initialize a start node
StartNode.Loc = StartLoc;
StartNode.CostFromStart = 0;
StartNode.CostToGoal = PathCostEstimate(StartLoc, GoalLoc, Agent);
StartNode.TotalCost = StartNode.CostToGoal ;
StartNode.Parent = NULL;
push StartNode on Open;

// process the list until success or failure
while Open is not empty {
pop Node from Open // Node has the lowest TotalCost... TRY HEAP TREE!!

 // if at a goal, we’re done
if (Node is a goal node) {
construct a path backward from Node to StartLoc
return SUCCESS;
}
else {
for each successor NewNode of Node {
NewCost = Node.CostFromStart + TraverseCost(Node, NewNode, Agent);

// ignore this node if exists and no improvement
if (NewNode is in Open or Closed) and
(NewNode.CostFromStart <= NewCost) {
continue;
}
else { // store the new or improved information
NewNode.Parent = Node;
NewNode.CostFromStart = NewCost;
NewNode.CostToGoal = PathCostEstimate(NewNode.Loc, GoalLoc, Agent);
NewNode.TotalCost = NewNode.CostFromStart + NewNode.CostToGoal;
if (NewNode is in Closed) {
remove NewNode from Closed
}
if (NewNode is in Open) {
adjust NewNode’s position in Open
}
else {
Push NewNode onto Open
}
}
}
}
push Node onto Closed
}
}
             */
            throw new NotImplementedException();
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
