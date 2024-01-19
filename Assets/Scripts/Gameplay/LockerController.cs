using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LockerController : MonoBehaviour
{
    private Vector3[] locker;
    // Start is called before the first frame update
    public AudioSource kadaSound;

    private float leftLimit = -20;
    private float rightLimit = 20;

    public int length;
    public int kadaIndex;

    private int currentIndex;
    void Start()
    {
        locker = GenerateVector3Array(length);
        currentIndex = length / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveArrayLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveArrayRight();
        }
    }

    public Vector3[] GenerateVector3Array(int length)
    {
        if (length < 3)
        {
            Debug.LogError("Array length should be at least 3.");
            return null;
        }

        Vector3[] vectorArray = new Vector3[length];
        vectorArray[0] = new Vector3(-10f, 0f, 3f);
        vectorArray[length - 1] = new Vector3(10f, 0f, 3f);

        float interval = (vectorArray[length - 1] - vectorArray[0]).magnitude / (length - 1);

        for (int i = 1; i < length - 1; i++)
        {
            vectorArray[i] = vectorArray[0] + interval * i * Vector3.Normalize(vectorArray[length - 1] - vectorArray[0]);
        }

        foreach(var item in vectorArray)
        {
            //print(item);
        }

        return vectorArray;
    }

    private void MoveArrayLeft()
    {
        if (currentIndex < locker.Length - 1)
        {
            currentIndex++;
        }
        kadaSound.transform.position = UpdateArrayPositions(locker)[kadaIndex];
        kadaSound.Play();
    }

    private void MoveArrayRight()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        UpdateArrayPositions(locker);
        kadaSound.transform.position = UpdateArrayPositions(locker)[kadaIndex];
        kadaSound.Play();
    }

    private Vector3[] UpdateArrayPositions(Vector3[] originalArray)
    {
        if (originalArray == null || originalArray.Length < 3 || currentIndex < 0 || currentIndex >= originalArray.Length)
        {
            Debug.LogError("Invalid input data.");
            return null;
        }

        Vector3[] recalculatedArray = new Vector3[originalArray.Length];
        Vector3 centerPosition = originalArray[currentIndex]; // 指定的中心位置

        for (int i = 0; i < originalArray.Length; i++)
        {
            recalculatedArray[i] = originalArray[i] - centerPosition + new Vector3(0f, 0f, 3f);
        }

        foreach (var item in recalculatedArray)
        {
            print(item);
        }

        return recalculatedArray;
    }

    private void PlayKada()
    {
        kadaSound.transform.position = locker[kadaIndex];
        kadaSound.Play();
    }

}
