using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main mInstance;
    public static Main Instance() { return mInstance; }

    ResourceLoader rLoader = null;
    AssetBundleLoader aLoader = null;
    SceneLoader sLoader = null;

    private void Awake()
    {
        mInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    //   private ResourceLoader mLoader;
    // Start is called before the first frame update
    void Start()
    {
        rLoader = ResourceLoader.Instance();
        aLoader = GetComponent<AssetBundleLoader>();
        sLoader = GetComponent<SceneLoader>();
        // mLoader = GetComponent<ResourceLoader>();

        // rLoader.LoadData();
        //   StartCoroutine(rLoader.LoadDataAsync());

        //StartCoroutine(aLoader.LoadManifestData());

        sLoader.ChangeScene("menu");

    }

    //public ResourceLoader GetLoader()
    //{
    //    return mLoader;
    //}

   void SpawnObjects(string sSceneName)
    {
        Debug.Log("SpawnObjects");



    }


    // Update is called once per frame
    void Update()
    {
     if(Input.GetMouseButtonDown(0))
        {
            string sName = sLoader.GetCurrentSceneName();
            if (sName.Equals("menu"))
            {
                StartCoroutine(sLoader.ChangeSceneAsync("s1", SpawnObjects));
                //sLoader.ChangeScene("s1");
            } else if(sName.Equals("s1"))  
            {
                StartCoroutine(sLoader.ChangeSceneAsync("menu", null));
                //sLoader.ChangeScene("menu");
            }
           // StartCoroutine(aLoader.LoadData2());
        }
    }
}
