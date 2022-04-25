using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드를 주는 로직 담당
public class CardManager
{
    List<Card> m_cards;
    public Dictionary<string, int> m_cards_dict;
    public GameObject m_game_object;

    public CardManager(GameObject obj)
    {
        m_game_object = obj;
    }

    public void AddCard(string card_script_name, string description, int quality)
    {
        Type type = Type.GetType(card_script_name);
        object card_instance = Activator.CreateInstance(type, description, quality);
        m_cards_dict[card_script_name] = m_cards.Count;
        m_cards.Add((Card)card_instance);
    }

    // 특정 카드 획득
    public void Acquisit(string card_script_name) 
    {
        m_cards[m_cards_dict[card_script_name]].Acquisit(m_game_object);
    }
}
