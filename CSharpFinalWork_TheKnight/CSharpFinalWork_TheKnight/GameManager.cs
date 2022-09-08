﻿using Console2048;
using System;
using System.Linq;

namespace CSharpFinalWork_TheKnight
{
    /// <summary>
    /// 生成遊戲用的物件、管理遊戲流程等。
    /// </summary>
    internal class GameManager
    {
        Player Player { get; set; }
        Giant Giant { get; set; }
        public int Round { get; set; }
        public GameManager()
        {
        }
        internal void Start()
        {
            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "是否觀看劇情？");
            if (UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Menu, "是", "否") == 0)
            {
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "??：", "勇者，你費盡千辛萬苦終於到了這個地方，", "眼前矗立著最後的難關──一個巨大的魔像，", "打敗他以後，你將成功的拯救公主，並與她共度美好的餘生。");
                UiGenerate.PressAnyKeyToContinue();
                UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "??：", "你一路走來累積的經歷和收穫，在此刻變化成 100個分配點數，好好分配它，你將有機會打敗眼前的魔像，往你嚮往的未來前進。");
                UiGenerate.PressAnyKeyToContinue();
            }

        }
        internal void SetPlayer()
        {
            //產生一個Player 物件
            var player = new Player();
            //讓 Client 分配點數
            player.DistributeProperty();
            //依照點數產生相對應的裝備 & 技能
            player.SetFightProperty();
            //set Player 物件
            Player = player;
        }

        internal bool Fight()
        {
            //排定戰鬥順序
            Func<FightCharacter, FightCharacter, FightCharacter> findNowFighter = (a, b) => a.FightRoundPoint >= b.FightRoundPoint ? a : b;

            FightCharacter nowFighter = RonToolsSt20.Compare.Best<FightCharacter>(findNowFighter, Player, Giant);
            ////輪到魔像的基本AI
            ////輪到玩家的攻擊選擇
            ResetState();
            nowFighter.UseSkill();
            PlayerMove();
            return false;
        }

        private void ResetState()
        {
            throw new NotImplementedException();
        }

        private void PlayerMove()
        {
            //跳出選單讓玩家選擇技能(回傳陣列字串)
            var skillsKeyArray = Player.Skills.Keys.ToArray();
            int skillIndex = UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Menu, skillsKeyArray);

            UiGenerate.RenderOut(true, UiGenerate.WindowSelect.Plot, "請選擇對象");
            int target = UiGenerate.RenderOut(false, UiGenerate.WindowSelect.Menu, "魔像", "自己");
            FightCharacter t = target == 0 ? (FightCharacter)Giant : Player;

            Player.Move(t, skillsKeyArray[skillIndex]);
        }
    }
}