using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    // ��� ������ �������� ���
    public List<ItemInfo> m_items;

    public Dictionary<string, int> m_items_dict;

    // Player
    public GameObject m_game_object;

    public ItemManager(GameObject obj, ItemInfo[] items)
    {
        m_game_object = obj;

        m_items = new List<ItemInfo>();
        m_items_dict = new Dictionary<string, int>();

        for (int i = 0; i < items.Length; i++) 
            m_items.Add(items[i]);

        for (int i = 0; i < m_items.Count; i++)
            m_items_dict[m_items[i].m_info] = i;
    }


    // ������ ����
    public ItemInfo GetItem(string item_name)
    {
        return m_items[m_items_dict[item_name + "Data"]];//�����ؾ���.
    }
}
