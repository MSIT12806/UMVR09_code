using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadManifestData()
    {
        Debug.Log(Application.streamingAssetsPath);
        string sUrl = "file://"+ Application.streamingAssetsPath;
        string sBundleUrl = sUrl + "/StandaloneWindows";
        WWW w = new WWW(sBundleUrl);
        yield return w;
        AssetBundleManifest abm = w.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        Debug.Log(abm);
        string [] ss = abm.GetAllAssetBundles();
        foreach (string sbundle in ss)
        {
            string[] sDeps = abm.GetAllDependencies(sbundle);
            Debug.Log(sbundle);
            foreach (string sDep in sDeps)
            {
                Debug.Log("Dep: " + sDep);
            }
        }

    }

    public IEnumerator LoadData()
    {
        Debug.Log(Application.streamingAssetsPath);
        string sUrl = "file://" + Application.streamingAssetsPath;
        string sBundleUrl = sUrl + "/models";
        WWW w = WWW.LoadFromCacheOrDownload(sBundleUrl, 1);
        yield return w;

        Object o = w.assetBundle.LoadAsset("Cube");
        Instantiate(o);

    }

    public IEnumerator LoadData2()
    {
     
        string sUrl = "file://" + Application.streamingAssetsPath;

        string sBundleUrlMat = sUrl + "/mat";

        UnityWebRequest uwrMat = UnityWebRequestAssetBundle.GetAssetBundle(sBundleUrlMat);
        UnityWebRequestAsyncOperation ao = uwrMat.SendWebRequest();
        yield return ao;

        AssetBundle[] abs = new AssetBundle[2];
        if (uwrMat.result != UnityWebRequest.Result.ConnectionError)
        {
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(uwrMat);
            if (ab != null)
            {
                Material m = ab.LoadAsset<Material>("New Material");
                Debug.Log("Material " + m.name);
              //  Instantiate(ab.LoadAsset<Object>("Cube"));
            }
            abs[0] = ab;
        }


        string sBundleUrl = sUrl + "/models";

        UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(sBundleUrl);
        ao = uwr.SendWebRequest();
        yield return ao;

        if(uwr.result != UnityWebRequest.Result.ConnectionError)
        {
            AssetBundle ab = DownloadHandlerAssetBundle.GetContent(uwr);
            if(ab != null)
            {
                Instantiate(ab.LoadAsset<Object>("Cube"));
            }
            abs[1] = ab;
        }


        foreach(AssetBundle ab in abs)
        {
            ab.Unload(false);
        }

    }
}

