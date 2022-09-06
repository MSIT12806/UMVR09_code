using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    internal abstract class AbstractCharacter
    {
        public ReleaseSkill NowSkill { protected set; get; }
        public ReleaseSkill NextSkill { protected set; get; }

        public int Hp {  set; get; }
        public float Stamina {  set; get; }
        public float Speed {  set; get; }
        public AbstractSword Sword { protected set; get; }
        public AbstractShield Shield { protected set; get; }

        public Dictionary<string, ReleaseSkill> Skills = new Dictionary<string, ReleaseSkill>();
        protected List<(string, int, AbstractCharacter target)> NowBuffs = new List<(string, int, AbstractCharacter target)> ();
        public void UseSkill()
        {
            foreach ((string sName, int round, var t) in NowBuffs)
            {
                Skills[sName](t);
            }
        }

        public float AttackBuff { set; get; }
        public int Attack { set; get; }
        public float 速度 { set; get; }
        public float 命中率 { set; get; }
        public float 閃避率 { set; get; }
        public float 擊暈率 { set; get; }
        public float 格檔發生率 { set; get; }
        public float 格檔成功率 { set; get; }

        internal void GetHurt(int r)
        {
            throw new NotImplementedException();
        }
    }
    internal class Giant : AbstractCharacter
    {

    }
}