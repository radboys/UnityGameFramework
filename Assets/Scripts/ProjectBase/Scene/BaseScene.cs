using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public virtual void InitializeScene()
    {

    }

    public virtual void ExitScene()
    {
        UIManager.Instance.ClearPanel();
        Destroy(gameObject);
    }
}
