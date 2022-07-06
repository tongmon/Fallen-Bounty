using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSetting : MonoBehaviour
{
    [SerializeField] GameObject[] m_box; //�����۹ڽ�
    [SerializeField] GameObject m_book_shelf;//��ųâ �ڽ�
    [SerializeField] GameObject[] m_char;//ĳ���� 4��
    [SerializeField] Image m_stat_panel;//ĳ���� ����â
    [SerializeField] GameObject m_hero_name;//ĳ�����̸�
    [SerializeField] GameObject m_hero_status;//ĳ���� ������
    [SerializeField] Button m_start_button;//�������� ��ư
    Vector2[] m_target_vec = { new Vector2(2, 0.75f), new Vector2(0, 2), new Vector2(-2, 0.75f), new Vector2(0, 0) };
    //ĳ���� �̵��� �̿��� ��ǥ��

    void OnEnable()
    {
        StartCoroutine(Box());//�ڷ�ƾ�̿�
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
        m_start_button.GetComponent<Image>().DOColor(Color.white, 1.0f);//���� ��Ÿ���� ��Ʈ��
    }
    IEnumerator Box() { //������â �������� ����
        for(int i = 0; i<9; i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.3f));
            Vector2 target_vec = m_box[i].transform.position + new Vector3(5, -1.5f, 0);
            m_box[i].transform.DORotate(new Vector3(0,0,180), 1);
            m_box[i].transform.DOMove(target_vec, 1);
        }
        StopCoroutine(Box());
    }
    IEnumerator BookShelf() //��ųâ ��Ÿ����
    {
        yield return 0;
        m_book_shelf.transform.DOMoveX(6, 1);
        StopCoroutine(BookShelf());
    }
    IEnumerator StatPanel()//���� ��Ÿ����
    {
        yield return 0;
        m_stat_panel.DOColor(Color.white, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);//���������� ��Ÿ�������� �ð��� ��
        m_hero_name.SetActive(true);
        m_hero_status.SetActive(true);
        StopCoroutine(StatPanel());
    }
    public void CharSwapRight()
    {
        GameObject temp = m_char[3];
        for (int i = 3; i >= 0; i--) //���������� ��ġ����
        {
            m_char[i].transform.DOMove(m_target_vec[i], 0.8f);
            if (i == 0) m_char[i] = temp;
            else m_char[i] = m_char[i - 1];
            //��ġ�������� ĳ���� ������ ����, 0�� �ڱ��ڽ�
        }
    }
    public void CharSwapLeft() 
    {
        GameObject temp = m_char[0];
        for (int i = 0; i < 4; i++)//�������� ��ġ����
        {
            m_char[i].transform.DOMove(m_target_vec[(i+2)%4], 0.8f);
            if (i == 3) m_char[i] = temp;
            else m_char[i] = m_char[i + 1];
            //��ġ�������� ĳ���� ������ ����, 0�� �ڱ��ڽ�
        }
    }
    public void StartButton()//�����ϱ��ư
    {
        SceneManager.LoadScene("Map_Scene");//���������� �̵�
    }
}
