using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem
{
    class Subject
    {
        List<IObserver> obs = new List<IObserver>();
        public void Register(IObserver ob)
        {
            Unregister(ob);
            obs.Add(ob);
        }
        public void Unregister(IObserver ob)
        {
            obs.Remove(ob);
        }
        public void OnNotify()
        {
            foreach (var v in obs) v.Update(this);
        }
    }
    interface IObserver
    {
        void Update(Subject target);
    }

    class ChatChannel: Subject
    {
        public string name;
        public string text;
        public void OnTalk(string str)
        {
            text = str;
            OnNotify();
        }
    }

    class Player : IObserver
    {
        public string name;
        public void Update(Subject subject)
        {
            var target = (ChatChannel)subject;
            var text = target.text;
            Console.WriteLine($"Channel[{target.name}]: {text}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ChatChannel cc = new ChatChannel { name = "UMVR9" };
            Player p = new Player { name = "Andrew"};
            Player p2 = new Player { name = "Manager" };
            cc.Register(p);
            cc.Register(p2);
            cc.OnTalk($"{p.name}:Say Hello");
        }
    }
}
