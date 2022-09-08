using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    // 모든 종류의 영웅이 담김
    public List<ItemInfo> m_items;
    public Dictionary<string, int> m_items_dict;

    // 사실상 Player
    public GameObject m_game_object;

    public ItemManager(GameObject obj)
    {
        m_game_object = obj;

        m_items = new List<ItemInfo>();
        m_items_dict = new Dictionary<string, int>();

        // json 읽어와서 영웅 초기화 json아니고 Scriptable쓸거임.
        //m_items = JsonParser.LoadJsonArrayToBaseList<HeroData>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");

        for (int i = 0; i < m_items.Count; i++)
            m_items_dict[m_items[i].m_name] = i;
    }

    /*
    // 영웅 추가(잠정 보류)
    public void AddHero(string hero_script_name ) // 추가적으로 영웅에게 필요한 인자 필요, 현재로서는... 영웅 이름 밖에 생각이 안남 
    {
        Type type = Type.GetType(hero_script_name);
        object ability_instance = Activator.CreateInstance(type); // 각 영웅 생성자에 맞는 추가 인자 필요
        m_heroes_dict[hero_script_name] = m_heroes.Count;
        m_heroes.Add((Hero)ability_instance);
    }
    */

    // 영웅 획득
    public ItemInfo GetHero(string item_name)
    {
        return m_items[m_items_dict[item_name + "Data"]];//수정해야함.
    }
}
