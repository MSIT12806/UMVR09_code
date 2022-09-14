using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    public class Giant : FightCharacter
    {
        public Giant()
        {
            Skills.Add("普攻", p =>
            {
                return ((int)(p.Attack * AttackBuff), 0);
            });
            Skills.Add("衝撞", p =>
            {
                return ((int)(p.Attack * AttackBuff), 0);
            });

            Name = "魔像";

        }

        public override int FightRoundUnit { get; protected set; }
        public override int FightRoundPoint { get; set; }
        public override FightCharacter opponent { get; protected set; }

        public override Dictionary<string, ReleaseSkill> Skills { get; } = new Dictionary<string, ReleaseSkill>();
        public override Sword Sword { get; protected set; }
        public override Shield Shield { get; protected set; }
        public override int Hp { get; set; }
        public override float Stamina { get; set; }
        public override int Attack { get; set; }
        public override float Speed { get; set; }
        public override float 速度 { get; set; }
        public override float 命中率 { get; set; }
        public override float 閃避率 { get; set; }
        public override float 擊暈率 { get; set; }
        public override float 格檔發生率 { get; set; }
        public override float 格檔成功率 { get; set; }

        internal void RandomSkill()
        {
            Random random = new Random();
            int skillIdx = random.Next(0, Skills.Count - 1);
            var skill = Skills[Skills.Keys.ToArray()[skillIdx]];


            nowFightContext.Add($"{this.Name} 對 {opponent.Name} 施展 {Skills.Keys.ToArray()[skillIdx]}");

            (int d, int r) = skill(opponent);
            if (r > 0)
                NowBuffs.Enqueue((Skills.Keys.ToArray()[skillIdx], r - 1, opponent));
            opponent.GetHurt(d);

            if (d > 0)
                nowFightContext.Add($" 對 {opponent.Name} 造成 {d} 點 傷害");
        }


        internal override void SetState(int round)
        {
            float level = 50 * ((11 - (float)round) / 10);
            Hp = (int)level * 20;

            FightRoundUnit = (int)(1000 / level + 1);
            this.Attack = (int)level;
            this.速度 = Speed = 1000 / level;
            this.命中率 = level / 1000;
            this.閃避率 = level / 1000;
            this.格檔成功率 = level / 1000;
            this.擊暈率 = level / 1000;
            this.格檔發生率 = level / 1000;
        
        }

        internal void ShowState()
        {
            List<string> builder = new List<string>();
            builder.Add($"魔像狀態：");
            builder.Add($"Hp:{Hp}");
            builder.Add($"力竭:{Stamina}");
            builder.Add($"速度:{Speed}");
            builder.Add($"攻擊力：{Attack}");
            builder.Add($"命中率：{命中率}");
            builder.Add($"閃避率：{閃避率}");
            builder.Add($"擊暈率：{擊暈率}");
            builder.Add($"格檔發生率：{格檔發生率}");
            builder.Add($"格檔成功率：{格檔成功率}");
            builder.Add("");
            builder.Add($"狀態：");
            builder.AddRange(NowBuffs.Select(i => i.Item1).ToArray());
            UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Note, builder.ToArray());
        }
    }

}
