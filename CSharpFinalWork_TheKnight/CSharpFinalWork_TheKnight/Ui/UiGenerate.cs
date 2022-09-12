using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Console2048
{
    internal static class UiGenerate
    {

        const int plotX = 0;
        const int plotY = 0;
        const int menuX = 0;
        const int menuY = 26;
        const int stateX = 91;
        const int stateY = 0;
        const int noteX = 91;
        const int noteY = 26;


        public enum WindowSelect
        {
            Plot,
            Menu,
            State,
            Note
        }
        /// <summary>
        /// 畫出內容。 回傳：client 的選擇(or -1) min is 1。
        /// </summary>
        public static int RenderOut(bool clearWindow, WindowSelect window, params string[] lines)
        {
            int result = -1;
            int y = -1, x=-1;
            if (clearWindow)
            {
                Clear();
                DefaultPattern();
            }
            switch (window)
            {
                case WindowSelect.Plot:
                    y = plotY;
                    x = plotX;
                    break;
                case WindowSelect.Menu: //注意
                    y = menuY;
                    x = menuX;
                    result = UiHelper.SetMenu(menuY, menuX, lines);
                    return result;
                case WindowSelect.State:
                    y = stateY;
                    x = stateX;
                    break;
                case WindowSelect.Note:
                    y = noteY;
                    x = noteX;
                    break;
            }
            foreach (var item in lines)
            {
                SetCursorPosition(x, y);
                WriteLine(item);
                y++;
            }
            return result;
        }
        public static int RenderOutEnumMenu<TEnum>() where TEnum : Enum
        {
            int result = RenderOut(false, WindowSelect.Menu, Enum.GetNames(typeof(TEnum)));
            return result;
        }
        public static string RenderOutMenuReadLine(string hint)
        {
            SetCursorPosition(menuX, menuY);
            WriteLine(hint);
            return ReadLine();
        }
        public static void PressAnyKeyToContinue()
        {
            SetCursorPosition(0, 26);
            WriteLine("按下任意鍵繼續");
            ReadKey(true);
        }
        /// <summary>
        /// 畫出本遊戲的框線
        /// </summary>
        private static void DefaultPattern()
        {
            UiHelper.SetConsoleWindowSize(UiHelper.WindowSize.median);
            UiHelper.DrawVerticalLine(0, 90, 37);
            UiHelper.DrawHorizontalLine(0, 25, 140);
        }
    }
}
