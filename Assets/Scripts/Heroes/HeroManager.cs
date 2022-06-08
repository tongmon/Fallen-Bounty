using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager
{
    // ��� ������ ������ ���
    public List<HeroData> m_heroes;
    public Dictionary<string, int> m_heroes_dict;

    // ��ǻ� Player
    public GameObject m_game_object;

    public HeroManager(GameObject obj)
    {
        m_game_object = obj;

        m_heroes = new List<HeroData>();
        m_heroes_dict = new Dictionary<string, int>();

        // json �о�ͼ� ���� �ʱ�ȭ
        m_heroes = JsonParser.LoadJsonArrayToBaseList<HeroData>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");

        for (int i = 0; i < m_heroes.Count; i++)
            m_heroes_dict[m_heroes[i].type_name] = i;

        // xml �о�ͼ� ���� �ʱ�ȭ
        // XmlParser.LoadXml(Application.dataPath + "datafiles/Hero.xml", this);
    }

    /*
    // ���� �߰�(���� ����)
    public void AddHero(string hero_script_name ) // �߰������� �������� �ʿ��� ���� �ʿ�, ����μ���... ���� �̸� �ۿ� ������ �ȳ� 
    {
        Type type = Type.GetType(hero_script_name);
        object ability_instance = Activator.CreateInstance(type); // �� ���� �����ڿ� �´� �߰� ���� �ʿ�
        m_heroes_dict[hero_script_name] = m_heroes.Count;
        m_heroes.Add((Hero)ability_instance);
    }
    */

    // ���� ȹ��
    public HeroData GetHero(string hero_name)
    {
        return m_heroes[m_heroes_dict[hero_name + "Data"]];
    }
}