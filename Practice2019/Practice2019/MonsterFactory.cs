using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    public static class MonsterFactory
    {
        public static Monster GetSlime()
        {
            return new Monster("史萊姆", "軟軟黏黏的弱小生物", 5, 0, 5, 5);
        }
        public static Monster GetBoar()
        {
            return new Monster("野豬", "野豬騎士來囉", 15, 0, 20, 5);
        }
        public static Monster GetGoblins()
        {
            return new Monster("哥布林", "矮小的人型生物", 50, 0, 100, 5);
        }
    }
}
