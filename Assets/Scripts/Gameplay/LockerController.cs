using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;

public class LockerController : MonoBehaviour
{
    [SerializeField] private AudioSource kadaSound; // Serialized for editor access, private to restrict external access
    [SerializeField] private AudioSource unlock; // Serialized for editor access, private to restrict external access
    [SerializeField] private AudioSource failUnlock; // Serialized for editor access, private to restrict external access

    private Vector3[] locker;

    private const float LeftLimit = -20;
    private const float RightLimit = 20;

    [SerializeField] private int length;
    [SerializeField] private int kadaIndex;
    [SerializeField] private int currentIndex;

    public Animator animator;

    public Light light;

    public Animator UnlockAnimation;
    // Start is called before the first frame update
    void Start()
    {
        locker = GenerateVector3Array(length);
        UpdateSoundPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveArrayLeft();
            ShiningDistance();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveArrayRight();
            ShiningDistance();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentIndex == kadaIndex)
            {
                unlock.Play();
                UnlockAnimation.SetTrigger("Unlock");
            }
            else
            {
                failUnlock.Play();
            }
        }
    }

    // Generates an array of Vector3s evenly spaced along a line
    private Vector3[] GenerateVector3Array(int length)
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

        return vectorArray;
    }

    // Move the array left and update sound position
    private void MoveArrayLeft()
    {
        if (currentIndex < locker.Length - 1)
        {
            currentIndex++;
           
        }
         UpdateSoundPosition();
    }

    // Move the array right and update sound position
    private void MoveArrayRight()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            
        }
        UpdateSoundPosition();
    }

    // Updates the array positions and plays the sound
    private void UpdateSoundPosition()
    {
        Vector3[] updatedPositions = UpdateArrayPositions(locker);
        if (updatedPositions != null)
        {
            kadaSound.transform.position = updatedPositions[kadaIndex];
            kadaSound.Play();
        }
    }

    // Recalculates positions of the array elements based on the current index
    private Vector3[] UpdateArrayPositions(Vector3[] originalArray)
    {
        if (originalArray == null || originalArray.Length < 3 || currentIndex < 0 || currentIndex >= originalArray.Length)
        {
            Debug.LogError("Invalid input data.");
            return null;
        }

        Vector3[] recalculatedArray = new Vector3[originalArray.Length];
        Vector3 centerPosition = originalArray[currentIndex];

        for (int i = 0; i < originalArray.Length; i++)
        {
            recalculatedArray[i] = originalArray[i] - centerPosition + new Vector3(0f, 0f, 3f);
        }

        return recalculatedArray;
    }

    private void ShiningDistance()
    {
        animator.SetTrigger("Tik");
        light.intensity = 2f - 2*(float)Mathf.Abs(currentIndex - kadaIndex) / (float)locker.Length;
        light.range = 4f - 3 *(float)Mathf.Abs(currentIndex - kadaIndex) / (float)locker.Length;
    }
}
