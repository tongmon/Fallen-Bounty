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
    public MapNode m_parent;
    public MapNode m_children;

    public MapNode()
    {
        m_mapType = eMapType.Common;
        m_parent = null;
        m_children = null;
        this.m_object = null;
        this.m_sprite = null;
    }
}
