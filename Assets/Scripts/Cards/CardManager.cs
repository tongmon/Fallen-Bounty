using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드를 주는 로직 담당
public class CardManager
{
    // 모든 종류의 카드가 게임 시작 전에 담김(몬스터만 사용하는 스킬은 따로 담을지 추후에 고려...)
    public List<Card> m_cards;

    // 이름으로 카드를 찾기위한 해쉬맵
    public Dictionary<string, int> m_cards_dict;

    // 사실상 Player
    public GameObject m_game_object;

    public int m_card_option_limit = 3;

    public CardManager(GameObject obj)
    {
        m_game_object = obj;

        // 여기에 json읽어서 AddCard 수행하여 모든 카드 초기화 완료하는 로직이 들어가야 함
        m_cards = JsonParser.LoadJsonArrayToBaseList<Card>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");

        for (int i = 0; i < m_cards.Count; i++)
            m_cards_dict[m_cards[i].card_name] = i;
    }

    /*
    // json에서 값을 읽어와 카드 초기화
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

    // 카드 랜덤 생성
    public List<Card> MakeRandomCards()
    {
        HeroHolder hero_holder = m_game_object.GetComponent<Player>().m_hero_holder;

        List<Card> ret_cards = new List<Card>(), available_cards = new List<Card>();
        int cards_num = 0; // 플레이어에 카드 고를 수 있는 수가 정의되어 이를 cards_num에 넣어줘야 할 것이다.

        // 현재 상태에서 뽑기 가능한 카드로만 추려 available_cards에 넣어줌
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

                        // 영웅이 한명이면 그냥 뽑을 수 있는 카드에 추가
                        if (heroes.Count == 1)
                            available_cards.Add(m_cards[i]);
                        // 해당 스킬을 가지고 있는 영웅이 여러 명이면 현재 사용중인 영웅 중에 한명을 무작위로 뽑아서 타켓으로 지정해줌
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
        // 특정 확률로 ret_cards에 알맞게 카드를 넣어줌
        for (int i = 0; i < cards_num; i++)
        {
            if (ChanceMaker.GetChanceResult(5)) // 퀄 3
                quality_index = 2;
            else if (ChanceMaker.GetChanceResult(15)) // 퀄 2
                quality_index = 1;
            else // 퀄 1
                quality_index = 0;

            while (true)
            {
                Card candidate_card = m_cards[UnityEngine.Random.Range(card_ranges[quality_index].Item1, card_ranges[quality_index].Item2)];
                if (!exsist_card.TryGetValue(candidate_card.card_name, out string name))
                {
                    exsist_card.Add(candidate_card.card_name); // 뽑은 카드 다시 못 뽑게 설정
                    ret_cards.Add(candidate_card);
                    break;
                }
            }
        }

        return ret_cards;
    }

    // 특정 카드 획득
    public void Acquisit(string card_script_name) 
    {
        m_cards[m_cards_dict[card_script_name]].Acquisit(m_game_object);
    }

    // 카드를 퀄리티 기준으로 정렬하고 정렬된 인덱스 범위를 반환함
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