using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promise
{
    class Promise
    {
        List<Func<object, bool>> thens = new List<Func<object, bool>>();
        Action<Promise> done { set; get; }
        public Promise Done(Action<Promise> action)
        {
            done = action;
            return this;
        }

        Action reject { set; get; }
        public Promise Reject(Action action)
        {
            reject = action;
            return this;
        }

        public Promise Then(Func<object, bool> then)
        {
            thens.Add(then);
            return this;
        }

        public void Invoke()
        {
            bool succ = true;
            foreach(var v in thens)
            {
                succ = v.Invoke(null);
                if (succ == false)
                {
                    reject?.Invoke();
                    break;
                }
            }
            done?.Invoke(this);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var prm = new Promise()
                .Reject(() => Console.WriteLine("Error"))
                .Done(_ => Console.WriteLine("Final"))
                .Then(_ => { Console.WriteLine("start"); return true; })
                .Then(_ => { Console.WriteLine("1"); return true; })
                .Then(_ => { Console.WriteLine("2"); return false; })
                .Then(_ => { Console.WriteLine("3"); return true; })
                .Then(_ => { Console.WriteLine("4"); return true; })
                .Then(_ => { Console.WriteLine("5"); return true; })
                .Then(_ => { Console.WriteLine("6"); return true; });

            prm.Invoke();
        }
    }
}
