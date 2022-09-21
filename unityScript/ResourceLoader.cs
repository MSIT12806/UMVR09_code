using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    private static ResourceLoader mInstance;
    public static ResourceLoader Instance() { return mInstance; }
    public void Awake()
    {
        mInstance = this;
    }

    public void LoadData()
    {

        Texture oTex = Resources.Load("body_01") as Texture;

        Object o = Resources.Load("Models/Cube");
        Debug.Log(o.GetType());
        if (o.GetType() == typeof(GameObject))
        {
            GameObject go = Instantiate(o) as GameObject;
            go.GetComponent<Renderer>().material.mainTexture = oTex;
        }
    }

    public IEnumerator LoadDataAsync()
    {
        ResourceRequest rr = Resources.LoadAsync("body_01");
        yield return rr;
        if(rr.asset == null)
        {
            yield break;
        }
        Texture oTex = rr.asset as Texture;

        rr = Resources.LoadAsync("Models/Cube");
        yield return rr;
        if (rr.asset == null)
        {
            yield break;
        }
        Object o = rr.asset;
        Debug.Log(o.GetType());
        if (o.GetType() == typeof(GameObject))
        {
            GameObject go = Instantiate(o) as GameObject;
            go.GetComponent<Renderer>().material.mainTexture = oTex;
        }
    }
}
