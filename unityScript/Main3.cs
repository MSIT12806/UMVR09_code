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
    ObjectPool oPool = null;

    private List<ObjectPool.ObjectPoolData> pTestList = null;
    private List<GameObject> pAliveObjects = new List<GameObject>();
    public Object npcObject;

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
        oPool = GetComponent<ObjectPool>();

       
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

   void mySceneLoaded(string sSceneName)
    {
        StartCoroutine(SpawnObjects(sSceneName));
    }

   IEnumerator SpawnObjects(string sSceneName)
    {
        if(sSceneName == "s1")
        {
            pTestList = oPool.InitObjectPoolData(npcObject, 5);
        }
        float fProgress = 0.0f;
        while (true)
        {
            int iProgress = (int)fProgress;
            Debug.Log("SpawnObjects  " + iProgress + "%");
            fProgress += 0.5f;
            oPool.AddObjectPoolData(pTestList, npcObject, 5);
            if (fProgress > 100.0f)
            {
                fProgress = 100.0f;
                Debug.Log("SpawnObjects  " + iProgress + "%");
                break;
            }
            yield return 0;
        }

        Debug.Log("num Object added " + pTestList.Count);
    }

    IEnumerator downloadData(string sName)
    {
        float fProgress = 0.0f;
        while (true)
        {
            int iProgress = (int)fProgress;
            Debug.Log("download data " + iProgress + "%");
            fProgress += 0.5f;
            if(fProgress > 100.0f)
            {
                fProgress = 100.0f;
                Debug.Log("download data " + iProgress + "%");
                break;
            }
            yield return 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        string sName = sLoader.GetCurrentSceneName();
        if (Input.GetMouseButtonDown(0))
        {
          
            if (sName.Equals("menu"))
            {
                StartCoroutine(sLoader.ChangeSceneAsync("s1", downloadData, mySceneLoaded));
                //sLoader.ChangeScene("s1");
            } else if(sName.Equals("s1"))  
            {
             //   StartCoroutine(sLoader.ChangeSceneAsync("menu", null, null));
                //sLoader.ChangeScene("menu");
            }
           // StartCoroutine(aLoader.LoadData2());
        }
         if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("AAA");
            if (sName == "s1")
            {
                Debug.Log("BBB");
                GameObject go = oPool.LoadObjectFromPool(pTestList);
                go.SetActive(true);
                go.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
                pAliveObjects.Add(go);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (sName == "s1")
            {
                int iCount = pAliveObjects.Count;
                int iO = Random.Range(0, iCount);
                GameObject go = pAliveObjects[iO];
                oPool.UnloadObjectToPool(pTestList, go);
                pAliveObjects.RemoveAt(iO);
            }
        }


    }
}
