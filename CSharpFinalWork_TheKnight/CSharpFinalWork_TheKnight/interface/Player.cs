using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 回傳：攻擊力加成 & 減傷效果 & 作用回合數
    /// </summary>
    delegate Tuple<int, int, int> ReleaseSkill();
    internal class Player
    {
        int _power;
        int _endurance;
        int _agile;
        int _sword;
        int _sheild;

        public int Point { get; private set; }
        public int Power { get { return _power; } set { if (DecreacePointWhenPropertyAdd(value)) _power = value; } }
        public int Endurance { get { return _endurance; } set { if (DecreacePointWhenPropertyAdd(value)) _endurance = value; } }
        public int Agile { get { return _agile; } set { if (DecreacePointWhenPropertyAdd(value)) _agile = value; } }
        public int SwordPoint { get { return _sword; } set { if (DecreacePointWhenPropertyAdd(value)) _sword = value; } }
        public int ShieldPoint { get { return _sheild; } set { if (DecreacePointWhenPropertyAdd(value)) _sheild = value; } }

        public Player()
        {
            Point = 100;
        }
        public int Hp { private set; get; }
        public int Stamina { private set; get; }
        public int Speed { private set; get; }
        public AbstractSword Sword { private set; get; }
        public AbstractShield Shield { private set; get; }
        public ReleaseSkill nowSkill { private set; get; }
        public int Move()
        {
            //跳出選單讓玩家選擇技能(回傳陣列字串)
            //注入玩家選擇的技能，並回傳值。



        }
        void SetFightProperty()
        {
            Hp = Power * 3 + Endurance * 5;
            Stamina = Endurance;
            Speed = Stamina;

            //gen sword
            if (SwordPoint > 35 && ShieldPoint <= 0)
                Shield = new SuperHeavyShield();
            else if (SwordPoint > 35)
                Shield = new HeavyShield();
            else if (SwordPoint > 20)
                Shield = new NiceShield();
            else if (ShieldPoint > 0)
                Shield = new NormalShield();
            else
                Shield = new NoShield();

            //gen shield
            if (ShieldPoint > 35 && SwordPoint <= 0)
                Sword = new TwoHandSword();
            else if (ShieldPoint > 35)
                Sword = new NiceSword();
            else if (ShieldPoint > 20)
                Sword = new GoodSword();
            else if (ShieldPoint > 0)
                Sword = new NormalSword();
            else
                Sword = new NoSword();
            //set skill when sword and shield combine.
            Sword.AppendSkillsByShield(Shield);

            //set skill when property arrived.
            if (this.Power >= 35)
            {
                Sword.AppendPlayerSkill("爆裂砍擊", () => Tuple.Create(2, 0, 1));
            }
            else if (this.Agile >= 35)
            {
                Sword.AppendPlayerSkill("暗隱伏擊", () => Tuple.Create(3, 0, 1));
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
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "分配你的點數，以找出擊敗魔像的方法。", $"剩餘點數：{Point}", "請選擇屬性");
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
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "分配你的點數，以找出擊敗魔像的方法。", $"剩餘點數：{Point}", $"已選擇：{s}");
            string input = UiGenerate.RenderOutMenuReadLine("請輸入要加多少點數：");
            int inputPoint = -1;
            if (!int.TryParse(input, out inputPoint))
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "錯誤：輸入的不是數值。");
                UiGenerate.PressAnyKeyToContinue();
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

        private bool DecreacePointWhenPropertyAdd(int val)
        {
            if (val > Point)
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "錯誤：你的點數不足。");
                UiGenerate.PressAnyKeyToContinue();
                return false;
            }
            Point -= val;
            return true;
        }
    }
}
