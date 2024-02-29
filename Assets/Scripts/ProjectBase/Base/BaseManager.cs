using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/// <summary>
/// Base Singleton mode
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseManager<T> where T: BaseManager<T> , new()
{
    private static T instance;
    //Branch test
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
