using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    // ��� ������ ������ ���
    public List<ItemInfo> m_items;
    public Dictionary<string, int> m_items_dict;

    // ��ǻ� Player
    public GameObject m_game_object;

    public ItemManager(GameObject obj)
    {
        m_game_object = obj;

        m_items = new List<ItemInfo>();
        m_items_dict = new Dictionary<string, int>();

        // json �о�ͼ� ���� �ʱ�ȭ json�ƴϰ� Scriptable������.
        //m_items = JsonParser.LoadJsonArrayToBaseList<HeroData>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");

        for (int i = 0; i < m_items.Count; i++)
            m_items_dict[m_items[i].m_name] = i;
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
    public ItemInfo GetHero(string item_name)
    {
        return m_items[m_items_dict[item_name + "Data"]];//�����ؾ���.
    }
}
