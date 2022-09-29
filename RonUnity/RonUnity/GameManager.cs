using System;
using UnityEngine;

namespace RonUnity
{
    public class Singleton<T> where T : MonoBehaviour
    {
        /*
         * 獨體模式：
         * 讓注入的類別(gameobject)，只能單一。
         * 應用static
         */
        /*
         * 自定義組件，又名 MonoBehaviour 派生對象，其構造函數中不能有參數，也不能使用 new 手動創建。組件是策略設計模式的一部分，只能使用 AddComponent() 創建
         */
        public static T instance;
        string gameVersion = "1";
        //void Awake()
        //{
        //    MustOnlyOne();
        //}

        public void MustOnlyOne()
        {
            if (instance != null)
            {
                Debug.LogErrorFormat(instance.gameObject,
                "Multiple instances of {0} is not allow", GetType().Name);
                UnityEngine.Object.DestroyImmediate(instance.gameObject);
                return;
            }
            UnityEngine.Object.DontDestroyOnLoad(instance.gameObject);
            //instance = this;
        }
    }
}
