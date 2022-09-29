using System;
using UnityEngine;

namespace RonUnity
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        string gameVersion = "1";
        void Awake()
        {
            if (instance != null)
            {
                Debug.LogErrorFormat(gameObject,
                "Multiple instances of {0} is not allow", GetType().Name);
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}
