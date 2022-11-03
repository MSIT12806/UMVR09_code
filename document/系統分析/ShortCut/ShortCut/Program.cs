using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortCut
{
    class Slot
    {
        public Action ClickEvent { set; get; }
        public void OnClick() => ClickEvent?.Invoke();
    }

    static class Commands
    {
        public static void OnUseItem(int id)
        {
            Console.WriteLine($"使用物品:{id}");
        }
        public static void OnEquip(int id)
        {
            Console.WriteLine($"裝備物品:{id}");
        }
        public static void OnSript(string text)
        {
            Console.WriteLine($"執行Script:'{text}'");
        }
    }

    class ClassSlot
    {
        public ICommand command { set; get; }
        public void OnClick() => command?.OnExecute();
    }

    interface ICommand
    {
        void OnExecute();
    }

    class OnUseItem: ICommand
    {
        public int targetId;
        public int itemId;

        public void OnExecute()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Slot> slots = new List<Slot>();
            for (int i = 0; i < 10; i++)
                slots.Add(new Slot());
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey().Key;
                    if(key>=ConsoleKey.F1 && key <= ConsoleKey.F10)
                    {
                        int id = key - ConsoleKey.F1;
                        slots[id].OnClick();
                    }
                    else if(key== ConsoleKey.I)
                    {
                        slots[0].ClickEvent = () => Commands.OnUseItem(10);
                    }
                    else if (key == ConsoleKey.W)
                    {
                        slots[1].ClickEvent = () => Commands.OnEquip(2);
                    }
                    else if (key == ConsoleKey.W)
                    {
                        var cc = new OnUseItem();

                    }
                }
            }
        }
    }
}
