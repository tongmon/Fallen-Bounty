using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�带 �ִ� ���� ���
public class CardManager
{
    // ��� ������ ī�尡 ���� ���� ���� ���
    public List<Card> m_cards;

    // ī�� ����Ƽ�� ������ ����
    public List<(int, int)> m_quality_range;

    // �̸����� ī�带 ã������ �ؽ���
    public Dictionary<string, int> m_cards_dict;

    // ��ǻ� �÷��̾�
    public GameObject m_game_object;

    public CardManager(GameObject obj)
    {
        m_game_object = obj;

        // ���⿡ json�о AddCard �����Ͽ� ��� ī�� �ʱ�ȭ �Ϸ��ϴ� ������ ���� ��

        // ī�带 ����Ƽ ��� ����
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

    // json���� ���� �о�� ī�� �ʱ�ȭ
    public void AddCard(string card_script_name, string description, int quality)
    {
        Type type = Type.GetType(card_script_name);
        object card_instance = Activator.CreateInstance(type, description, quality);
        m_cards_dict[card_script_name] = m_cards.Count;
        m_cards.Add((Card)card_instance);
    }

    // ī�� ���� ����
    public List<Card> MakeRandomCards()
    {
        List<Card> ret_cards = new List<Card>();
        int cards_num = 0; // �÷��̾ ī�� �� �� �ִ� ���� ���ǵǾ� �̸� cards_num�� �־���� �� ���̴�.

        // Ư�� Ȯ���� ret_cards�� �˸°� ī�带 �־���
        for (int i = 0; i < cards_num; i++)
        {

        }

        return ret_cards;
    }

    // Ư�� ī�� ȹ��
    public void Acquisit(string card_script_name) 
    {
        m_cards[m_cards_dict[card_script_name]].Acquisit(m_game_object);
    }
}
