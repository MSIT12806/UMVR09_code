using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSaveLoadSystem
{
    interface IMemento
    {
    }

    class Player
    {
        public int hp;
        public int id;
        public IMemento OnSave()
        {
            return new PlayerData
            {
                id = id,
                hp = hp,
                saveTime = DateTime.Now
            };
        }
        public void OnLoad(IMemento mem)
        {
            if (mem is PlayerData pd)
            {
                id = pd.id;
                hp = pd.hp;
            }
        }
    }

    class PlayerData : IMemento
    {
        public int id;
        public int hp;
        public DateTime saveTime;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player p = new Player();
            var mem = p.OnSave();
            p.OnLoad(mem);
        }
    }
}
