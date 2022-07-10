using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MouseOver : MonoBehaviour
{
    [SerializeField] Image m_panel;

    public void MouseEnter()
    {
        StopCoroutine("MOut");
        StartCoroutine("MIn");
    }
    
    public void MouseOut()
    {
        StopCoroutine("MIn");
        StartCoroutine("MOut");
    }
    IEnumerator MIn()
    {
        yield return null;

        m_panel.gameObject.SetActive(true);
        m_panel.DOColor(Color.white, 0.1f);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_panel.transform.position = pos * Camera.main.transform.localScale;
        m_panel.transform.localPosition = new Vector3(m_panel.transform.localPosition.x + 150, m_panel.transform.localPosition.y - 100, 0);
    }
    IEnumerator MOut()
    {
        m_panel.DOFade(0, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.gameObject.SetActive(false);
        StopCoroutine(MOut());
    }
}
