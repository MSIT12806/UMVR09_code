using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaBasic
{
    class Mob
    {
        public int def;
        public int hp;
        public void PlayDead()
        {

        }
    }

    class Player
    {
        public int att;
        public int mp;
        public void Attack(Mob b)
        {
            // var c = GameFlow.mobs.Count;
            if(b.hp<=0)
            {
                // b.PlayDead();
                Attack();
            }
        }

        public void Attack ()
        {

        }

        public void Show(Player chr)
        {
            // chr = loadNpcData();
            // chr.hp;
        }

        public void UpdateHp()
        {
            // update hp each frame
            if (true) 
            {
                mp += mp*0.1;
            }
        }

        public void AI()
        {
            if ()
            {

            }
        }
    }

    class GameFlow
    {
        public static List<Mob> mobs = new List<Mob>();
        Player p = null;
        public void DoFlow()
        {
            byte[] msg = new byte[4];
            int v = BitConverter.ToInt32(msg, 0);

            if(true)
            {
                var m = mobs.Find(v => v.hp == 0);
                p.Attack(m);
                m.PlayDead();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
