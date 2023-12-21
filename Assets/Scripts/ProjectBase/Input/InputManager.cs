using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    public bool isStart = false;
    public InputManager()
    {
        MonoManager.Instance.AddUpdateListener(MyUpdate);
    }

    private void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {

        }
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }
    public void MyUpdate()
    {
        if(!isStart)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            EventCenter.Instance.EventTrigger<string>("W", KeyCode.W.ToString());
        }
    }
}