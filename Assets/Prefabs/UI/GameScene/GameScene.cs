using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameScene : MonoBehaviour
{
    [SerializeField] GameObject m_panel;
    [SerializeField] GameObject[] m_item;
    [SerializeField] GameObject[] m_reward_card;
    [SerializeField] GameObject[] m_panalty_card;
    [SerializeField] GameObject m_back_button;
    GameObject m_selected_reward_card;
    GameObject m_selected_panalty_card;
    bool m_toggle = false;
    bool m_reward_selected = false;
    bool m_panalty_selected = false;
    float m_angle = 0;
    float m_game_speed = 1.0f;
    void Update()
    {
        if (m_reward_selected)
        {
            m_angle += 20f;
            m_angle %= 360;
            for (int i = 0; i < m_reward_card.Length; i++)
            {
                if (m_reward_card[i] != m_selected_reward_card)
                {
                    m_reward_card[i].transform.rotation = Quaternion.Euler(0, 0, m_angle);
                    m_reward_card[i].transform.DOScale(0, 1.2f);
                }
            }
        }
        if (m_panalty_selected)
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
    public void PauseButton()
    {
        m_panel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void DoubleTime()
    {
        if (Time.timeScale != 2.0f) m_game_speed = 2.0f;
        else m_game_speed = 1.0f;
        Time.timeScale = m_game_speed;
    }
    public void BackToGameButton()
    {
        Time.timeScale = m_game_speed;
        m_panel.SetActive(false);
    }
    public void ItemButton()
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
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i, 0.8f);
        }
        StopCoroutine(ItemOpen());
    }
    IEnumerator ItemRollBack()
    {
        yield return 0;
        for (int i = 0; i < m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 10 * i, 0.8f);
        }
        StopCoroutine(ItemRollBack());
    }
    public void ItemUse()
    {

    }
    public void RewardSelect()
    {
        StartCoroutine(RewardCardMove());
    }
    public void PanaltySelect()
    {
        StartCoroutine(PanaltyCardSpawn());
    }
    IEnumerator RewardCardMove()
    {
        m_selected_reward_card = EventSystem.current.currentSelectedGameObject;
        m_reward_selected = true;
        m_selected_reward_card.transform.DOMove(new Vector2(0, 0), 0.4f);
        yield return new WaitForSecondsRealtime(0.4f);
        m_selected_reward_card.transform.DOScale(1.5f, 0.4f);
        yield return new WaitForSecondsRealtime(0.3f);
        m_selected_reward_card.transform.DOScale(0, 0.4f);
        yield return new WaitForSecondsRealtime(0.4f);
        m_reward_selected = false;
        for (int i = 0; i < m_panalty_card.Length; i++) //나타나기
        {
            m_panalty_card[i].transform.DOScale(1, 0.6f);
        }
        StopCoroutine(RewardCardMove());
    }
    IEnumerator PanaltyCardSpawn()
    {
        m_angle = 0;
        m_selected_panalty_card = EventSystem.current.currentSelectedGameObject;
        m_panalty_selected = true;
        for (int i = 0; i < m_panalty_card.Length; i++)
        {
            if(m_panalty_card[i] != m_selected_panalty_card) m_panalty_card[i].transform.DOScale(0, 0.6f);
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
    public void BackToMap()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map_Scene");
    }
}

