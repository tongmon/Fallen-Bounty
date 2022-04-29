using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager
{
    // 모든 종류의 영웅이 담김
    public List<Hero> m_heroes;
    public Dictionary<string, int> m_heroes_dict;

    // 사실상 Player
    public GameObject m_game_object;

    public HeroManager(GameObject obj)
    {
        m_game_object = obj;

        m_heroes = new List<Hero>();
        m_heroes_dict = new Dictionary<string, int>();

        // json 읽어와서 영웅 초기화
        string filepath = Application.dataPath + "datafiles/Hero.xml";
        //XmlParser.LoadXml(filepath, "Hero", ref this);
    }

    // 영웅 추가
    public void AddHero(string hero_script_name /* 추가적으로 영웅에게 필요한 인자 필요, 현재로서는... 영웅 이름 밖에 생각이 안남 */)
    {
        Type type = Type.GetType(hero_script_name);
        object ability_instance = Activator.CreateInstance(type /* 각 영웅 생성자에 맞는 추가 인자 필요 */);
        m_heroes_dict[hero_script_name] = m_heroes.Count;
        m_heroes.Add((Hero)ability_instance);
    }

    // 영웅 획득
    public Hero GetHero(string hero_script_name)
    {
        return m_heroes[m_heroes_dict[hero_script_name]];
    }
}