using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class AbstractShield
    {
        public string Name { get; private set; }
        public int DefensePoint { get; protected set; }
        public abstract void Defense(int damage);
        public abstract void Collide(int damage);
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
