using System;
using System.Collections.Generic;
using System.Text;
using RonTools.DataStruct;

namespace RonTools
{
    public class Algorithm
    {
        public static RonQueue<RonTreeNode<T>> DepthFirstSearch<T>(RonTreeNode<T> head, Func<RonTreeNode<T>, bool> ansFunc)
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
            RonQueue<RonTreeNode<T>> result = new RonQueue<RonTreeNode<T>>();
            RonStack<RonTreeNode<T>> list = new RonStack<RonTreeNode<T>>(); //1
            list.Push(head);
            RonTreeNode<T> nParent = null;
            //loop
            while (true)
            {
                RonTreeNode<T> n = list.Pop();//s2
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
        public static void MinimaxSearch<T>(RonTreeNode<RonMinimaxNode<T>> head, bool ismMaximizingNode, Func<RonTreeNode<RonMinimaxNode<T>>, int> evaluationFunc, int limitLayer = 99)
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
            RonStack<RonTreeNode<RonMinimaxNode<T>>> list = new RonStack<RonTreeNode<RonMinimaxNode<T>>>();
            list.Push(head);
            int layer = 0;
            //loop 因為無法正確判斷目前在哪一層，所以改成遞迴處理。
            recursion(ismMaximizingNode);

            void recursion(bool ismMaximizing)
            {
                RonTreeNode<RonMinimaxNode<T>> x = list.Pop();
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
