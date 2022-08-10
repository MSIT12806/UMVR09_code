using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    public class Scene
    {
        public string sceneName { get; set; }
        public bool IsClear
        {
            get
            {
                bool r = true;
                foreach (var item in monsters)
                {
                    r = r && !item.Alive;
                }
                return r;
            }
        }
        public Monster[] monsters { get; set; }
        public void ShowSceneName()
        {
            Console.WriteLine($"您目前在{sceneName}中。");
        }

        public void ShowMonsters()
        {

            foreach (var (item, index) in monsters.WithIndex())
            {
                if (item != null)
                {
                    Console.WriteLine($"編號 {index}：");
                    item.ShowInfo();

                }
            }
        }
    }
}
