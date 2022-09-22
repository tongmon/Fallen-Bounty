using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager
{
    // ��� ������ ������ ���
    public List<Hero> m_heroes;

    public Dictionary<string, int> m_heroes_dict;

    public HeroManager(GameObject obj, Hero[] hero_sample)
    {
        m_heroes_dict = new Dictionary<string, int>();
        m_heroes = new List<Hero>();

        for(int i = 0;i<hero_sample.Length; i++)
        {
            m_heroes.Add(hero_sample[i]);
        }

        for (int i = 0; i < m_heroes.Count; i++)
            m_heroes_dict[m_heroes[i].name] = i;
    }

    // ���� ȹ��
    public Hero GetHero(string hero_name)
    {
        return m_heroes[m_heroes_dict[hero_name]];
    }
}