using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder
{
    public List<ItemInfo> m_items;
    public List<ItemInfo> m_sub_items;

    public int m_item_limit;
    
    public ItemHolder(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();
        m_item_limit = 4;
    }
}
