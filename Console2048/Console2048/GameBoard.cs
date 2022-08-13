using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static void GameStart()
        {
            while (true)
            {
                Round();
                ShowGameBoard();
                Move();
            }

        }

        private static void Round()
        {

            GenerateCube();
            GenerateCube();
        }


        /// <summary>
        /// 操作，每行更動的結果
        /// </summary>
        /// <param name="one">第一個數</param>
        /// <param name="two">第二個數</param>
        /// <param name="three">第三個數</param>
        /// <param name="four">第四個數</param>
        public static void OperatePerLine(ref int one, ref int two, ref int three, ref int four)
        {
            /*
             * 合併
             * 移動
             * 卡住 ..do nothing
             */

            // #1 合併
            if (one == two)
            {
                one += two;
                two = 0;
            }
            else if (two == 0 && one == three)
            {
                one += three;
                three = 0;
            }
            else if (three==0 && one == four)
            {
                one += four;
                four = 0;
            }
            if(two == three)
            {
                two += three;
                three = 0;
            }
            else if(three ==0 && two == four)
            {
                two += four;
                four = 0;
            }
            if(three == four)
            {
                three += four;
                four = 0;
            }

            //#2 移動
            if (one == 0)
            {
                if(two == 0)
                {
                    if(three == 0)
                    {
                        one = four;
                        four = 0;
                    }
                    else
                    {
                        one = three;
                        three = 0;
                    }
                }
                else
                {
                    one = two;
                    two = 0;
                }
            }
            if (two == 0)
            {
                if (three == 0)
                {
                    two = four;
                    four = 0;
                }
                else
                {
                    two = three;
                    three = 0;
                }
            }
            if (three == 0)
            {
                three = four;
                four = 0;
            }


        }
        private static void Move()
        {
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    OperatePerLine(ref board[0][0], ref board[0][1], ref board[0][2], ref board[0][3]);
                    OperatePerLine(ref board[1][0], ref board[1][1], ref board[1][2], ref board[1][3]);
                    OperatePerLine(ref board[2][0], ref board[2][1], ref board[2][2], ref board[2][3]);
                    OperatePerLine(ref board[3][0], ref board[3][1], ref board[3][2], ref board[3][3]);
                    break;
                case ConsoleKey.UpArrow:
                    OperatePerLine(ref board[0][0], ref board[1][0], ref board[2][0], ref board[3][0]);
                    OperatePerLine(ref board[0][1], ref board[1][1], ref board[2][1], ref board[3][1]);
                    OperatePerLine(ref board[0][2], ref board[1][2], ref board[2][2], ref board[3][2]);
                    OperatePerLine(ref board[0][3], ref board[1][3], ref board[2][3], ref board[3][3]);
                    break;
                case ConsoleKey.RightArrow:
                    OperatePerLine(ref board[0][3], ref board[0][2], ref board[0][1], ref board[0][0]);
                    OperatePerLine(ref board[1][3], ref board[1][2], ref board[1][1], ref board[1][0]);
                    OperatePerLine(ref board[2][3], ref board[2][2], ref board[2][1], ref board[2][0]);
                    OperatePerLine(ref board[3][3], ref board[3][2], ref board[3][1], ref board[3][0]);
                    break;
                case ConsoleKey.DownArrow:
                    OperatePerLine(ref board[3][0], ref board[2][0], ref board[1][0], ref board[0][0]);
                    OperatePerLine(ref board[3][1], ref board[2][1], ref board[1][1], ref board[0][1]);
                    OperatePerLine(ref board[3][2], ref board[2][2], ref board[1][2], ref board[0][2]);
                    OperatePerLine(ref board[3][3], ref board[2][3], ref board[1][3], ref board[0][3]);
                    break;
                default:
                    Console.WriteLine("無用的操作");
                    break;
            }
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

        private static void GenerateCube()
        {
            bool fail = true;
            while (fail)
            {
                int col = rd.Next(0, 4);
                int row = rd.Next(0, 4);
                if (board[col][row] == 0)
                {
                    board[col][row] = RandomValue();
                    fail = false;
                }

            }
        }
        private static int RandomValue()
        {
            int value = rd.Next(1, 3);
            return value * 2;
        }

        public static void ShowGameBoard()
        {
            Console.Clear();
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
