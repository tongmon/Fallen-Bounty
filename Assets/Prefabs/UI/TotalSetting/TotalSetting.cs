using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSetting : FadeInOut
{
    //���̺�����
    SaveState saveState;

    //�÷��̾�
    Player player;

    private int char_index;

    //�����۹ڽ�
    [SerializeField] GameObject[] m_box;

    //��ųâ �ڽ�
    [SerializeField] GameObject m_book_shelf;

    //ĳ���� 4��
    [SerializeField] GameObject[] m_char;

    //ĳ�����̸�
    [SerializeField] GameObject m_hero_name;

    //ĳ���� ������
    [SerializeField] GameObject m_hero_status;

    //���� ��ġ �����
    Vector3 []m_pos = new Vector3[10];

    //ĳ���� ����â
    [SerializeField] Image m_stat_panel;

    //���� ��ư
    [SerializeField] Button[] m_swap_button;

    //���̵� �ƿ� �̹���
    [SerializeField] Image FadeInOut;

    //�������� ��ư
    [SerializeField] Button m_start_button;

    Vector3[] m_target_vec_two = { new Vector3(0, 225f, 0), new Vector3(0, 75f, 0) };

    Vector3[] m_target_vec_three = { new Vector3(200, 225f, 0), new Vector3(-200, 225f, 0), new Vector3(0, 75f, 0) };

    Vector3[] m_target_vec_four = { new Vector3(200, 150f,0), new Vector3(0, 225f,0),
        new Vector3(-200, 150f,0), new Vector3(0, 75f,0) };

    private void Awake()//��ġ ���� �� ���̺����� ������
    {
        char_index = 0;//��ų�ڽ��ȿ� �ε����� ã������.
        saveState = (SaveState)Resources.Load("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName"));

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//�÷��̾� ����

        OnStart(player.m_hero_holder.m_hero_limit);

        for(int i = 0; i < m_pos.Length- 1; i++)
        {
            m_pos[i] = m_box[i].transform.localPosition;
        }
        m_pos[9] = m_book_shelf.transform.localPosition;
    }
    void OnEnable()
    {
        for (int i = 0; i < m_pos.Length - 1; i++)
        {
            m_box[i].transform.localPosition = m_pos[i];
        }
        m_book_shelf.transform.localPosition = m_pos[9];
        
        StartCoroutine(Box());//�ڷ�ƾ �̿�
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
        m_start_button.GetComponent<Image>().DOColor(Color.white, 1.0f);//���� ��Ÿ���� ��Ʈ��
    }

    public void OnStart(int index)//���ҹ�ư ���� �� ĳ���� ��Ȱ��ȭ.
    {
        if(index == 1)
        {
            m_swap_button[0].interactable = false;
            m_swap_button[1].interactable = false;
        }
        for(int i = index; i <m_char.Length; i++)
        {
            m_char[i].SetActive(false);
        }
        if (index == 2)
        {
            m_char[0].transform.localPosition = new Vector3(0, 75f, 0);
            m_char[1].transform.localPosition = new Vector3(0, 225f, 0);
        }
        else if (index == 3) 
        { 
            m_char[0].transform.localPosition = new Vector3(0, 75f, 0);
            m_char[1].transform.localPosition = new Vector3(200, 225f, 0);
            m_char[2].transform.localPosition = new Vector3(-200, 225f, 0);
        }
    }
    public void StartButtonClicked()//ĳ���� ���õ��� ����.
    {
        StartCoroutine(StartScene());
    }
    public void CharSwapRight()
    {
        int index = player.m_hero_holder.m_hero_limit;//�ε���(����� ��)�� �� ��ġ���� �ٸ��� �ؾ���.
        GameObject temp = m_char[index-1];
        if(char_index == 0)
        {
            char_index = index;
        }
        else char_index--;

        for (int i = index-1; i >= 0; i--) //���������� ��ġ����
        {
            if(index == 2) m_char[i].transform.DOLocalMove(m_target_vec_two[i], 0.4f);
            else if(index == 3) m_char[i].transform.DOLocalMove(m_target_vec_three[i], 0.4f);
            else m_char[i].transform.DOLocalMove(m_target_vec_four[i], 0.4f);

            if (i == 0)
            {
                m_char[i] = temp;
            }
            else m_char[i] = m_char[i - 1];

            //��ġ�������� ĳ���� ������ ����, 0�� �ڱ��ڽ�
        }
    }
    public void CharSwapLeft()
    {
        int index = player.m_hero_holder.m_hero_limit;
        GameObject temp = m_char[0];
        if (char_index == index)
        {
            char_index = 0;
        }
        else char_index++;

        for (int i = 0; i < index; i++)//�������� ��ġ����
        {
            if(index == 2) m_char[i].transform.DOLocalMove(m_target_vec_two[(i) % index], 0.4f);//�Ѵ� �ѹ� ����ٰ�
            else if(index == 3) m_char[i].transform.DOLocalMove(m_target_vec_three[(i + 1) % index], 0.4f);//�굵
            else m_char[i].transform.DOLocalMove(m_target_vec_four[(i + 2) % index], 0.4f);

            if (i == index-1) m_char[i] = temp;
            else m_char[i] = m_char[i + 1];
        }
    }   
    public void StartButton()//�����ϱ��ư
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartScene()//���ӽ���
    {
        FadeInOut.gameObject.SetActive(true);
        FadeInOut.DOColor(Color.black, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);

        SceneManager.LoadScene("Map_Scene");
    }
    IEnumerator Box() { //������â �������� ����
        for(int i = 0; i<9; i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.3f));
            Vector3 target_vec = m_box[i].transform.position + new Vector3(5f, -1.5f, 0);
            m_box[i].transform.DORotate(new Vector3(0,0,180), 1);
            m_box[i].transform.DOMove(target_vec, 1);
        }
        StopCoroutine(Box());
    }
    IEnumerator BookShelf() //��ųâ ��Ÿ����
    {
        yield return 0;
        m_book_shelf.transform.DOMoveX(6f, 1);
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
    IEnumerator StartGame()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("Map_Scene");//���������� �̵�
    }
}
