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
        internal static void GameStart()
        {

        }
        internal static void DistributeState()
        {

        }
        internal static void FightState()
        {

        }
        internal static void DefaultPattern()
        {
            UiHelper.SetConsoleWindowSize(UiHelper.WindowSize.median);
            UiHelper.DrawVerticalLine(0, 90, 37);
            UiHelper.DrawHorizontalLine(0, 25, 140);
            SetCursorPosition(0, 26);
            WriteLine("HIHI1");
            WriteLine("HIHI2");
            WriteLine("HIHI3");
            UiHelper.SetMenu(26, 0,  3);
        }
    }
}
