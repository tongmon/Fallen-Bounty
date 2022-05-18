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
    GameObject m_selected_card;
    bool m_toggle = false;
    float m_game_speed = 1.0f;

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
        for (int i =0; i<m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i , 0.8f);
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
        StartCoroutine(CardMove());
    }
    IEnumerator CardMove() //적 강화카드 나와야함.
    {
        m_selected_card = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < m_reward_card.Length; i++)
        {
            if (m_reward_card[i] != m_selected_card)
            {
                m_reward_card[i].transform.DOLocalMoveX(1200, 0.8f);
            }
        }
        yield return new WaitForSecondsRealtime(0.8f);
        m_selected_card.transform.DOMove(new Vector2(0, 0), 0.8f);
        yield return new WaitForSecondsRealtime(0.8f);
        m_selected_card.transform.DOScale(1.5f, 0.6f);
        yield return new WaitForSecondsRealtime(0.6f);
        m_selected_card.transform.DOScale(0, 0.8f);
    }
}
