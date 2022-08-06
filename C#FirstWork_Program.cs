using System;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //TestMiss();
            GameStart();
        }
        static void GameStart()
        {
            Player p1 = new Player(50, 20, 10, 0);
            Monster monster1 = new Monster(100, 5, 30, 10);
            while (monster1.Alive)
            {
                p1.Attack(monster1);
            }

            Monster monster2 = new Monster(50, 5, 30, 10);
            while (monster2.Alive)
            {
                p1.Attack(monster2);
            }

            Monster monster3 = new Monster(50, 5, 30, 10);
            while (monster3.Alive)
            {
                p1.Attack(monster3);
            }
        }
        static void TestMiss()
        {
            Player p1 = new Player(50, 0, 0, 0);
            Monster monster1 = new Monster(100, 5, 30, 10);
            p1.Attack(monster1);
        }
        class Player
        {
            public Player(double st, double dex, double acc, double exp)
            {
                St = st;
                Dex = dex;
                Acc = acc;
                Exp = exp;
            }

            public double St { get; set; } //力量
            public double Dex { get; set; }//敏捷度
            public double Acc { get; set; }//命中率加乘
            public double Exp { get; set; }//經驗值
            public void Attack(Monster target)
            {
                bool hit = this.Dex / target.Dex + this.Acc > 1.5;
                if (hit)
                {
                    var damage = this.St * 1.4 - target.Ac * 2;
                    Console.WriteLine("命中！傷害：" + damage);
                    target.GetHurt(damage);
                    if (!target.Alive)
                    {
                        GetExperience(target);
                    }
                }
                else
                {
                    Console.WriteLine("未命中！");
                }

            }

            private void GetExperience(Monster target)
            {
                this.Exp += target.Exp;
                Console.WriteLine("獲得經驗值，您目前累積的經驗值為" +
                    "：" + this.Exp);
            }
        }
        class Monster
        {
            public Monster(double hp, double dex, double ac, double exp)
            {
                Hp = hp;
                Dex = dex;
                Ac = ac;
                Exp = exp;
            }

            public bool Alive { get { return Hp > 0; } }
            public double Hp { get; set; }
            public double Dex { get; set; }//敏捷度
            public double Ac { get; set; }//防禦力
            public double Exp { get; set; }//經驗值

            public void GetHurt(double attack)
            {
                Hp -= attack;
                if (this.Alive)
                {
                    ShowHp();
                }
                else
                {
                    Console.WriteLine("怪物死亡");
                }
            }

            public void ShowHp()
            {
                Console.WriteLine("剩餘血量：" + this.Hp);
            }
        }
    }
}
