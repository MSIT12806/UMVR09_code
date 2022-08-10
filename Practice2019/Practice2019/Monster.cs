using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    public class Monster
    {
        public Monster(string name,string description, double hp, double ac, int sPoint, int roundLimit)
        {
            Name = name;
            Description = description;
            Hp = hp;
            Ac = ac;
            StrengthenPoint = sPoint;
            OneHpKeepUsed = false;
            RoundLimit = roundLimit;
        }

        public bool Alive { get { return Hp > 0; } }
        public string Name { get; private set; }
        public string Description { private set; get; }
        public double Hp { get; private set; }
        public double Ac { get; private set; }//防禦力

        public int RoundLimit { get; private set; }
        public int StrengthenPoint { get; private set; }//經驗值

        public void GetHurt(double attack)
        {
            //damage count
            Hp -= attack;
            if (Hp <= 0)
                OneHpKeep();

            //monster dead
            if (this.Alive)
            {
                ShowHp();
            }
            else
            {
                Console.WriteLine("怪物死亡");
            }
        }

        private bool OneHpKeepUsed { get; set; }

        private void OneHpKeep()
        {
            if (!OneHpKeepUsed)
            {
                Console.WriteLine("此怪物還有最後一絲頑強的意志！");
                Hp = Hp <= 0 ? 1 : Hp;
                OneHpKeepUsed = true;
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine(this.Name);
            Console.WriteLine(this.Description);
            Console.WriteLine($"血量：{Hp}");
            Console.WriteLine($"防禦力：{Ac}");
            Console.WriteLine($"經驗值：{StrengthenPoint}");
        }
        public void ShowHp()
        {
            Console.WriteLine("剩餘血量：" + this.Hp);
        }

        public void GetWeek(double point)
        {
            Ac -= point;
        }

    }
}
