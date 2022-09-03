using Console2048;
using System;

namespace CSharpFinalWork_TheKnight
{
    /// <summary>
    /// 生成遊戲用的物件、管理遊戲流程等。
    /// </summary>
    internal class GameManager
    {
        Player Player { get; set; }
        public GameManager()
        {
            Player = SetPlayerState();
        }
        internal void GameStart()
        {
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "是否觀看劇情？");
            if (UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Menu, "是", "否") == 1)
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "??：", "勇者，你費盡千辛萬苦終於到了這個地方，", "眼前矗立著最後的難關──一個巨大的魔像，", "打敗他以後，你將成功的拯救公主，並與她共度美好的餘生。");
                UiGenerate.PressAnyKeyToContinue();
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "??：", "你一路走來累積的經歷和收穫，在此刻變化成 100個分配點數，好好分配它，你將有機會打敗眼前的魔像，往你嚮往的未來前進。");
                UiGenerate.PressAnyKeyToContinue();
            }

        }
        internal Player SetPlayerState()
        {
            //產生一個Player 物件
            var player = new Player();
            //讓 Client 分配點數
            DistributeState(player);
            //依照點數產生相對應的裝備 & 技能
            //回傳 Player 物件
            return null;
        }
        private void DistributeState(Player player)
        {
            void SetPlayerProperty()
            {
                player.ShowDistribute();
                player.ShowState();
                int s = UiGenerate.RenderOutEnumMenu<Player.PlayerBasicProperty>();
                Player.Distribute((Player.PlayerBasicProperty)s);
            }

            while (true)
            {
                if (player.Point < 10)
                { //可選擇直接開打
                    break;
                }
                SetPlayerProperty();

            }

        }
        internal bool FightState()
        {
            //排定戰鬥順序
            ////輪到魔像的基本AI
            ////輪到玩家的攻擊選擇
            int playerAttackPoint = Player.Move();
            return false;
        }
    }
}