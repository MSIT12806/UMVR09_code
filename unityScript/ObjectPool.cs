using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public class ObjectPoolData
    {
        public GameObject go;
        public bool bUsing;
    }

    // private List<ObjectPoolData> pObjects;
    private List<List<ObjectPoolData>> pObjectTypes;

    private void Awake()
    {
        pObjectTypes = new List<List<ObjectPoolData>>();
    }

    List<ObjectPoolData> QueryEmptyList()
    {
        List<ObjectPoolData> pList = new List<ObjectPoolData>();
        pObjectTypes.Add(pList);
        return pList;
       // int inList = pObjectTypes.Count;
    }

    public List<ObjectPoolData> InitObjectPoolData(Object o, int iCount)
    {
        List<ObjectPoolData> pList = QueryEmptyList();

        for(int i = 0; i < iCount; i++)
        {
            GameObject go = Instantiate(o) as GameObject;
            ObjectPoolData data = new ObjectPoolData();
            data.bUsing = false;
            data.go = go;
            go.SetActive(false);
            pList.Add(data);
        }

        return pList;
    }

    public void AddObjectPoolData(List<ObjectPoolData> pList, Object o, int iCount)
    {
        for (int i = 0; i < iCount; i++)
        {
            GameObject go = Instantiate(o) as GameObject;
            ObjectPoolData data = new ObjectPoolData();
            data.bUsing = false;
            data.go = go;
            go.SetActive(false);
            pList.Add(data);
        }
    }

    public void ClearObjectPoolList(List<ObjectPoolData> pList)
    {
        pList.Clear();
        pObjectTypes.Remove(pList);
    }

    public GameObject LoadObjectFromPool(List<ObjectPoolData> pList)
    {
        int iCount = pList.Count;
        for(int i = 0; i < iCount; i++)
        {
            if(pList[i].bUsing == false)
            {
                pList[i].bUsing = true;
                return pList[i].go;
            }

        }

        return null;
    }

    public void UnloadObjectToPool(List<ObjectPoolData> pList, GameObject go)
    {
        int iCount = pList.Count;
        for (int i = 0; i < iCount; i++)
        {
            if (pList[i].go == go)
            {
                go.SetActive(false);
                pList[i].bUsing = false;
                break;
            }
        }
    }
}
