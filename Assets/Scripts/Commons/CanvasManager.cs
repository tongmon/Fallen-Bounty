using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_canvas;

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

    public void CanvasRemove() //마지막에 있는 친구 제거
    {
        StartCoroutine(CanvasOut());
    }
    IEnumerator CanvasIn(int index)
    {
        m_canvasList.Last.Value.gameObject.SetActive(false);
        m_canvasList.AddLast(m_canvas[index].gameObject);
        yield return new WaitForSecondsRealtime(0.4f);
        
        m_canvas[index].gameObject.SetActive(true);
    }
    IEnumerator CanvasOut()
    {
        m_canvasList.Last.Value.gameObject.SetActive(false);
        m_canvasList.RemoveLast();
        yield return new WaitForSecondsRealtime(0.4f);

        m_canvasList.Last.Value.gameObject.SetActive(true);
    }
}
