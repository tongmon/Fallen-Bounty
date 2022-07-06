using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameScene : MonoBehaviour
{
    [SerializeField] GameObject m_panel; //�� �г��� ����Ŭ���Ǿ��ϹǷ� ���ο� �������� �г��̿�
    [SerializeField] GameObject[] m_item; //������ ����Ʈ
    [SerializeField] GameObject[] m_reward_card; //�������� Ŭ����� �����ϴ� ���� ī�帮��Ʈ
    [SerializeField] GameObject[] m_panalty_card;//�г�Ƽ ī�� ����Ʈ
    [SerializeField] GameObject m_back_button;//�������� ������ ���ư����ư
    GameObject m_selected_reward_card;//���� ������ ����
    GameObject m_selected_panalty_card;//���� ������ �г�Ƽ
    bool m_toggle = false;//�Ͻ����� ��ۿ� �οﺯ��
    bool m_reward_selected = false;//���� �������� �οﺯ��
    bool m_panalty_selected = false;//�г�Ƽ �������� �οﺯ��
    float m_angle = 0;//�ִϸ��̼ǿ� ��������
    float m_game_speed = 1.0f;//���Ӽӵ� ����� ���� 
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
        m_panel.SetActive(true);//������ ��ȣ�ۿ� �г�
        Time.timeScale = 0.0f;//Ÿ�ӽ����� 0�� ����
    }
    public void DoubleTime()
    {
        if (Time.timeScale != 2.0f) m_game_speed = 2.0f;
        //�̹��� 2��������ϰ� ����. ������ �Ͻ����� Ǯ������ 2��� �����ؾ��ؼ�
        else m_game_speed = 1.0f;
        Time.timeScale = m_game_speed;
    }
    public void BackToGameButton()//�ٽ� �������� ���ư���, �� �Ͻ����� ����
    {
        Time.timeScale = m_game_speed;//�� ���Ӽӵ� �ݿ�
        m_panel.SetActive(false);
    }
    public void ItemButton()//������ ������
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
    IEnumerator ItemOpen()
    {
        yield return 0;
        for (int i = 0; i < m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i, 0.8f);//������ġ�� �����ϱ� ��Ʈ��
        }
        StopCoroutine(ItemOpen());
    }
    IEnumerator ItemRollBack()
    {
        yield return 0;
        for (int i = 0; i < m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 10 * i, 0.8f);//�ٽ� ������ġ�� ���ư��� ��Ʈ��
        }
        StopCoroutine(ItemRollBack());
    }
    public void ItemUse()//�����ۼ��ý� ��ȣ�ۿ� , �̰�
    {

    }
    public void RewardSelect()//������ �ڷ�ƾȣ��
    {
        StartCoroutine(RewardCardMove());
    }
    public void PanaltySelect()//�г�Ƽ���� �ڷ�ƾȣ��
    {
        StartCoroutine(PanaltyCardSpawn());
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
    public void BackToMap()//�������� ���ư���
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map_Scene");
    }
}

