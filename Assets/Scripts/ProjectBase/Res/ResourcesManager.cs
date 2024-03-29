using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesManager : BaseManager<ResourcesManager>
{
    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
           
    }

    public void LoadAsync<T>(string name,UnityAction<T> callback) where T:Object
    {
        MonoManager.Instance.StartCoroutine(RootLoadAsync(name,callback));
    }

    private IEnumerator RootLoadAsync<T>(string name,UnityAction<T> callback) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;
        if(r.asset is GameObject)
        {
            callback(GameObject.Instantiate(r.asset) as T);
        }
        else
        {
            callback(r.asset as T);
        }
    }
}
