using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Push", 1);
    }

    // Update is called once per frame
    void Push()
    {
        PoolManager.Instance.PushObject(name, gameObject);
    }
}
