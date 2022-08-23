using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eMapType
{
    Common,
    Elite,
    Town,
    Train,
    Battle,
    Store,
    Random,
    Boss
}

[CreateAssetMenu]
public class MapNode : ScriptableObject
{
    public int m_num;
    public Vector3 m_position;//위치, 선위치.
    public Sprite m_sprite;
    public eMapType m_mapType;
    public List <int> m_parent_num;
    public List <int> m_child_num;

    public MapNode()
    {
        m_mapType = eMapType.Common;
        m_parent_num = new List<int>();
        m_child_num = new List<int>();
        this.m_position = new Vector3(0,0,0);
        this.m_sprite = null;
    }
    
}
