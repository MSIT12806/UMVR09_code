using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    /// <summary>
    /// 遊戲道具
    /// </summary>
    public abstract class Tool
    {
        public string name { get; set; }
        public string description { get; set; }
        public double effectPoint {get;set;}
        abstract public void Effect(Player player);
        abstract public void Effect(Monster monster);
    }

    public class SoftenPotion : Tool
    {
        public SoftenPotion()
        {
            effectPoint = 10;
            name = "軟化藥水";
            description = $"此藥水可以減少怪物的防禦力 {effectPoint}點。";
        }
        public override void Effect(Player player)
        {
            throw new NotImplementedException();
        }

        public override void Effect(Monster monster)
        {
            monster.GetWeek(effectPoint);
        }
    }
}
