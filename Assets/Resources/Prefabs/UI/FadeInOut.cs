using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeInOut : MonoBehaviour
{
    [SerializeField] Image fade_image;
    void Start()
    {
        fade_image.DOFade(0, 0.01f);
    }

    IEnumerator FadeIn()
    {
        yield return null;
    }

    IEnumerator FadeOut()
    {
        yield return null;
    }
}
