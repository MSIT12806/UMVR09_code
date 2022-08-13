using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    /// <summary>
    /// 2048 的棋盤
    /// </summary>
    public static class GameBoard
    {
        public static List<int[]> board = new List<int[]> { new int[4], new int[4], new int[4], new int[4] };

        static Random rd = new Random();

        public static void CombineLeft()
        {
            /*
             * 合併
             * 移動
             * 卡住
             */
            for (int i = 0; i < 4 - 1; i++)
            {
                if (board[0][i] == 0)
                {

                }
                else if (board[0][i] == board[0][i + 1]) //合併
                    board[0][i] += board[0][i + 1];
                else if (board[0][i] != board[0][i + 1])//卡住
                { 


                } 

            }
        }
        private static void Move()
        {

        }
        public static void CombineRight()
        {
            //board[0]
            //board[1]
            //board[2]
            //board[3]
        }
        public static void CombineUp()
        {

        }
        public static void CombineDown()
        {

        }

        public static int GenerateRandomCubeValue()
        {
            int r = rd.Next(1, 3);
            return r * 2;
        }

        public static void ShowGameBoard()
        {
            //Console.Clear();
            SepreadBlock();
            Console.WriteLine("-----------------------------");
            PrintLine(board[0]);
            PrintLine(board[1]);
            PrintLine(board[2]);
            PrintLine(board[3]);
            SepreadBlock();
        }
        public static void PrintLine(int[] line)
        {

            Console.WriteLine("|      |      |      |      |");
            Console.WriteLine(string.Format("| {0,4} | {1,4} | {2,4} | {3,4} |", line[0].Show(), line[1].Show(), line[2].Show(), line[3].Show()));
            Console.WriteLine("|      |      |      |      |");
            Console.WriteLine("-----------------------------");
        }

        public static void SepreadBlock()
        {
            Console.WriteLine("====================");
        }

        public static string Show(this int val)
        {
            if (val == 0)
            {
                return "";
            }
            return val.ToString();
        }
    }

}
