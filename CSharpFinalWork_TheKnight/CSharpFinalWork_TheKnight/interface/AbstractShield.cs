using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    public abstract class Shield
    {
        protected Dictionary<string, ReleaseSkill> Skills = new Dictionary<string, ReleaseSkill>();
        protected Player _player;
        public Shield(Player player)
        {
            _player = player;
        }

        public abstract string Name { get; }
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
        public virtual (int damage, int rounds) Attack(FightCharacter target)
        {
            target.擊暈判定(_player.擊暈率 + _player.擊暈率Buff);
            return ((int)((_player.Attack + DefensePoint * 0.5f) * _player.AttackBuff), 0);
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
            player.擊暈率 *= 1.2f;
            Skills.Add("超重量盾擊", t =>
            {
                _player.Stamina -= 1;
                return Attack(t);
            });
        }

        public override string Name => "超重盾";


    }
    internal class HeavyShield : Shield
    {
        public override string Name => "厚重盾";
        public HeavyShield(Player player) : base(player)
        {
            DefensePoint = 30;
            Skills.Add("重盾擊", Attack);
        }

    }
    internal class NiceShield : Shield
    {
        public override string Name => "精良盾";
        public NiceShield(Player player) : base(player)
        {
            DefensePoint = 20;
            Skills.Add("良盾擊", Attack);
        }

    }
    internal class NormalShield : Shield
    {
        public override string Name => "盾";
        public NormalShield(Player player) : base(player)
        {
            DefensePoint = 10;
            Skills.Add("盾擊", Attack);
        }

    }
    internal class NoShield : Shield
    {
        public override string Name => "無盾";
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
