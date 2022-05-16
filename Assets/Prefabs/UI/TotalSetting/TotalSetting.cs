using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TotalSetting : MonoBehaviour
{
    [SerializeField] GameObject[] m_box;
    [SerializeField] GameObject m_book_shelf;
    [SerializeField] GameObject[] m_char;
    [SerializeField] Image m_stat_panel;
    [SerializeField] GameObject m_hero_name;
    [SerializeField] GameObject m_hero_status;
    Vector2[] m_target_vec = { new Vector2(2, 0.75f), new Vector2(0, 2), new Vector2(-2, 0.75f), new Vector2(0, 0) };

    public int m_char_number = 0;
    void Start()
    {
        StartCoroutine(Box());
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
    }
    IEnumerator Box() { 
        for(int i = 0; i<9; i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.3f));
            Vector2 target_vec = m_box[i].transform.position + new Vector3(5, -1.5f, 0);
            m_box[i].transform.DORotate(new Vector3(0,0,180), 1);
            m_box[i].transform.DOMove(target_vec, 1);
        }
        StopCoroutine(Box());
    }
    IEnumerator BookShelf()
    {
        yield return 0;
        m_book_shelf.transform.DOMoveX(6, 1);
        StopCoroutine(BookShelf());
    }
    IEnumerator StatPanel()
    {
        yield return 0;
        m_stat_panel.DOColor(Color.white, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);
        m_hero_name.SetActive(true);
        m_hero_status.SetActive(true);
        StopCoroutine(StatPanel());
    }
    public void CharSwapRight()
    {
        m_char_number %= 4;
        for (int i = 0; i < 4; i++)
        {
            m_char[i].transform.DOMove(m_target_vec[(i+ m_char_number) % 4], 0.8f);
        }
        m_char_number++;
    }
    public void CharSwapLeft() //아직 미완성
    {
        for (int i = 0; i < 4; i++)
        {
            m_char[i].transform.DOMove(m_target_vec[(i + m_char_number) % 4], 0.8f);
        }
        if (m_char_number == 0) m_char_number = 3;
        else m_char_number--;
    }
}
