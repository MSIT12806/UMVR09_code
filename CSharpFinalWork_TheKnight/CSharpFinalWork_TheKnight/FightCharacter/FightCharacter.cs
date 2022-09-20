using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 成員有戰鬥基本屬性、戰鬥增益屬性、戰鬥方式、技能列等。
    /// </summary>
    public abstract class FightCharacter
    {
        public string Name = "佚失";
        public FightCharacter()
        {
        }
        //戰鬥前置屬性
        public abstract int FightRoundUnit { get; protected set; }
        public abstract int FightRoundPoint { get; set; }
        public abstract FightCharacter opponent { get; protected set; }
        public abstract Dictionary<string, ReleaseSkill> Skills { get; }
        public void SetOpponent(FightCharacter t)
        {
            opponent = t;
        }

        //戰鬥基本屬性... 基本上不能在戰鬥階段被更動。
        public abstract int Hp { set; get; }
        public abstract float Stamina { set; get; }
        public abstract float Speed { set; get; }
        public abstract Sword Sword { protected set; get; }
        public abstract Shield Shield { protected set; get; }
        public abstract int Attack { set; get; }

        public void 擊暈判定(float incidence)
        {
            Random random = new Random();
            int v = random.Next(0, 100);
            if (v < incidence * 100)
            {
                NowBuffs.Enqueue(("被擊暈", 5, this));
            }
            else { }
        }

        public abstract float 閃避率 { set; get; }
        public abstract float 擊暈率 { set; get; }
        public abstract float 格檔發生率 { set; get; }
        public abstract float 格檔成功率 { set; get; }

        public string GetHurt(int r)
        {
            Random random = new Random();
            int p = random.Next(1, 1000);
            int d = 0;
            if(p< 閃避率 * 1000)
            {
                return $"{this.Name} 閃避了這次攻擊";
            }
            if (Shield != null)
                d = r - Shield.DefensePoint;
            else
                d = r;
            Hp -= d;
            if (d == 0) return "";

            return $"對 {this.Name} 造成 {d} 點 傷害";
        }

        //戰鬥增益屬性，因增益有作用回合數，所以之後要能變回原本狀態
        public float AttackBuff { set; get; }
        public float DefenceBuff { set; get; }
        public float SpeedBuff { set; get; }
        public float 閃避率Buff { set; get; }
        /// <summary>
        /// 擊暈率加乘
        /// </summary>
        public float StunBuff { set; get; }
        /// <summary>
        /// 格擋發生率加乘
        /// </summary>
        public float BlockBuff { set; get; }
        public List<string> nowFightContext = new List<string>();
        internal abstract void SetState(int round);
        internal virtual void ResetState()
        {
            AttackBuff = SpeedBuff = 1;
            閃避率Buff = StunBuff = BlockBuff = 0;
        }
        protected Queue<(string, int, FightCharacter target)> NowBuffs { get; } = new Queue<(string, int, FightCharacter target)>();
        public void SetBuffAndUseSkill()
        {
            int buffNum = NowBuffs.Count;
            while (buffNum != 0)
            {
                (string sName, int round, var t) = NowBuffs.Dequeue();
                Skills[sName](t);
                int r = round - 1;
                if (r > 0)
                    NowBuffs.Enqueue((sName, r, t));
                buffNum--;
            }
        }


    }
}