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
    internal abstract class FightCharacter
    {
        //戰鬥基本屬性... 基本上不能在戰鬥階段被更動。
        public int Hp { set; get; }
        public float Stamina { set; get; }
        public float Speed { set; get; }
        public Sword Sword { protected set; get; }
        public Shield Shield { protected set; get; }
        public int Attack { set; get; }
        public float 速度 { set; get; }
        public float 命中率 { set; get; }
        public float 閃避率 { set; get; }
        public float 擊暈率 { set; get; }
        public float 格檔發生率 { set; get; }
        public float 格檔成功率 { set; get; }

        //戰鬥前置屬性
        public int FightRoundUnit { get; protected set; }
        public int FightRoundPoint { get; set; }
        public Dictionary<string, ReleaseSkill> Skills = new Dictionary<string, ReleaseSkill>();

        //戰鬥增益屬性，因增益有作用回合數，所以之後要能變回原本狀態
        public float AttackBuff { set; get; }
        public float 速度Buff { set; get; }
        public float 命中率Buff { set; get; }
        public float 閃避率Buff { set; get; }
        public float 擊暈率Buff { set; get; }
        public float 格檔發生率Buff { set; get; }
        public float 格檔成功率Buff { set; get; }
        protected virtual void InitialBuffProperty()
        {
            AttackBuff = 速度Buff = 1;
            命中率Buff = 閃避率Buff = 擊暈率Buff = 格檔成功率Buff = 格檔發生率Buff = 0;
        }
        public ReleaseSkill NowSkill { protected set; get; }
        public ReleaseSkill NextSkill { protected set; get; }
        protected List<(string, int, FightCharacter target)> NowBuffs = new List<(string, int, FightCharacter target)>();
        public void UseSkill()
        {
            foreach ((string sName, int round, var t) in NowBuffs)
            {
                Skills[sName](t);
            }
        }


        internal void GetHurt(int r)
        {
            throw new NotImplementedException();
        }
    }
    internal class Giant : FightCharacter
    {

    }
}