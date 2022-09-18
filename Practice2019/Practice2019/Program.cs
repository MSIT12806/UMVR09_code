using System;

namespace Practice02
{
    internal class Program
    {
        static bool doItAgain = true;
        static void Main(string[] args)
        {
            
        }
        public static void Pause()
        {
            Console.WriteLine($"按下任意按繼續");
            Console.ReadKey();
        }

        private static void ResponseForMainMenu(ConsoleKeyInfo consoleKeyInfo)
        {
            Console.WriteLine("\n=============");
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.D0:
                    Console.WriteLine("您確定要離開遊戲？(y / n)");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        doItAgain = false;
                    }
                    return;
                case ConsoleKey.D1:
                    GameManager.player.ShowState();
                    return;
                case ConsoleKey.D2:
                    GameManager.roundState = GameManager.RoundState.戰鬥;
                    return;
                case ConsoleKey.D9:
                    ShowMainMenu();
                    return;
                default:
                    Console.WriteLine("無效的輸入");
                    return;
            }
        }

        private static void FightingMenu()
        {
            if (!GameManager.scene.IsClear)
            {
                GameManager.scene.ShowMonsters();
                Console.WriteLine("輸入怪物編號以進行戰鬥或是按下 q 離開戰鬥。");
                var input = Console.ReadKey();
                ResponseForFightingMenu(input);
            }
            else
            {
                Console.WriteLine("此地區怪物已被清空，請選擇新地區。");
                Console.WriteLine("1. 新手村。2. 歌唱之島。3. 鬼島。");
                var input = -1;
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out input))
                {
                    GameManager.ChangeScene((GameManager.SceneNumber)input);
                }
                else
                {
                    CanNotReadKey();
                }
            }

        }
        private static void CanNotReadKey()
        {
            Console.WriteLine("無效的輸入。");
        }

        private static void ResponseForFightingMenu(ConsoleKeyInfo input)
        {
            int inputValue = -1;
            if (int.TryParse(input.KeyChar.ToString(), out inputValue))
            {
                if (inputValue > 0 || inputValue < GameManager.scene.monsters.Length)
                    FightProcess(GameManager.scene.monsters[inputValue]);
                else
                    CanNotReadKey();
            }
            else if (input.Key == ConsoleKey.Q)
            {
                LeaveFight();
            }
        }

        private static void LeaveFight()
        {
            Console.Clear();
            Console.WriteLine("結束戰鬥。");
            GameManager.roundState = GameManager.RoundState.一般;
        }
        private static void FightProcess(Monster target)
        {
            if (target.Alive)
            {
                Console.Clear();
                Console.WriteLine("=============");
                Console.WriteLine("進入戰鬥階段");
                target.ShowInfo();
                while (GameManager.roundState == GameManager.RoundState.戰鬥)
                {
                    if (target.Alive) ChooseFightWay(target);
                    else
                    {
                        GameManager.player.GetTool();
                        return;
                    }
                }
            }

        }

        private static void ChooseFightWay(Monster target)
        {
            Console.WriteLine("選擇戰鬥方式：1. 普攻。2. 道具。q. 離開戰鬥。");
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    GameManager.player.Attack(target);
                    break;
                case ConsoleKey.D2:
                    GameManager.player.UseTool(target);
                    break;
                case ConsoleKey.Q:
                    LeaveFight();
                    return;
            }

        }


        private static void ShowMainMenu()
        {
            Console.WriteLine("按下 0 離開遊戲。");
            Console.WriteLine("按下 1 查看目前狀態。");
            Console.WriteLine("按下 2 進入戰鬥。");
            Console.WriteLine("按下 9 重新說明。");
        }
    }
}
