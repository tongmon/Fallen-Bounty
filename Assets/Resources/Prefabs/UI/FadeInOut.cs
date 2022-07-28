using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeInOut : MonoBehaviour
{
    [SerializeField] Image fade_image;

    public void FadeInM()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutM()
    {
        StartCoroutine(FadeOut());
    }
    public void FadeOutForScene()
    {
        StartCoroutine(FadeOutScene());
    }

    IEnumerator FadeIn()
    {
        fade_image.transform.parent.GetComponent<Canvas>().sortingOrder = 2;
        fade_image.DOFade(0, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);
        fade_image.transform.parent.GetComponent<Canvas>().sortingOrder = -1;
    }

    IEnumerator FadeOut()
    {
        fade_image.transform.parent.GetComponent<Canvas>().sortingOrder = 2;
        fade_image.DOColor(Color.black, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);
        fade_image.transform.parent.GetComponent<Canvas>().sortingOrder = -1;
    }
    IEnumerator FadeOutScene()
    {
        fade_image.transform.parent.GetComponent<Canvas>().sortingOrder = 2;
        fade_image.DOColor(Color.black, 1.0f);
        yield return null;
    }
}
