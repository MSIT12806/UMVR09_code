using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Mob
    {
        public int type;
        public string name;
        public int mp;
        public int hp;
        public int ap;
    }

    class SlimBuilder
    {
        Mob mob = new Mob { type = 1, name = "Slime" };
        public SlimBuilder SetMp(int mp)
        {
            mob.mp = mp;
            return this;
        }
        public SlimBuilder SetAp(int ap)
        {
            mob.ap = ap;
            return this;
        }
        public Mob Build()
        {
            return mob;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mob = new Mob();
            mob.type = 1;
            mob.name = "";
            var slime = new SlimBuilder()
                .SetMp(10)
                .SetAp(20)
                .Build();
        }
    }
}
