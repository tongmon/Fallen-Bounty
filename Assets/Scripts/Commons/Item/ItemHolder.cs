using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder
{
    public List<ItemInfo> m_items;
    public List<ItemInfo> m_sub_items;

    public int m_item_limit;

    public Dictionary<string, int> m_items_dict;
    public Dictionary<string, int> m_sub_items_dict;

    public ItemHolder(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        m_item_limit = 4;

        m_items = new List<ItemInfo>();
        m_sub_items = new List<ItemInfo>();

        m_items_dict = new Dictionary<string, int>();
        m_sub_items_dict = new Dictionary<string, int>();
    }
    public void AddItem(ItemInfo item)
    {
        if (m_items.Count < m_item_limit)
        {
            m_items_dict[item.m_name] = m_items.Count;
            m_items.Add(item);
        }
        else
        {
            m_sub_items_dict[item.m_name] = m_sub_items.Count;
            m_sub_items.Add(item);
        }
    }

    public ItemInfo GetItem(string name_of_item)
    {
        ItemInfo ret_item;
        int index;
        if (m_items_dict.TryGetValue(name_of_item, out index))
            ret_item = m_items[m_items_dict[name_of_item]];
        else
            ret_item = m_sub_items[m_sub_items_dict[name_of_item]];

        return ret_item;
    }
    public void SwitchItem(string item_name_one, string item_name_two)
    {
        int one_item_index, two_item_index;
        ItemInfo item_temp;
        if (m_items_dict.TryGetValue(item_name_one, out one_item_index))
        {
            two_item_index = m_sub_items_dict[item_name_two];

            m_items_dict[item_name_two] = one_item_index;
            m_sub_items_dict[item_name_one] = two_item_index;

            m_items_dict.Remove(item_name_one);
            m_sub_items_dict.Remove(item_name_two);

            item_temp = m_items[one_item_index];
            m_items[one_item_index] = m_sub_items[two_item_index];
            m_sub_items[two_item_index] = item_temp;

            return;
        }
        one_item_index = m_sub_items_dict[item_name_one];
        two_item_index = m_items_dict[item_name_two];

        m_items_dict[item_name_one] = two_item_index;
        m_sub_items_dict[item_name_two] = one_item_index;

        m_items_dict.Remove(item_name_two);
        m_sub_items_dict.Remove(item_name_one);

        item_temp = m_sub_items[one_item_index];
        m_sub_items[one_item_index] = m_items[two_item_index];
        m_items[two_item_index] = item_temp;
    }
}
