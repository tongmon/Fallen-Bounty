using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameScene : FadeInOut
{
    [SerializeField] GameObject m_map_obj;

    [SerializeField] GameObject m_pause_panel; 

    [SerializeField] GameObject m_confirm_panel;

    //������ ����Ʈ
    [SerializeField] List <GameObject> m_item; 

    //�������� Ŭ����� �����ϴ� ���� ī�帮��Ʈ
    [SerializeField] GameObject[] m_reward_card; 

    //�г�Ƽ ī�� ����Ʈ
    [SerializeField] GameObject[] m_panalty_card;

    //�������� ������ ���ư����ư
    [SerializeField] GameObject m_back_button;

    //������ ����
    [SerializeField] ItemInfo[] m_itemInfos;

    //��ųĭ
    [SerializeField] GameObject[] m_skills;
    
    //���� ������ ����
    GameObject m_selected_reward_card;

    //���� ������ �г�Ƽ
    GameObject m_selected_panalty_card;

    //������ �÷��̾�
    GameObject m_player;
    
    //�� ���
    List <MapNode> m_node;

    //�Ͻ����� ��ۿ� �οﺯ��
    bool m_toggle = false;

    //���� �������� �οﺯ��
    bool m_reward_selected = false;

    //�г�Ƽ �������� �οﺯ��
    bool m_panalty_selected = false;

    //�ִϸ��̼ǿ� ��������
    float m_angle = 0;

    //���Ӽӵ� ����� ���� 
    float m_game_speed = 1.0f;

    private void Start()
    {
        m_player = GameObject.Find("Player");
        m_skills[0].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        m_player.transform.GetChild(0).GetComponent<Hero>().abilities[0].Activate(m_player.transform.GetChild(0).GetComponent<Hero>() ,m_player.transform.GetChild(0).gameObject));
        /*for(int i = 0; i < player.transform.childCount; i++)//���� ��
        {
            for(int j= 0; j < 4; j++)//��ų ����
            {
                m_skills[i].transform.GetChild(j).GetComponent<Button>().onClick.AddListener(() =>
                player.transform.GetChild(i).GetComponent<Hero>().abilities[j].Activate(player.transform.GetChild(i).gameObject));
            }
        }*/
        StartCoroutine(OnStart());
    }
    void Update()
    {
        if (m_reward_selected) //�����ý� ���þȵȾֵ� ���ư��� ȸ�� �ִϸ��̼�
        {
            m_angle += 20f;
            m_angle %= 360;
            for (int i = 0; i < m_reward_card.Length; i++)
            {
                if (m_reward_card[i] != m_selected_reward_card)
                {
                    m_reward_card[i].transform.rotation = Quaternion.Euler(0, 0, m_angle);
                    m_reward_card[i].transform.DOScale(0, 1.2f);//1.2�ʾȿ� �����
                }
            }
        }
        if (m_panalty_selected)//�г�Ƽ ���ý� ���Ͱ���
        {
            m_angle += 20f;
            m_angle %= 360;
            for (int i = 0; i < m_panalty_card.Length; i++)
            {
                if (m_panalty_card[i] != m_selected_panalty_card)
                {
                    m_panalty_card[i].transform.rotation = Quaternion.Euler(0, 0, m_angle);
                }
            }
        }
    }
    public void PauseButton() //�Ͻ�������ư
    {
        m_pause_panel.SetActive(true);//������ ��ȣ�ۿ� �г�

        Time.timeScale = 0.0f;//Ÿ�ӽ����� 0�� ����
    }
    public void DoubleTime()
    {
        if (Time.timeScale != 2.0f) m_game_speed = 2.0f;
        //�̹��� 2��������ϰ� ����. ������ �Ͻ����� Ǯ������ 2��� �����ؾ��ؼ�
        else m_game_speed = 1.0f;
        Time.timeScale = m_game_speed;
    }
    public void GiveUp()
    {
        m_confirm_panel.SetActive(true);
    }
    public void BackToGameButton()//�ٽ� �������� ���ư���, �� �Ͻ����� ����
    {
        Time.timeScale = m_game_speed;//�� ���Ӽӵ� �ݿ�

        m_pause_panel.SetActive(false);
    }
    public void BackToPause()//�ٽ� �Ͻ������� ���ư���
    {
        m_confirm_panel.SetActive(false);
    }
    public void GiveUpYes()//json �����ϰ� ������.
    {
        StartCoroutine(OnGiveUpYes());
    }
    public void ItemButton()//������ ��ġ��
    {
        if (!m_toggle) {
            StartCoroutine(ItemOpen());

            m_toggle = true;
        }
        else {
            StartCoroutine(ItemRollBack());

            m_toggle = false;
        }
    }
    public void ItemUse()//�����ۼ��ý� ��ȣ�ۿ�, �Ͻ����ΰŰ� ��ųó�� Ȱ���ؾ���.
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        
        m_player.GetComponent<Player>().ActivateItem(m_itemInfos[int.Parse(obj.name)]);
    }

    public void RewardSelect()//������ �ڷ�ƾȣ��
    {
        StartCoroutine(RewardCardMove());
    }
    public void PanaltySelect()//�г�Ƽ���� �ڷ�ƾȣ��
    {
        StartCoroutine(PanaltyCardSpawn());
    }
    public void BackToMap()//�������� ���ư���
    {
        StartCoroutine(OnBackToMap());
    }
    IEnumerator ItemOpen()
    {
        yield return 0;
        for (int i = 0; i < m_item.Count; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i, 0.8f);//������ġ�� �����ϱ� ��Ʈ��
        }
        for (int i = 0; i < m_item.Count-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = true;
        }
    }
    IEnumerator ItemRollBack()
    {
        yield return 0;
        for (int i = 0; i < m_item.Count; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 10 * i, 0.8f);//�ٽ� ������ġ�� ���ư��� ��Ʈ��
        }
        for (int i = 0; i < m_item.Count-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator RewardCardMove()
    {
        m_selected_reward_card = EventSystem.current.currentSelectedGameObject;//�����Ѱ� ���� ����
        m_reward_selected = true;//ȸ������
        m_selected_reward_card.transform.DOMove(new Vector2(0, 0), 0.4f);//����� �̵�
        yield return new WaitForSecondsRealtime(0.4f);
        m_selected_reward_card.transform.DOScale(1.5f, 0.4f);//Ŀ���ٰ�
        yield return new WaitForSecondsRealtime(0.3f);
        m_selected_reward_card.transform.DOScale(0, 0.4f);//�ٽ��پ���
        yield return new WaitForSecondsRealtime(0.4f);
        m_reward_selected = false;
        for (int i = 0; i < m_panalty_card.Length; i++) //�г�Ƽ ��Ÿ����
        {
            m_panalty_card[i].transform.DOScale(1, 0.6f);
        }
        StopCoroutine(RewardCardMove());
    }
    IEnumerator PanaltyCardSpawn()
    {
        m_angle = 0;//���� 0���� �ʱ�ȭ���Ѿ��� �ȱ׷��� ���������� ����
        m_selected_panalty_card = EventSystem.current.currentSelectedGameObject;//�ٽ� �����Ѱ� �г�Ƽ����
        m_panalty_selected = true;
        for (int i = 0; i < m_panalty_card.Length; i++)
        {
            if(m_panalty_card[i] != m_selected_panalty_card) m_panalty_card[i].transform.DOScale(0, 0.6f);//����� ��������~
        }
        m_selected_panalty_card.transform.DOMove(new Vector2(0, 0), 0.4f);
        yield return new WaitForSecondsRealtime(0.4f);
        m_selected_panalty_card.transform.DOScale(1.5f, 0.4f);
        yield return new WaitForSecondsRealtime(0.3f);
        m_selected_panalty_card.transform.DOScale(0, 0.4f);
        m_panalty_selected = false;
        yield return new WaitForSecondsRealtime(0.4f);
        m_back_button.transform.DOScale(1, 0.5f);
        StopCoroutine(PanaltyCardSpawn());
    }

    IEnumerator OnStart()
    {
        yield return null;
        FadeInM();
        m_node = JsonParser.LoadJsonArrayToList<MapNode>("Assets/Resources/MapJson/MapJson");
    }
    
    IEnumerator OnBackToMap()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
        for (int i = 0; i < m_map_obj.transform.childCount; i++)
        {
            m_map_obj.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        if (m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num.Count > 1)
        {
            m_map_obj.transform.GetChild(m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num[1]).GetComponent<Button>().interactable = true;
        }
        m_map_obj.transform.GetChild(m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num[0]).GetComponent<Button>().interactable = true;
        Destroy(GameObject.FindGameObjectWithTag("MapType"));
        SceneManager.LoadScene("Map_Scene");
    }

    IEnumerator OnGiveUpYes()
    {
        Time.timeScale = 1.0f;//Ÿ�ӽ����� 0�� �������� Ǯ����� �۵���
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.1f);

        System.IO.File.Delete("Assets/Resources/MapJson/MapJson.json");
        SceneManager.LoadScene("Saveslot_Scene");
        Destroy(GameObject.FindGameObjectWithTag("MapType"));
        Destroy(GameObject.FindGameObjectWithTag("SaveFileName"));
    }
}

