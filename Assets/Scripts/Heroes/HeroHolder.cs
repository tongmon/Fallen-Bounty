using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHolder
{
    // Player�� �����ϰ� �ִ� �������� ���
    public List<Hero> m_heroes;
    public List<Hero> m_sub_heroes;

    // ���� ��� ���� �� ���� ����
    public int m_hero_limit;

    public Dictionary<string, int> m_heroes_dict;
    public Dictionary<string, int> m_sub_heroes_dict;

    // ��ǻ� Player
    public GameObject m_game_object;

    public HeroHolder(GameObject obj)
    {
        m_game_object = obj;
        Player player = obj.GetComponent<Player>();
        m_hero_limit = player.m_hero_limit;

        m_heroes = new List<Hero>();
        m_sub_heroes = new List<Hero>();

        m_heroes_dict = new Dictionary<string, int>();
        m_sub_heroes_dict = new Dictionary<string, int>();
    }

    // ���� �߰�
    public void AddHero(Hero hero)
    {
        if (m_heroes.Count < m_hero_limit)
        {
            m_heroes_dict[hero.m_name] = m_heroes.Count;
            m_heroes.Add(hero);
        }
        else
        {
            m_sub_heroes_dict[hero.m_name] = m_sub_heroes.Count;
            m_sub_heroes.Add(hero);
        }
    }

    // ���� �̸����� ���� ȹ��
    public Hero GetHero(string name_of_hero)
    {
        Hero ret_hero;
        int index;
        if (m_heroes_dict.TryGetValue(name_of_hero, out index))
            ret_hero = m_heroes[m_heroes_dict[name_of_hero]];
        else
            ret_hero = m_sub_heroes[m_sub_heroes_dict[name_of_hero]];

        return ret_hero;
    }

    // ���� ��ü
    public void SwitchHero(string hero_name_one, string hero_name_two)
    {
        int one_hero_index, two_hero_index;
        Hero hero_temp;
        if (m_heroes_dict.TryGetValue(hero_name_one, out one_hero_index))
        {
            two_hero_index = m_sub_heroes_dict[hero_name_two];

            m_heroes_dict[hero_name_one] = two_hero_index;
            m_sub_heroes_dict[hero_name_two] = one_hero_index;

            hero_temp = m_heroes[one_hero_index];
            m_heroes[one_hero_index] = m_sub_heroes[two_hero_index];
            m_sub_heroes[two_hero_index] = hero_temp;

            return;
        }
        one_hero_index = m_sub_heroes_dict[hero_name_one];
        two_hero_index = m_heroes_dict[hero_name_two];

        m_heroes_dict[hero_name_two] = one_hero_index;
        m_sub_heroes_dict[hero_name_one] = two_hero_index;

        hero_temp = m_sub_heroes[one_hero_index];
        m_sub_heroes[one_hero_index] = m_heroes[two_hero_index];
        m_heroes[two_hero_index] = hero_temp;
    }
}