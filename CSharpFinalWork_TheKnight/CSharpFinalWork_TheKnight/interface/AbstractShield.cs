using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class AbstractShield
    {
        public Dictionary<string, ReleaseSkill> Skills;
        protected Player _player;
        public AbstractShield(Player player)
        {
            _player = player;
        }

        public string Name { get; private set; }
        public int DefensePoint { get; protected set; }
        public abstract void Defense(int damage);
        public abstract void Collide(int damage);


        /// <summary>
        /// 不想讓外部改到這個，所以回傳一個新的。
        /// </summary>
        public Dictionary<string, ReleaseSkill> GetSkills()
        {
            return new Dictionary<string, ReleaseSkill>(Skills);
        }
    }
    internal class SuperHeavyShield : AbstractShield
    {
        public override void Collide(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Defense(int damage)
        {
            throw new NotImplementedException();
        }
    }
    internal class HeavyShield : AbstractShield
    {
        public override void Collide(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Defense(int damage)
        {
            throw new NotImplementedException();
        }
    }
    internal class NiceShield : AbstractShield
    {
        public override void Collide(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Defense(int damage)
        {
            throw new NotImplementedException();
        }
    }
    internal class NormalShield : AbstractShield
    {
        public override void Collide(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Defense(int damage)
        {
            throw new NotImplementedException();
        }
    }
    internal class NoShield : AbstractShield
    {
        public override void Collide(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Defense(int damage)
        {
            throw new NotImplementedException();
        }
    }
}
