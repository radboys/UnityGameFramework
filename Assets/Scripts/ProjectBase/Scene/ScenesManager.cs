using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

public class ScenesManager : BaseManager<ScenesManager>
{
    private GameObject currentScene = null;

    private TransitionEffect transition = UIManager.Instance.TransitionCover.GetComponent<TransitionEffect>();

    public void LoadScene(string name, UnityAction func)
    {
        SceneManager.LoadScene(name);
        func();
    }
    
    public void LoadSceneAsync<T>(string name,UnityAction func) where T : BaseScene
    {
        MonoManager.Instance.StartCoroutine(RootLoadSceneAsync<T>(name, func));
    }

    private IEnumerator RootLoadSceneAsync<T>(string name, UnityAction func) where T : BaseScene
    {
        transition.FadeOut();
        yield return new WaitForSeconds(0.7f);

        if (currentScene != null)
        {
            currentScene.GetComponent<BaseScene>().ExitScene();
        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while(!ao.isDone)
        {
            //TODO
            //EventCenter.GetInstance().EventTrigger("", ao.progress);
            yield return null;
        }
        

        currentScene = new GameObject("CurrentSceneManager");
        currentScene.AddComponent<T>();

        if (func != null)
        {
            func();
        }

        transition.FadeIn();
    }
}
