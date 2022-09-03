using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class AbstractSword
    {
        protected Player _player;
        public AbstractSword(Player player)
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
    }
    internal class NoSword : AbstractSword
    {
        public NoSword(Player player):base(player)
        {
            AttackPoint = 0;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("徒手毆打", HitByHand);
        }

        protected override IEnumerable<string> AppendSkillsByShield()
        {
            AbstractShield shield = _player.Shield;
            if (shield.GetType().Name == typeof(SuperHeavyShield).Name)
            {
                Skills.Add("堅若磐石", SameAsRock);
                Skills.Add("重如泰山", HeavyAsMountain);
            }

            return Skills.Keys;
        }
        /// <summary>
        /// 徒手毆打
        /// </summary>
        internal (float, int, int) HitByHand()
        {
            return (1, 0, 1);
        }
        /// <summary>
        /// 堅若磐石
        /// </summary>
        internal (float, int, int) SameAsRock()
        {
            return (0, 50, 1);
        }
        /// <summary>
        /// 重如泰山：持續五回合，每次傷害減免。
        /// </summary>
        internal (float, int, int) HeavyAsMountain()
        {
            return (0, 10, 5);
        }
    }
    internal class NormalSword : AbstractSword
    {
        public NormalSword(Player player) : base(player)
        {
            AttackPoint = 15;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("普通砍擊", ()=> (1, 0, 1));
        }
    }
    internal class GoodSword : AbstractSword
    {
        public GoodSword(Player player) : base(player)
        {
            AttackPoint = 18;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("好的砍擊", () => (1.1f, 0, 1));
        }
    }
    internal class NiceSword : AbstractSword
    {
        public NiceSword(Player player) : base(player)
        {
            AttackPoint = 23;
            Skills = new Dictionary<string, ReleaseSkill>();
            Skills.Add("優良砍擊", () => (1.1f, 0, 1));
        }
    }
    internal class TwoHandSword : AbstractSword
    {
        public TwoHandSword(Player player) : base(player)
        {
            AttackPoint = 33;
            Skills = new Dictionary<string, ReleaseSkill>();
        }
    }
}
