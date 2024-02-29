using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool OnDebug = false;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(OnDebug)
        {
            return;
        }
        DontDestroyOnLoad(gameObject);

        InitializeUISetting();
    }

    public void InitializeUISetting()
    {
        UIManager.Instance.ShowPanel<InitialPanel>("InitialPanel", E_UI_Layer.System);
    }
}
