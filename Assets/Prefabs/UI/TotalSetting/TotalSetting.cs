using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSetting : MonoBehaviour
{
    [SerializeField] GameObject[] m_box;
    [SerializeField] GameObject m_book_shelf;
    [SerializeField] GameObject[] m_char;
    [SerializeField] Image m_stat_panel;
    [SerializeField] GameObject m_hero_name;
    [SerializeField] GameObject m_hero_status;
    [SerializeField] Button m_start_button;
    Vector2[] m_target_vec = { new Vector2(2, 0.75f), new Vector2(0, 2), new Vector2(-2, 0.75f), new Vector2(0, 0) };

    void Start()
    {
        StartCoroutine(Box());
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
        m_start_button.GetComponent<Image>().DOColor(Color.white, 1.0f);
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
        //4-3 3-2 2-1 1-4
        GameObject temp = m_char[3];
        for (int i = 3; i >= 0; i--)
        {
            m_char[i].transform.DOMove(m_target_vec[i], 0.8f);
            if (i == 0) m_char[i] = temp;
            else m_char[i] = m_char[i - 1];
        }
    }
    public void CharSwapLeft() 
    {
        GameObject temp = m_char[0];
        for (int i = 0; i < 4; i++)
        {
            m_char[i].transform.DOMove(m_target_vec[(i+2)%4], 0.8f);
            if (i == 3) m_char[i] = temp;
            else m_char[i] = m_char[i + 1];
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Map_Scene");
    }
}
