using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEffect : MonoBehaviour
{
    private Image cover;

    private void Awake()
    {
        cover = GetComponent<Image>();
    }
    public void FadeOut()
    {
        print("work");
        cover.raycastTarget = true;
        DOTween.To(() => cover.color,
            fadeOutEffect => cover.color = fadeOutEffect,
            new Color(0, 0, 0, 1),
            0.5f
            );
    }

    public void FadeIn()
    {
        cover.raycastTarget = false;
        DOTween.To(() => cover.color,
            fadeOutEffect => cover.color = fadeOutEffect,
            new Color(0, 0, 0, 0),
            0.5f
            );
    }
}
