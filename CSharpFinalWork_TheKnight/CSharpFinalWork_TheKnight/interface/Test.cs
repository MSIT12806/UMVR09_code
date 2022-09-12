using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFinalWork_TheKnight
{
    //internal class TestGM
    //{
    //    Player Player { get; set; }
    //    Giant Giant { get; set; }
    //    public int Round { get; set; }
    //    bool win = false;
    //    public GameManager()
    //    {
    //    }
    //    public void Game()
    //    {
    //        while (!win)
    //        {
    //            Start();
    //            SetPlayer();
    //            Fight();
    //            End();
    //        }
    //    }


    //    internal void Start()
    //    {
    //            UiGenerate.PressAnyKeyToContinue();

    //        //選擇難易度？
    //        Giant = new Giant();

    //    }
    //    internal void SetPlayer()
    //    {
    //        //產生一個Player 物件
    //        var player = new Player();
    //        //讓 Client 分配點數
    //        player.DistributeProperty();
    //        //set Player 物件
    //        Player = player;
    //    }

    //    internal void Fight()
    //    {
    //        //前置作業

    //        //1. 綁定雙方戰鬥物件
    //        Player.SetOpponent(Giant);
    //        Giant.SetOpponent(Player);
    //        //2. 生成戰鬥屬性
    //        //依照點數產生相對應的裝備 & 技能 & 從gm注入要挑戰的對象
    //        Player.SetFightProperty();
    //        //3. 排定戰鬥順序
    //        Func<FightCharacter, FightCharacter, FightCharacter> findNowFighter = (a, b) => a.FightRoundPoint >= b.FightRoundPoint ? a : b;
    //        //3. 戰鬥迴圈
    //        while (true)
    //        {
    //            FightCharacter nowFighter = RonToolsSt20.Compare.Best<FightCharacter>(findNowFighter, Player, Giant);
    //            ////輪到魔像的基本AI
    //            ////輪到玩家的攻擊選擇
    //            ResetState();
    //            if (nowFighter == Player)
    //                Player.SelectSkill();
    //            else
    //                Giant.RandomSkill();
    //            nowFighter.SetBuffAndUseSkill();

    //        }

    //        void ResetState()
    //        {
    //            Player.ResetState();
    //            Giant.ResetState();
    //        }
    //    }

    //    private void End()
    //    {
    //        if (win)
    //        {

    //        }
    //        else
    //        {

    //        }
    //    }

    //}
}
