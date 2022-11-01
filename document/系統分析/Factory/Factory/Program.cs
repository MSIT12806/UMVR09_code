using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    sealed class Factory<DT>
    {
        public Dictionary<DT, Action> map = new Dictionary<DT, Action>();
        public Func<DT, DT> ChoasConverter { set; get; } = key => key;
        public void SetEvent(DT @event, Action act)
        {
            map[@event] = act;
        }
        public void OnEvent(DT key)
        {
            key = ChoasConverter(key);
            map.TryGetValue(key, out var act);
            act?.Invoke();
        }
    }

    class Program
    {
        static Factory<ConsoleKey> moveControl = new Factory<ConsoleKey>();
        static void Main(string[] args)
        {
            var rand = new Random();
            moveControl.SetEvent(ConsoleKey.UpArrow, () => Console.WriteLine("Up"));
            moveControl.SetEvent(ConsoleKey.DownArrow, () => Console.WriteLine("Down"));
            moveControl.SetEvent(ConsoleKey.LeftArrow, () => Console.WriteLine("Left"));
            moveControl.SetEvent(ConsoleKey.RightArrow, () => Console.WriteLine("Right"));
            moveControl.ChoasConverter = key =>
                moveControl.map.Keys.ToArray()[rand.Next(moveControl.map.Count)];
            while(true)
            {
                if(Console.KeyAvailable)
                {
                    var k = Console.ReadKey();
                    moveControl.OnEvent(k.Key);
                }
            }
        }
    }
}
