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
        public List<GameObject> map_path = new List<GameObject>(); //맵 경로 저장 리스트
        public List<GameObject> map_path1 = new List<GameObject>(); 
        public List<GameObject> map_path2 = new List<GameObject>(); 

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
        [SerializeField] Sprite[] m_sprite;
        [SerializeField] GameObject m_prefab_canvas;
        [SerializeField] GameObject m_saved_map;
        LineRenderer l;//이걸써보자
        private static string m_save_path => "Assets/Resources/MapJson/";

        private void Start()
        {
            Instantiate(l, m_prefab_canvas.transform);//라인을 따로 생성가능 이걸 이용해야함
            if (!Directory.Exists(m_save_path)) //디렉토리가 없으면
            {
                Directory.CreateDirectory(m_save_path); //만듦
                MapInfo m_mapInfo = new MapInfo();//맵 인포 객체 생성

                for(int i =0; i < 30; i++) //저장맵 초기화
                {
                    m_saved_map.transform.GetChild(i).transform.position = Resources.Load<GameObject>("MapPrefab").transform.GetChild(i).transform.position;
                }
                Instantiate(m_saved_map, m_prefab_canvas.transform); //인스턴스 화
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                for (int i = 0; i < 30; i += 3) //맵 경로설정
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))); //초기화와 동시에 위치 랜덤화
                    map.transform.GetChild(i+1).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))); 
                    map.transform.GetChild(i+2).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)));
                    m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject); //얘네 수정 해야됨.
                    m_mapInfo.map_path1.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject);
                    m_mapInfo.map_path2.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject);
                }
                m_mapInfo.map_path.Add(map.transform.GetChild(30).gameObject);//보스 노드 넣기
                m_mapInfo.map_path1.Add(map.transform.GetChild(30).gameObject);
                m_mapInfo.map_path2.Add(map.transform.GetChild(30).gameObject);

                for (int i = 0; i < m_mapInfo.map_path.Count-1; i++) //랜덤 맵타입 삽입
                {
                    int map_number = Random.Range(0, 6);
                    if (map_number == 1 && m_mapInfo.elite_exist == true)
                    {
                        while (map_number == 1) //엘리트는 유일해야함
                        {
                            map_number = Random.Range(0, 6);
                        }
                    }
                    else if (map_number == 1) m_mapInfo.elite_exist = true;//엘리트 있음
                    m_mapInfo.mapType.Add((eMapType)map_number); //그 맵타입 저장
                    //m_mapInfo.sprite.Add(m_sprite[map_number]); //UI삽입인데 아직 스프라이트가 없음
                }
                for (int i = 0; i < m_mapInfo.map_path1.Count-1; i++) 
                {
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
                }
                for (int i = 0; i < m_mapInfo.map_path2.Count-1; i++)
                {
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
                m_mapInfo.mapType.Add(eMapType.Boss);//맵종류 보스넣기
                m_mapInfo.mapType1.Add(eMapType.Boss);
                m_mapInfo.mapType2.Add(eMapType.Boss);

                string save_json = JsonUtility.ToJson(m_mapInfo, true); //json으로 변환
                string save_file_path = m_save_path + "MapJson.json"; //경로설정
                File.WriteAllText(save_file_path, save_json); //json 생성
                Debug.Log(save_json);//확인용
            }
            else
            {
                string save_file_path = m_save_path + "MapJson.json";//json 경로
                string save_file = File.ReadAllText(save_file_path); //json 읽기
                MapInfo m_mapInfo = JsonUtility.FromJson<MapInfo>(save_file); //json 적용
                Debug.Log(save_file);
                Instantiate(m_saved_map, m_prefab_canvas.transform);//저장된 맵 인스턴스화
            }
        }
    }
}