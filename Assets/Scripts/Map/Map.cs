using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    /*
     �ʿ��Ѱ� : �� ����� ����Ʈ, �� �˻�� �迭, �� ����, ���� ��� ��ġ, ���� ���. 
     */
    public class Map : ScriptableObject
    {
        List<List<string>> m_map = new List<List<string>>();
    }
    public enum MapType
    {
        Common,
        Elite,
        Train,
        Battle,
        Town,
        Store,
        Cursed,
        Boss
    }
    public class MapInfo
    {
        public MapType m_map_type;
        public int m_distance;
    } 

    public class MapSetting
    {
        
    }
}