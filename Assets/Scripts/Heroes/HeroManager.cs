using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager
{
    // ��� ������ ������ ���
    public List<Hero> m_heroes;
    public Dictionary<string, int> m_heroes_dict;

    // ��ǻ� Player
    public GameObject m_game_object;

    public HeroManager(GameObject obj)
    {
        m_game_object = obj;

        m_heroes = new List<Hero>();
        m_heroes_dict = new Dictionary<string, int>();
    }

    // ���� �߰�
    public void AddHero(string hero_script_name /* �߰������� �������� �ʿ��� ���� �ʿ�, ����μ���... ���� �̸� �ۿ� ������ �ȳ� */)
    {
        Type type = Type.GetType(hero_script_name);
        object ability_instance = Activator.CreateInstance(type /* �� ���� �����ڿ� �´� �߰� ���� �ʿ� */);
        m_heroes_dict[hero_script_name] = m_heroes.Count;
        m_heroes.Add((Hero)ability_instance);
    }
}