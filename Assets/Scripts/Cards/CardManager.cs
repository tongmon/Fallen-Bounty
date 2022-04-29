using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�带 �ִ� ���� ���
public class CardManager
{
    // ��� ������ ī�尡 ���� ���� ���� ���(���͸� ����ϴ� ��ų�� ���� ������ ���Ŀ� ���...)
    public List<Card> m_cards;

    // ī�� ����Ƽ�� ������ ����
    public List<(int, int)> m_quality_range;

    // �̸����� ī�带 ã������ �ؽ���
    public Dictionary<string, int> m_cards_dict;

    // ��ǻ� Player
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
    public void AddCard(string card_script_name, string description, string target, int quality)
    {
        Type type = Type.GetType(card_script_name);
        object card_instance = Activator.CreateInstance(type, description, target, quality);
        m_cards_dict[card_script_name] = m_cards.Count;
        m_cards.Add((Card)card_instance);
    }

    // ī�� ���� ����
    public List<Card> MakeRandomCards()
    {
        HeroHolder hero_holder = m_game_object.GetComponent<Player>().m_hero_holder;

        List<Card> ret_cards = new List<Card>();
        int cards_num = 0; // �÷��̾ ī�� �� �� �ִ� ���� ���ǵǾ� �̸� cards_num�� �־���� �� ���̴�.

        int quality = 0;
        // Ư�� Ȯ���� ret_cards�� �˸°� ī�带 �־���
        for (int i = 0; i < cards_num; i++)
        {
            if (true/* Ư��Ȯ�� */)
                quality = 2;
            else if (true/* Ư��Ȯ�� */)
                quality = 1;
            else
                quality = 0;

            while (true)
            {
                // ������ ����Ƽ�� ���� �ش� ����Ƽ�� �´� ī�� Ǯ���� ī�� ������ ã��
                Card card = m_cards[UnityEngine.Random.Range(m_quality_range[quality].Item1, m_quality_range[quality].Item2)];

                // ��� ������ ����� �� �ִ� ī��
                if (card.m_apply_target == "EveryOne")
                {
                    /* � ������ ������ �ִ��Ŀ� ����� ���� ī���̱⿡ �׳� ����Ʈ�� �߰����ѵ� �Ǵ� ī�� */
                }
                // ���� ��ų�� ���� ī��
                else if (card.m_apply_target == "CommonSkill") 
                {
                    List<Hero> heroes = hero_holder.GetHeroThatHaveTargetAbility(card.m_name);
                    
                }
                else
                {

                }
            }
        }

        return ret_cards;
    }

    // Ư�� ī�� ȹ��
    public void Acquisit(string card_script_name) 
    {
        m_cards[m_cards_dict[card_script_name]].Acquisit(m_game_object);
    }

    public void OrderByQuality(ref List<Card> cards, ref List<(int,int)> quality_range)
    {

    }
}