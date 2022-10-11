using RonTools.DataStruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class AStarNode<T>
    {
        public T Location;
        public AStarNode<T> Parent;
        public float CostFromStart;
        public float CostToGoal;
        public float TotalCost;
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(AStarNode<T>))
                return base.Equals(obj);
            else
                return ((AStarNode<T>)obj).Location.Equals(this.Location);
        }

        public override string ToString()
        {
            List<AStarNode<T>> list = new List<AStarNode<T>>();

            var node = this;
            do
            {
                list.Add(node);
                node = node.Parent;
            } while (node != null);

            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.Location.ToString());
                sb.Append(", ");
            }

            return sb.ToString();
        }

        // AStarSearch(location StartLoc, location GoalLoc, agenttype Agent)
        public static AStarNode<T> AStarSearch<T>(T starPoint, T goal, Func<T, T, float> calculateWeight, Func<T, IEnumerable<T>> getSuccessor)
        {
            //clear Open & Closed
            var open = new RonHeapTree<AStarNode<T>>((a, b) => a.TotalCost < b.TotalCost ? 1 : -1); //get minimum heap tree
            var closed = new List<AStarNode<T>>();

            /*
// initialize a start node
StartNode.Loc = StartLoc;
StartNode.CostFromStart = 0;
StartNode.CostToGoal = PathCostEstimate(StartLoc, GoalLoc, Agent);
StartNode.TotalCost = StartNode.CostToGoal ;
StartNode.Parent = NULL;
push StartNode on Open;
            */
            AStarNode<T> startNode = new AStarNode<T>();
            startNode.Location = starPoint;
            startNode.CostFromStart = calculateWeight(starPoint, starPoint);//0;
            startNode.CostToGoal = calculateWeight(starPoint, goal);
            startNode.TotalCost = startNode.CostFromStart + startNode.CostToGoal;
            open.Add(startNode);

            /*

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
      if (NewNode is in Open or Closed) and (NewNode.CostFromStart <= NewCost) {
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
    push Node onto Closed
  }
}
             */

            while (open.Tail >= 0)
            {
                var nowNode = open.TakeAway(); // Node has the lowest TotalCost
                if (nowNode.Location.Equals(goal)) return nowNode; //construct a path backward from Node to StartLoc

                var neighbours = getSuccessor(nowNode.Location);
                foreach (var newPosition in neighbours)
                {
                    var newCost = nowNode.CostFromStart + calculateWeight(newPosition, nowNode.Location);//calculate new position cost from start.
                    var newNode = new AStarNode<T>();
                    newNode.Location = newPosition;
                    bool newNodeIsInOped = open.FindAndTakeAway(newNode, out AStarNode<T> newNodeHasOldRecord);
                    newNodeHasOldRecord = newNodeHasOldRecord ?? closed.Find(i => i.Location.Equals(newNode.Location));
                    bool newNodeIsInClosed = newNodeHasOldRecord != null;
                    if ((newNodeIsInOped || newNodeIsInClosed) && newNodeHasOldRecord.CostFromStart <= newCost) continue;
                    else
                    {
                        newNode.Parent = nowNode;
                        newNode.CostFromStart = newCost;
                        newNode.CostToGoal = calculateWeight(newNode.Location, goal);
                        newNode.TotalCost = newNode.CostFromStart + newNode.CostToGoal;

                        if (newNodeIsInClosed) closed.Remove(newNodeHasOldRecord);
                        else if (newNodeIsInOped) open.Add(newNode); //adjust NewNode’s position in Open
                        else open.Add(newNode); //Push NewNode onto Open
                    }
                }
                closed.Add(nowNode);
            }
            return null; //return fail.
        }

    }
}
