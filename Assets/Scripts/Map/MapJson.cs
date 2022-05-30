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
        public List<int> map_path = new List<int>(); //맵 경로 저장 리스트
        public List<int> map_path1 = new List<int>();
        public List<int> map_path2= new List<int>();

        public List <Sprite> sprite = new List<Sprite>(); //스트라이트 리스트
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public List <eMapType> mapType = new List <eMapType>(); //맵타입 적용 리스트
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();

        public bool elite_exist = false; //엘리트 확인용 부울변수
        public bool elite_exist1 = false;
        public bool elite_exist2 = false;
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] GameObject m_map_node;
        [SerializeField] Sprite[] m_sprite;
        private static string m_save_path => "Assets/Resources/MapJson/";

        private void Awake()
        {
            if (!Directory.Exists(m_save_path)) //디렉토리가 없으면
            {
                Directory.CreateDirectory(m_save_path); //만듦
                MapInfo m_mapInfo = new MapInfo();
                for (int i = 0; i < 30; i += 3)
                {
                    m_mapInfo.map_path.Add(Random.Range(i, i + 2)); //랜덤으로 경로설정 * 3
                    m_mapInfo.map_path1.Add(Random.Range(i, i + 2));
                    m_mapInfo.map_path2.Add(Random.Range(i, i + 2));
                }
                for (int i = 0; i < m_mapInfo.map_path.Count; i++)
                {
                    int map_number = Random.Range(0, 6);
                    if (map_number == 1 && m_mapInfo.elite_exist == true)
                    {
                        while (map_number == 1) //엘리트는 유일해야함
                        {
                            map_number = Random.Range(0, 6); 
                        }
                    }
                    else if (map_number == 1) m_mapInfo.elite_exist = true;
                    m_mapInfo.mapType.Add((eMapType)map_number); //그 맵타입 저장
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
                string save_json = JsonUtility.ToJson(m_mapInfo, true); //json 저장
                string save_file_path = m_save_path + "MapJson.json"; //경로설정
                File.WriteAllText(save_file_path, save_json); //json 생성
                Debug.Log(save_json);//확인용
            }
            else
            {
                string save_file_path = m_save_path + "MapJson.json";//json 경로
                string save_file = File.ReadAllText(save_file_path); //json 읽기
                MapInfo mapInfo = JsonUtility.FromJson<MapInfo>(save_file); //json 적용
            }
        }
    }
}