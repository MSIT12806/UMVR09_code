using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    public class Player
    {
        public Player(double st, double dex)
        {
            St = st;
            Dex = dex;
        }
        public string name { get; set; }
        public double St { get; set; } //攻擊力
        public double Dex { get; set; }//敏捷度

        public void ShowState()
        {
            Console.WriteLine("目前狀態：");
            Console.WriteLine($"攻擊力：{St}");
            Console.WriteLine($"敏捷度：{Dex}");
            Program.Pause();
        }
        public void Attack(Monster target)
        {
            var damage = this.St - target.Ac;
            if (damage > 0)
            {
                Console.WriteLine("命中！傷害：" + damage);
                target.GetHurt(damage);
                if (!target.Alive)
                {
                    GetStrengthPoint(target);
                }
            }
            else
            {
                Console.WriteLine("你無法傷害它！");
            }

        }

        private void GetStrengthPoint(Monster target)
        {
            Console.WriteLine($"獲得強化點數 {target.StrengthenPoint}點。");
        }

        //包包

        private Tool[] bag = new Tool[10];

        public void GetTool()
        {
            for (int i = 0; i < bag.Length; i++)
            {
                if (bag[i] == null)
                {
                    bag[i] = new SoftenPotion();
                    return;
                }
            }
        }

        public void UseTool(Monster target)
        {
            int toolIndex = -1;
            ShowBagInfo();
            Console.WriteLine("輸入編號以選擇道具，或按 q 以離開。");
            var input = Console.ReadKey().KeyChar.ToString();
            if (int.TryParse(input, out toolIndex))
            {
                Tool tool = bag[toolIndex];
                tool.Effect(target);
            }
            else if (input.ToLower() == "q")
            {
                return;
            }
        }

        public void ShowBagInfo()
        {
            foreach (var (item, index) in bag.WithIndex())
            {
                if (item != null)
                    Console.WriteLine($"編號 {index}：{item.name}");
            }
        }
    }
}
