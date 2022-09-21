using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    private int iTest = 0;
    ResourceLoader rLoader = null;
 //   private ResourceLoader mLoader;
    // Start is called before the first frame update
    void Start()
    {
        rLoader = ResourceLoader.Instance();
        // mLoader = GetComponent<ResourceLoader>();

        rLoader.LoadData();
        StartCoroutine(rLoader.LoadDataAsync());
    }

    //public ResourceLoader GetLoader()
    //{
    //    return mLoader;
    //}


    // Update is called once per frame
    void Update()
    {
     
    }
}
