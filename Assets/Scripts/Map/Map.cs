using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    /*
     필요한것 : 맵 저장용 리스트, 맵 검사용 배열, 맵 종류, 현재 노드 위치, 보스 노드. 
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