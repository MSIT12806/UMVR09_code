using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 回傳：攻擊 
    /// </summary>
    delegate (int damage, int rounds) ReleaseSkill(AbstractCharacter target);
    internal class Player : AbstractCharacter
    {
        int _power;
        int _endurance;
        int _agile;
        int _sword;
        int _sheild;

        public int OriginPoint { get; private set; }
        public int Power { get { return _power; } set { if (DecreacePointWhenPropertyAdd(value - _power)) _power = value; } }
        public int Endurance { get { return _endurance; } set { if (DecreacePointWhenPropertyAdd(value - _endurance)) _endurance = value; } }
        public int Agile { get { return _agile; } set { if (DecreacePointWhenPropertyAdd(value - _agile)) _agile = value; } }
        public int SwordPoint { get { return _sword; } set { if (DecreacePointWhenPropertyAdd(value - _sword)) _sword = value; } }
        public int ShieldPoint { get { return _sheild; } set { if (DecreacePointWhenPropertyAdd(value - _sheild)) _sheild = value; } }

        public Player()
        {
            OriginPoint = 100;
        }

        public void Move(AbstractCharacter t, string v)
        {

            //注入玩家選擇的技能，並回傳值。
            (var d, var r) = NowSkill(t);
            if (r > 0)
                NowBuffs.Add((v, r, t));


            t.GetHurt(d);

        }

        internal void ResetState()
        {
            this.AttackBuff = 1;
            this.Attack = Power + Sword.AttackPoint;
            this.速度 = 1000 / Agile;
            this.命中率 = Agile / 1000;
            this.閃避率 =  Agile / 1000;
            this.格檔成功率 = Power / 1000;
            this.擊暈率 = Power / 1000;
            this.格檔發生率 =   Agile / 1000;
        }

        internal void SetFightProperty()
        {
            Hp = Power * 3 + Endurance * 5;
            Stamina = Endurance;
            Speed = Stamina;

            //gen sword
            if (SwordPoint > 35 && ShieldPoint <= 0)
                Shield = new SuperHeavyShield(this);
            else if (SwordPoint > 35)
                Shield = new HeavyShield(this);
            else if (SwordPoint > 20)
                Shield = new NiceShield(this);
            else if (ShieldPoint > 0)
                Shield = new NormalShield(this);
            else
                Shield = new NoShield(this);

            //gen shield
            if (ShieldPoint > 35 && SwordPoint <= 0)
                Sword = new TwoHandSword(this);
            else if (ShieldPoint > 35)
                Sword = new NiceSword(this);
            else if (ShieldPoint > 20)
                Sword = new GoodSword(this);
            else if (ShieldPoint > 0)
                Sword = new NormalSword(this);
            else
                Sword = new NoSword(this);

            this.GetSkills();


        }

        private void GetSkills()
        {
            foreach (var item in Shield.GetSkills())
            {
                if (!Skills.ContainsKey(item.Key))
                    Skills.Add(item.Key, item.Value);
                else
                    throw new Exception("技能名稱出現重複");
            }
            foreach (var item in Sword.GetSkills())
            {
                if (!Skills.ContainsKey(item.Key))
                    Skills.Add(item.Key, item.Value);
                else
                    throw new Exception("技能名稱出現重複");
            }            //set skill when property arrived.
            if (this.Power >= 35)
            {
                Skills.Add("勢如破竹", p =>
                {
                    p.Stamina -= 1;
                    p.AttackBuff += 0.2f;
                    return (0, 5);
                });
            }
            else if (this.Endurance >= 35)
            {
                Skills.Add("血牛爆發", p =>
                {
                    p.Stamina -= 1;
                    p.Stamina *= 0.75f;
                    Attack = (int)(p.Attack * 1.5f);
                    return (0, 0);
                });
            }
            else if (this.Agile >= 35)
            {
                Skills.Add("暗隱伏擊", p =>{
                    p.Stamina -= 1;
                    p.擊暈率 *= 3;
                    return (0, 3);
                });
            }
        }
        public enum PlayerBasicProperty
        {
            Power,
            Endurance,
            Agile,
            Sword,
            Sheild,
        }

        internal void ShowDistribute()
        {
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "分配你的點數，以找出擊敗魔像的方法。", $"剩餘點數：{OriginPoint}", "請選擇屬性");
        }

        internal void ShowState()
        {
            UiGenerate.RenderOut(false, UiGenerate.WindowSelect.State,
                $"基本數值：",
                $"力量：{Power}",
                $"耐力：{Endurance}",
                $"敏捷：{Agile}",
                $"劍：{Sword}",
                $"盾：{ShieldPoint}",
                "",
                $"戰鬥數值："

                );
        }

        public void Distribute(PlayerBasicProperty s)
        {
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "分配你的點數，以找出擊敗魔像的方法。", $"剩餘點數：{OriginPoint}", $"已選擇：{s}");
            string input = UiGenerate.RenderOutMenuReadLine("請輸入要加多少點數：");
            int inputPoint = -1;
            if (!int.TryParse(input, out inputPoint))
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "錯誤：輸入的不是數值。");
                UiGenerate.PressAnyKeyToContinue();
                return;
            }

            switch (s)
            {
                case PlayerBasicProperty.Power:
                    Power += inputPoint;
                    break;
                case PlayerBasicProperty.Endurance:
                    Endurance += inputPoint;
                    break;
                case PlayerBasicProperty.Agile:
                    Agile += inputPoint;
                    break;
                case PlayerBasicProperty.Sword:
                    SwordPoint += inputPoint;
                    break;
                case PlayerBasicProperty.Sheild:
                    ShieldPoint += inputPoint;
                    break;
                default:
                    break;
            }
        }
        internal void DistributeProperty()
        {

            while (true)
            {

                if (OriginPoint < 10)
                {
                    ShowState();
                    UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "是否進入戰鬥？");
                    if (UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Menu, "是", "否") == 0)
                        return;
                }
                ShowDistribute();
                int s = UiGenerate.RenderOutEnumMenu<Player.PlayerBasicProperty>();
                Distribute((Player.PlayerBasicProperty)s);

            }

        }
        /// <summary>
        /// return: 點數是否足夠
        /// </summary>
        private bool DecreacePointWhenPropertyAdd(int val)
        {
            if (val > OriginPoint)
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "錯誤：你的點數不足。");
                UiGenerate.PressAnyKeyToContinue();
                return false;
            }
            OriginPoint -= val;
            return true;
        }
    }
}
