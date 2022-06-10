using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonSubTypes;
using Newtonsoft.Json;


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
[JsonConverter(typeof(JsonSubtypes))]
public class MapNode
{
    public int m_num;
    public GameObject m_object;
    public Sprite m_sprite;
    public eMapType m_mapType;
    public List<MapNode> m_parent;
    public List<MapNode> m_children;

    public MapNode()
    {
        m_mapType = eMapType.Common; 
        m_parent = new List<MapNode>();
        m_children = new List<MapNode>();
        this.m_object = null;
        this.m_sprite = null;
    }
}
