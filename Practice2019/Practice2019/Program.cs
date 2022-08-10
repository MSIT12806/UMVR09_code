using System;

namespace Practice02
{
    internal class Program
    {
        /*作業三：
         * 試在Main程式區段中解決以下情境: 企劃設計一個遊戲控制機制
         * 10隻怪物擁有血量(mHp)、最大血量(mMaxHp)、經驗值(mExp)、50%機率掉落炸彈(編號1，不堆疊，對目標造成100傷害)、目前狀態(mState, 0→滿血 1→失血 2→死亡)
         * 玩家擁有力量(pSt)、經驗值(pExp)、包包(pBag 數量上限 10)。
         * 
         * 當玩家按下’0’ 後通過”再次確認”機制後結束遊戲
         * 當玩家按下’1’ 時顯示玩家狀態(力、經驗、包包每格內容)           ===>狀態檢視
         * 當玩家按下’2’ 進入選怪(確認後顯示該怪物資訊)                  ===> 進入戰鬥場景
         * 當按下’3’時，對選擇怪物造成傷害(力量值)
         * 如果選擇怪物此時死亡→玩家經驗值與包包物品依怪物設定增加
         * 如果沒有死亡→顯示攻擊扣傷等字串
         * 當按下’4’時使用包包內炸彈對選擇怪物造成傷害(後續處理同上)
         * 
         * 依據各自遊戲數值區間經驗，自行宣告酷炫屬性名稱並給定屬性值
         * 設計包包、選怪的容錯處理
         * 機率可暫用每打兩隻掉一個取代
         */

        /*作業四：
         * 使用類別改寫作業 3
         * 需使用分檔方式
         * 數值可改用隨機方式或檔案載入方式產生
         * 一樣要注意系統架構
         * 進階  單例 – Singleton （不要Google努力自己猜看看）
         * 遊戲有需要一個獨一無二的資源管理器(GameMaster)，可快速、方便讓所成員函式查找目前遊戲狀態下資運，如怪物清單、玩家清單、關卡資訊等等
         * 使用目前為止所學，設計 GameMaster 類別，使其只能被配置一次、有方便的取得物件方法，未來以應用於上述情境
         */
        class SingletonA
        {
            private static SingletonA instance = null;

            public static SingletonA Instance
            {
                get
                {
                    return instance ?? new SingletonA();
                }
            }

            private SingletonA()
            {
                instance = this;
            }
        }
        static bool doItAgain = true;
        static void Main(string[] args)
        {
            GameManager.GameStart();

            while (doItAgain)
            {
                switch (GameManager.roundState)
                {
                    case GameManager.RoundState.一般:
                        ShowMainMenu();
                        Console.Write("請輸入文字：");
                        ResponseForMainMenu(Console.ReadKey());
                        break;
                    case GameManager.RoundState.戰鬥:
                        FightingMenu();
                        break;
                }
            }
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
