﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Buffer pools(Gameobject pool) module
/// in order to save the memory cost
/// </summary>
public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, PoolData> poolsDictionary = new Dictionary<string, PoolData>();

    private GameObject poolObject;
    /// <summary>
    /// obtain the gameobjects stored in the pools
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetGameObject(string name,UnityAction<GameObject> callback)
    {
        if(poolsDictionary.ContainsKey(name)&&poolsDictionary[name].poolList.Count>0)
        {
            callback(poolsDictionary[name].GetGameObject());
        }
        else
        {
            //load resource async
            ResourcesManager.Instance.LoadAsync<GameObject>(name, (o) => 
            {
                o.name = name;
                callback(o);
            }
            );
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            //obj.name = name;
        }
    }

    /// <summary>
    /// put the gameobjects back to pools
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObject(string name,GameObject obj)
    {
        if(poolObject == null)
        {
            poolObject = new GameObject("Pool");           
        }

        if(poolsDictionary.ContainsKey(name))
        {
            poolsDictionary[name].PushObject(obj);
        }
        else
        {
            poolsDictionary.Add(name, new PoolData(obj,poolObject));
        }
    }

    /// <summary>
    /// activated when switch among scenes
    /// </summary>
    public void Clear()
    {
        poolsDictionary.Clear();
        poolObject = null;
    }

}
