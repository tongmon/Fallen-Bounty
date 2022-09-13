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

    // �̸����� ī�带 ã������ �ؽ���
    public Dictionary<string, int> m_cards_dict;

    // ��ǻ� Player
    public GameObject m_game_object;

    public int m_card_option_limit = 3;

    public CardManager(GameObject obj)
    {
        m_game_object = obj;

        // ���⿡ json�о AddCard �����Ͽ� ��� ī�� �ʱ�ȭ �Ϸ��ϴ� ������ ���� ��
        m_cards = JsonParser.LoadJsonArrayToBaseList<Card>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");

        for (int i = 0; i < m_cards.Count; i++)
            m_cards_dict[m_cards[i].card_name] = i;
    }

    /*
    // json���� ���� �о�� ī�� �ʱ�ȭ
    public void AddCard(string card_script_name, string description, string target, int quality)
    {
        Type type = Type.GetType(card_script_name);
        object card_instance = Activator.CreateInstance(type, description, target, quality);
        m_cards_dict[card_script_name] = m_cards.Count;
        m_cards.Add((Card)card_instance);
    }
    */

    public Card GetCard(string card_name)
    {
        return m_cards[m_cards_dict[card_name]];
    }

    // ī�� ���� ����
    public List<Card> MakeRandomCards()
    {
        HeroHolder hero_holder = m_game_object.GetComponent<Player>().m_hero_holder;

        List<Card> ret_cards = new List<Card>(), available_cards = new List<Card>();
        int cards_num = 0; // �÷��̾ ī�� �� �� �ִ� ���� ���ǵǾ� �̸� cards_num�� �־���� �� ���̴�.

        // ���� ���¿��� �̱� ������ ī��θ� �߷� available_cards�� �־���
        for (int i = 0; i < m_cards.Count; i++)
        {
            switch(m_cards[i].apply_category)
            {
                case "Everything":
                    available_cards.Add(m_cards[i]);
                    break;
                case "Ability":
                    {
                        List<Hero> heroes = hero_holder.GetHeroThatHaveTargetAbility(m_cards[i].apply_target);

                        // ������ �Ѹ��̸� �׳� ���� �� �ִ� ī�忡 �߰�
                        if (heroes.Count == 1)
                            available_cards.Add(m_cards[i]);
                        // �ش� ��ų�� ������ �ִ� ������ ���� ���̸� ���� ������� ���� �߿� �Ѹ��� �������� �̾Ƽ� Ÿ������ ��������
                        else if (heroes.Count > 1)
                        {
                            m_cards[i].apply_hero = ((HeroData)hero_holder.m_heroes[UnityEngine.Random.Range(0, hero_holder.m_heroes.Count - 1)].m_data).type_name;
                            available_cards.Add(m_cards[i]);
                        }
                    }
                    break;
                case "Passive":
                    break;
                case "Immune":
                    break;
            }
        }

        List<(int, int)> card_ranges = OrderByQuality(ref available_cards);
        HashSet<string> exsist_card = new HashSet<string>();

        int quality_index = 0;
        // Ư�� Ȯ���� ret_cards�� �˸°� ī�带 �־���
        for (int i = 0; i < cards_num; i++)
        {
            if (ChanceMaker.GetChanceResult(5)) // �� 3
                quality_index = 2;
            else if (ChanceMaker.GetChanceResult(15)) // �� 2
                quality_index = 1;
            else // �� 1
                quality_index = 0;

            while (true)
            {
                Card candidate_card = m_cards[UnityEngine.Random.Range(card_ranges[quality_index].Item1, card_ranges[quality_index].Item2)];
                if (!exsist_card.TryGetValue(candidate_card.card_name, out string name))
                {
                    exsist_card.Add(candidate_card.card_name); // ���� ī�� �ٽ� �� �̰� ����
                    ret_cards.Add(candidate_card);
                    break;
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

    // ī�带 ����Ƽ �������� �����ϰ� ���ĵ� �ε��� ������ ��ȯ��
    public List<(int, int)> OrderByQuality(ref List<Card> cards)
    {
        List<(int, int)> quality_range = new List<(int, int)>();

        cards = cards.OrderBy(x => x.quality).ToList();
        int back_quality = cards[0].quality, back_quality_index = 0;
        for (int i = 1; i < cards.Count; i++)
        {
            if (back_quality != cards[i].quality)
            {
                quality_range.Add((back_quality_index, i - 1));
                back_quality = cards[i].quality;
                back_quality_index = i;
            }
        }
        quality_range.Add((back_quality_index, cards.Count - 1));

        return quality_range;
    }

    
}