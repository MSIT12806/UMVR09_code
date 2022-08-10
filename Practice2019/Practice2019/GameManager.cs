using System;
using System.Collections.Generic;
using System.Text;

namespace Practice02
{
    /// <summary>
    /// 資源管理器，可快速、方便讓所成員函式查找目前遊戲狀態下資源，如怪物清單、玩家清單、關卡資訊等等
    /// </summary>
    public static class GameManager
    {
        /*
         #1 生成場景：場景名稱、怪物、角色等等。 

         */
        public enum SceneNumber
        {
            新手村 = 1,
            歌唱之島 = 2,
            鬼島 = 3
        }

        //遊戲流程

        public enum RoundState
        {
            一般,
            戰鬥,
        }
        public static RoundState roundState;

        public static Player player;
        public static Scene scene = new Scene();

        public static void GameStart()
        {
            player = new Player(10, 5);
            ChangeScene(SceneNumber.新手村);

            Console.WriteLine("你好勇者");
        }

        
        public static void ChangeScene(SceneNumber sceneNum)
        {
            switch (sceneNum)
            {
                case SceneNumber.新手村:
                    SetSceneBeginnerVillage();
                    break;
                case SceneNumber.歌唱之島:
                    SetSceneSingingIsland();
                    break;
                case SceneNumber.鬼島:
                    SetSceneGhostIsland();
                    break;
            }

            scene.ShowSceneName();
        }

        public static void SetSceneBeginnerVillage()
        {
            scene.sceneName = SceneNumber.新手村.ToString();
            scene.monsters = new Monster[] {
                MonsterFactory.GetSlime(),
                MonsterFactory.GetSlime(),
                MonsterFactory.GetSlime(),
            };

            
        }
        public static void SetSceneSingingIsland()
        {
            scene.sceneName = SceneNumber.歌唱之島.ToString();
            scene.monsters = new Monster[] {
                MonsterFactory.GetBoar(),
                MonsterFactory.GetBoar(),
                MonsterFactory.GetBoar(),
            };

        }
        public static void SetSceneGhostIsland()
        {
            scene.sceneName = SceneNumber.鬼島.ToString();
            scene.monsters = new Monster[] {
                MonsterFactory.GetGoblins(),
                MonsterFactory.GetGoblins(),
                MonsterFactory.GetGoblins(),
            };

        }
    }
}
