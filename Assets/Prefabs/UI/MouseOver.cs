using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MouseOver : MonoBehaviour
{
    [SerializeField] Image m_panel;
    
    
    public void ObjectIn()
    {
        StartCoroutine(InputIn());
    }
    public void ObjectOut()
    {
        StartCoroutine(InputOut());
    }
    IEnumerator InputIn()
    {
        m_panel.gameObject.SetActive(true);
        m_panel.transform.localPosition = Input.mousePosition + new Vector3(-550,-300); //위치조정 필
        m_panel.DOColor(Color.white, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);
        //텍스트 가져오기 필
    }
    IEnumerator InputOut()
    {
        m_panel.DOFade(0, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);
        m_panel.gameObject.SetActive(false);
        //텍스트 가져오기 필
    }
}
