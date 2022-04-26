using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드를 주는 로직 담당
public class CardManager
{
    // 모든 종류의 카드가 게임 시작 전에 담김
    public List<Card> m_cards;

    // 카드 퀼리티가 나뉘는 범위
    public List<(int, int)> m_quality_range;

    // 이름으로 카드를 찾기위한 해쉬맵
    public Dictionary<string, int> m_cards_dict;

    // 사실상 플레이어
    public GameObject m_game_object;

    public CardManager(GameObject obj)
    {
        m_game_object = obj;

        // 여기에 json읽어서 AddCard 수행하여 모든 카드 초기화 완료하는 로직이 들어가야 함

        // 카드를 퀼리티 대로 정렬
        m_cards = m_cards.OrderBy(x => x.m_quality).ToList();
        int back_quality = m_cards[0].m_quality, back_quality_index = 0;
        for (int i = 1; i < m_cards.Count; i++) 
        {
            if(back_quality != m_cards[i].m_quality)
            {
                m_quality_range.Add((back_quality_index, i - 1));
                back_quality = m_cards[i].m_quality;
                back_quality_index = i;
            }
        }
        m_quality_range.Add((back_quality_index, m_cards.Count - 1));
    }

    // json에서 값을 읽어와 카드 초기화
    public void AddCard(string card_script_name, string description, int quality)
    {
        Type type = Type.GetType(card_script_name);
        object card_instance = Activator.CreateInstance(type, description, quality);
        m_cards_dict[card_script_name] = m_cards.Count;
        m_cards.Add((Card)card_instance);
    }

    // 카드 랜덤 생성
    public List<Card> MakeRandomCards()
    {
        List<Card> ret_cards = new List<Card>();
        int cards_num = 0; // 플레이어에 카드 고를 수 있는 수가 정의되어 이를 cards_num에 넣어줘야 할 것이다.

        // 특정 확률로 ret_cards에 알맞게 카드를 넣어줌
        for (int i = 0; i < cards_num; i++)
        {

        }

        return ret_cards;
    }

    // 특정 카드 획득
    public void Acquisit(string card_script_name) 
    {
        m_cards[m_cards_dict[card_script_name]].Acquisit(m_game_object);
    }
}
