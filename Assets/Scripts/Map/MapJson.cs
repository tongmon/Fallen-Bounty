using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    public enum eMapType
    {
        Common,
        Elite,
        Store,
        Town,
        Train,
        Battle,
        Cursed,
        Boss
    }
    [System.Serializable]
    public class MapInfo
    {
        public List<int> map_path = new List<int>();
        public List <Sprite> sprite = new List<Sprite>();
        public List <eMapType> mapType = new List <eMapType>();
        public bool elite_exist = false;
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] GameObject m_map_node;
        private void Start()
        {
            MapInfo m_mapInfo1 = new MapInfo();
            MapInfo m_mapInfo2 = new MapInfo();
            MapInfo m_mapInfo3 = new MapInfo();
            for (int i = 3; i < 30; i += 3)//하나로 합쳐보자
            {
                m_mapInfo1.map_path.Add(Random.Range(i, i + 2));
            }
            string str = JsonUtility.ToJson(m_mapInfo1, true);
            string str1 = JsonUtility.ToJson(m_mapInfo1, true);
            string str2 = JsonUtility.ToJson(m_mapInfo1, true);
        }
    }
}