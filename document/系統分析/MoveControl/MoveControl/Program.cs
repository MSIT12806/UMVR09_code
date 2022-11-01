using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveControl
{
    class Proxy
    {
        public virtual ConsoleKey OnKey(ConsoleKey key)
        {
            return key;
        }
    }

    class Player
    {
        public Proxy driver = new Proxy();
        public bool IsChoas 
        {
            set 
            {
                driver = value == true ? new Drunker() : new Proxy();
                _isChoas = value;
            }
            get => _isChoas; 
        }
        bool _isChoas;
        public bool IsParalysis
        {
            set
            {
                driver = value == true ? new IdleProxy() : new Proxy();
                _IsParalysis = value;
            }
            get => _IsParalysis;
        }
        bool _IsParalysis;
        public void OnKey(ConsoleKey key)
        {
            switch(driver.OnKey(key))
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("UpArrow");
                    break;
                case ConsoleKey.DownArrow:
                    Console.WriteLine("DownArrow");
                    break;
                case ConsoleKey.LeftArrow:
                    Console.WriteLine("LeftArrow");
                    break;
                case ConsoleKey.RightArrow:
                    Console.WriteLine("RightArrow");
                    break;
            }
        }
    }

    class Drunker : Proxy
    {
        Dictionary<ConsoleKey, ConsoleKey> keys = new Dictionary<ConsoleKey, ConsoleKey>();
        public Drunker()
        {
            List<ConsoleKey> keys = new List<ConsoleKey> { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow };
            var random = new Random();
            var choas = keys.OrderBy(v => random.Next()).ToList();
            for (int i = 0; i < keys.Count; i++)
                this.keys[keys[i]] = choas[i];
        }
        public override ConsoleKey OnKey(ConsoleKey key)
        {
            if (keys.TryGetValue(key, out var newKey))
                return newKey;
            return key;
        }
    }

    class IdleProxy : Proxy
    {
        public override ConsoleKey OnKey(ConsoleKey key)
        {
            return default;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            //var driver = new Proxy();
            //driver = new IdleProxy();

            while (true)
            {
                if(Console.KeyAvailable)
                {
                    // var key = player.OnKey(Console.ReadKey().Key);
                    var k = Console.ReadKey().Key;
                    player.OnKey(k);
                    if (k == ConsoleKey.I)
                        player.IsParalysis = true;
                    else if (k == ConsoleKey.C)
                        player.IsChoas = true;
                }
            }
        }
    }
}
