using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSystem
{
    class Seller
    {

    }
    class Bag
    {
        public List<int> items = Enumerable.Repeat(0, 10).ToList();
        public Stack<List<int>> undo = new Stack<List<int>>();
        public Stack<List<int>> redo = new Stack<List<int>>();
        public override string ToString()
        {
            return "包包內容:"+string.Join(", ", items);
        }
        List<int> Clone() => items.ToList();
        public void GetItem(int slot, int item)
        {
            var old = Clone();
            undo.Push(old);
            redo.Clear();
            items[slot] = item;
        }
        public void SellItem(int slot)
        {
            GetItem(slot, 0);
        }
        public void Undo()
        {
            if (undo.Count == 0) return;
            var oldItems = undo.Pop();
            redo.Push(items);
            items = oldItems;
        }
        public void Redo()
        {
            if (redo.Count == 0) return;
            var oldItems = redo.Pop();
            undo.Push(items);
            items = oldItems;
        }
    }
    class Player
    {
        public Bag bag = new Bag();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            Random r = new Random();
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    if(key>=ConsoleKey.D1 && key<=ConsoleKey.D9)
                    {
                        int item = key - ConsoleKey.D1 +1;
                        int slot = r.Next(9);
                        player.bag.GetItem(slot, item);
                        Console.WriteLine(player.bag);
                    }
                    if(key==ConsoleKey.R)
                    {
                        player.bag.Redo();
                        Console.WriteLine(player.bag);
                    }
                    if (key == ConsoleKey.U)
                    {
                        player.bag.Undo();
                        Console.WriteLine(player.bag);
                    }
                }
            }
        }
    }
}
