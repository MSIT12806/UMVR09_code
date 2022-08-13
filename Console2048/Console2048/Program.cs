using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard.GameStart();
        }

        /*
         * 專案構想：
         * 模仿遊戲 2048
         * 1. 4*4 格子
         * 2. 隨機出現 2 / 4 的數字
         * 3. ↑↓←→控制全部格子移動，相同數字的會合併，數字會加總成新的格子
         */
    }
}
