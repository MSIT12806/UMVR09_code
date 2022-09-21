using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private System.Action<string> m_afterSceneLoadCB;
    public void Start()
    {
        SceneManager.sceneLoaded += AfterSceneLoaded;
    }

    private void AfterSceneLoaded(Scene sc, LoadSceneMode mode)
    {
        Debug.Log("AfterSceneLoaded " + sc.name);

        if (m_afterSceneLoadCB != null) {
            m_afterSceneLoadCB(sc.name);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator ChangeSceneAsync(string sName, System.Action<string> afterLoadedCB)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sName);
        if(ao == null)
        {
            yield break;
        }
        m_afterSceneLoadCB = afterLoadedCB;
        ao.allowSceneActivation = false;
        while(ao.isDone == false)
        {
          //  Debug.Log("loading progress :" + ao.progress);
            if (ao.progress > 0.8999f)
            {
               ao.allowSceneActivation = true;
            }
            yield return 0;
        }
       

    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
