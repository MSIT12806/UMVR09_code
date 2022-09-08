using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class Shield
    {
        private Dictionary<string, ReleaseSkill> Skills = new Dictionary<string, ReleaseSkill>();
        protected Player _player;
        public Shield(Player player)
        {
            _player = player;
        }

        public string Name { get; private set; }
        public int DefensePoint { get; protected set; }
        public virtual int Defense(int damage)
        {

            Random r = new Random();
            int cPoint = r.Next(0, 100);
            if (cPoint > _player.格檔發生率)
            {
                return damage - _player.Power;
            }
            return damage;
        }
        public virtual int Collide(int damage)
        {
            return damage - DefensePoint;
        }


        /// <summary>
        /// 不想讓外部改到這個，所以回傳一個新的。
        /// </summary>
        public Dictionary<string, ReleaseSkill> GetSkills()
        {
            return new Dictionary<string, ReleaseSkill>(Skills);
        }
    }
    internal class SuperHeavyShield : Shield
    {
        public SuperHeavyShield(Player player) : base(player)
        {
            DefensePoint = 50;
        }

    }
    internal class HeavyShield : Shield
    {
        public HeavyShield(Player player) : base(player)
        {
            DefensePoint = 30;
        }

    }
    internal class NiceShield : Shield
    {
        public NiceShield(Player player) : base(player)
        {
            DefensePoint = 20;
        }

    }
    internal class NormalShield : Shield
    {
        public NormalShield(Player player) : base(player)
        {
            DefensePoint = 10;
        }

    }
    internal class NoShield : Shield
    {
        public NoShield(Player player) : base(player)
        {
            DefensePoint = 0;
        }
        public override int Collide(int damage)
        {
            return damage;
        }
    }
}
