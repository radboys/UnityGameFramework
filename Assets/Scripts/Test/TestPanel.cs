using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestPanel : MonoBehaviour
{
    public void Switch()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
