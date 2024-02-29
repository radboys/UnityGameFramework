using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class InitialPanel : BasePanel
{
    public Image logo;
    private void Start()
    {
        StartCoroutine(LogoEffect());

    }

    IEnumerator LogoEffect()
    {
        DOTween.To(() => logo.color,
            fadeOutEffect => logo.color = fadeOutEffect,
            new Color(1, 1, 1, 1),
            2
            ).SetEase(Ease.Linear);
        yield return new WaitForSeconds(3);
        DOTween.To(() => logo.color,
            fadeOutEffect => logo.color = fadeOutEffect,
            new Color(1, 1, 1, 0),
            2
            ).SetEase(Ease.Linear);
        yield return new WaitForSeconds(2);
        ScenesManager.Instance.LoadSceneAsync<StartScene>("StartScene", () => { Destroy(gameObject); });
    }
}
