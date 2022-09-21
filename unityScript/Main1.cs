using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    private int iTest = 0;
    ResourceLoader rLoader = null;
    AssetBundleLoader aLoader = null;
    //   private ResourceLoader mLoader;
    // Start is called before the first frame update
    void Start()
    {
        rLoader = ResourceLoader.Instance();
        aLoader = GetComponent<AssetBundleLoader>();
        // mLoader = GetComponent<ResourceLoader>();

       // rLoader.LoadData();
     //   StartCoroutine(rLoader.LoadDataAsync());

        StartCoroutine(aLoader.LoadManifestData());
        
    }

    //public ResourceLoader GetLoader()
    //{
    //    return mLoader;
    //}


    // Update is called once per frame
    void Update()
    {
     if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(aLoader.LoadData2());
        }
    }
}
