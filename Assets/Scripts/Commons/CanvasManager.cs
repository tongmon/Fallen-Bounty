using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_canvas;

    [SerializeField] Image FadeInOut;

    private LinkedList <GameObject> m_canvasList = new LinkedList<GameObject>();


    void Start()
    {
        m_canvas[0].SetActive(true);
        m_canvasList.AddFirst(m_canvas[0]);
    }
    public void TitleCanvasAdd()
    {
        int index = 1;
        StartCoroutine(CanvasIn(index));
    }
    public void LogCanvasAdd()
    {
        int index = 2;
        StartCoroutine(CanvasIn(index));
    }
    public void OptionCanvasAdd()
    {
        int index = 3;
        StartCoroutine(CanvasIn(index));
    }
    public void CharacterCanvasAdd()
    {
        int index = 4;
        StartCoroutine(CanvasIn(index));
    }
    public void TotalSettingCanvasAdd()
    {
        int index = 5;
        StartCoroutine(CanvasIn(index));
    }

    public void CanvasRemove() //�������� �ִ� ģ�� ����
    {
        StartCoroutine(CanvasOut());
    }
    IEnumerator CanvasIn(int index)
    {
        FadeInOut.gameObject.SetActive(true);
        FadeInOut.DOColor(Color.black, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);

        m_canvasList.Last.Value.gameObject.SetActive(false);
        m_canvasList.AddLast(m_canvas[index].gameObject);
        FadeInOut.DOFade(0, 1.0f);
        yield return new WaitForSecondsRealtime(0.4f);

        m_canvas[index].gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.6f);

        FadeInOut.gameObject.SetActive(false);
    }
    IEnumerator CanvasOut()
    {
        FadeInOut.gameObject.SetActive(true);
        FadeInOut.DOColor(Color.black, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);

        m_canvasList.Last.Value.gameObject.SetActive(false);
        m_canvasList.RemoveLast();
        FadeInOut.DOFade(0, 1.0f);
        yield return new WaitForSecondsRealtime(0.4f);

        m_canvasList.Last.Value.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.6f);

        FadeInOut.gameObject.SetActive(false);
    }
}