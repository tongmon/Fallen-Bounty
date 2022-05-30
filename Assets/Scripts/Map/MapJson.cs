using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

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
        public List<int> map_path1 = new List<int>();
        public List<int> map_path2= new List<int>();

        public List <Sprite> sprite = new List<Sprite>();
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public List <eMapType> mapType = new List <eMapType>();
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();

        public bool elite_exist = false;
        public bool elite_exist1 = false;
        public bool elite_exist2 = false;
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] GameObject m_map_node;
        [SerializeField] Sprite[] m_sprite;
        private static string m_save_path => "Assets/Resources/MapJson";

        private void Awake()
        {
            if (!Directory.Exists(m_save_path))
            {
                Directory.CreateDirectory(m_save_path);
            }
            MapInfo m_mapInfo = new MapInfo();
            for (int i = 0; i < 30; i += 3)
            {
                m_mapInfo.map_path.Add(Random.Range(i, i + 2));
                m_mapInfo.map_path1.Add(Random.Range(i, i + 2));
                m_mapInfo.map_path2.Add(Random.Range(i, i + 2));
            }
            for(int i=0; i<m_mapInfo.map_path.Count; i++)
            {
                int map_number = Random.Range(0, 6);
                if(map_number == 1 && m_mapInfo.elite_exist == true)
                {
                    while (map_number == 1)
                    {
                        map_number = Random.Range(0, 6);
                    }
                }
                else if (map_number == 1) m_mapInfo.elite_exist = true;
                m_mapInfo.mapType.Add((eMapType)map_number);
                //m_mapInfo.sprite.Add(m_sprite[map_number]);

                int map_number1 = Random.Range(0, 6);
                if (map_number1 == 1 && m_mapInfo.elite_exist1 == true)
                {
                    while (map_number1 == 1)
                    {
                        map_number1 = Random.Range(0, 6);
                    }
                }
                else if (map_number1 == 1) m_mapInfo.elite_exist1 = true;
                m_mapInfo.mapType1.Add((eMapType)map_number1);
                //m_mapInfo.sprite1.Add(m_sprite[map_number]);

                int map_number2 = Random.Range(0, 6);
                if (map_number2 == 1 && m_mapInfo.elite_exist == true)
                {
                    while (map_number2 == 1)
                    {
                        map_number2 = Random.Range(0, 6);
                    }
                }
                else if (map_number2 == 1) m_mapInfo.elite_exist2 = true;
                m_mapInfo.mapType2.Add((eMapType)map_number2);
                //m_mapInfo.sprite2.Add(m_sprite[map_number]);
            }
            string str = JsonUtility.ToJson(m_mapInfo, true);
            Debug.Log(str);
        }
    }
}