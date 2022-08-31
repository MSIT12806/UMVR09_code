using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Console2048
{

    public static class UiHelper
    {
        /// <summary>
        /// 畫直。
        /// startYPosition：起始高度(min:0)。
        /// xPosition：橫向位置(min:0)。
        /// length： 長度 。
        /// word： 畫的字元(預設:|) 。
        /// </summary>
        public static void DrawVerticalLine(int startYPosition, int xPosition, int length, string word = "|")
        {
            for (int i = 0; i < length; i++)
            {
                SetCursorPosition(xPosition, startYPosition + i);
                WriteLine(word);
            }
        }
        public static void DrawHorizontalLine(int startXPosition, int yPosition, int length, string word = "-")
        {
            SetCursorPosition(startXPosition, yPosition);
            for (int i = 0; i < length; i++)
            {
                Write(word);
            }
        }
        public enum WindowSize
        {
            FullSize,
            large,
            median,
            small
        }
        /// <summary>
        /// 設定黑窗大小(預設=small)
        /// </summary>
        public static void SetConsoleWindowSize(WindowSize ws = WindowSize.small)
        {
            switch (ws)
            {
                case WindowSize.FullSize:
                    SetWindowSize(240, 63);
                    break;
                case WindowSize.large:
                    SetWindowSize(190, 50);
                    break;
                case WindowSize.median:
                    SetWindowSize(140, 37);
                    break;
                case WindowSize.small:
                    SetWindowSize(90, 34);
                    break;
            }
        }

        public static int SetMenu(int y, int x, int row)
        {
            Console.SetCursorPosition(x, y);        //设置光标位置
            int oldtop = y;
            bool tag = true;
            do
            {
                ConsoleKeyInfo info = Console.ReadKey();
                switch (info.Key)
                {
                    case ConsoleKey.Enter:
                        tag = false;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > oldtop && y <= oldtop + row - 1)
                        {
                            y -= 1;
                            Console.Write("\b\b");
                            Console.SetCursorPosition(x, y);
                            Console.Write("⊕");
                        }
                        else
                        {
                            Console.Write("\b\b");
                            Console.SetCursorPosition(x, y);
                            Console.Write("⊕");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (y >= oldtop && y < oldtop + row - 1)
                        {
                            y += 1;
                            Console.Write("\b\b");
                            Console.SetCursorPosition(x, y);
                            Console.Write("⊕");
                            //Console.Write(top);
                        }
                        else
                        {
                            Console.Write("\b\b");
                            Console.SetCursorPosition(x, y);
                            Console.Write("⊕");
                        }
                        break;
                    default:
                        Console.Write("\b\b");
                        Console.SetCursorPosition(x, y);
                        Console.Write("⊕");
                        break;
                }
            } while (tag);
            return y + 1 - oldtop;
        }
    }
}
