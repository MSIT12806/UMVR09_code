using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class Sword
    {
        protected Player _player;
        public Sword(Player player)
        {
            _player = player;
        }
        public string Name { get; private set; }
        public int AttackPoint { get; protected set; }

        protected Dictionary<string, ReleaseSkill> Skills;

        /// <summary>
        /// 不想讓外部改到這個，所以回傳一個新的。
        /// </summary>
        public Dictionary<string, ReleaseSkill> GetSkills()
        {
            return new Dictionary<string, ReleaseSkill>(Skills);
        }

        protected virtual IEnumerable<string> AppendSkillsByShield()
        {
            return Skills.Keys;
        }

        protected (int damage, int rounds) Attack(FightCharacter p)
        {
            p.Stamina -= 1;
            return (p.Attack + AttackPoint, 0);
        }
    }
    internal class NoSword : Sword
    {
        public NoSword(Player player) : base(player)
        {
            AttackPoint = 0;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("徒手毆打", Attack);
        }

        protected override IEnumerable<string> AppendSkillsByShield()
        {
            Shield shield = _player.Shield;
            if (shield.GetType().Name == typeof(SuperHeavyShield).Name)
            {
                Skills.Add("固若金湯", p =>
                {
                    p.Stamina -= 2;//雙手武器技能耐力值消耗*2
                    p.格檔成功率 *= 10;
                    return (0, 5);
                });
                Skills.Add("天崩地裂", p =>
                {
                    p.Stamina -= 5;
                    p.格檔成功率 = 1;
                    return (p.Attack, 0);
                });
            }

            return Skills.Keys;
        }
    }
    internal class NormalSword : Sword
    {
        public NormalSword(Player player) : base(player)
        {
            AttackPoint = 10;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("普通砍擊", Attack);
        }
    }
    internal class GoodSword : Sword
    {
        public GoodSword(Player player) : base(player)
        {
            AttackPoint = 18;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("好的砍擊", Attack);
        }
    }
    internal class NiceSword : Sword
    {
        public NiceSword(Player player) : base(player)
        {
            AttackPoint = 28;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("優良砍擊", Attack);
        }
    }
    internal class TwoHandSword : Sword
    {
        public TwoHandSword(Player player) : base(player)
        {
            AttackPoint = 40;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("優良砍擊", Attack);
        }
    }
}
