using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobAISystem
{
    class Player
    {
        public int id;
        public int hp;
    }

    class Mob
    {
        public void OnSelectPlayer(Func<Player, bool> visitor)
        {
            List<Player> fits = new List<Player>();
            PlayerManager.OnVisit(visitor);
            //
        }
    }

    static class PlayerManager
    {
        static List<Player> players = new List<Player>();
        static public void OnVisit(Func<Player, bool> visitor)
        {
            foreach(var p in players)
            {
                if (visitor.Invoke(p) == false) break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mob = new Mob();
            mob.OnSelectPlayer(p =>
            {
                if (p.hp < 10)
                {
                    fits.Add(p);
                    return false;
                }
                return true;
            });
        }
    }
}
