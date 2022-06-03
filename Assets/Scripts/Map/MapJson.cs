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
        public List<Vector3> map_position = new List<Vector3>();//맵 위치 저장 리스트

        public List<GameObject> map_path = new List<GameObject>(); //맵 경로 저장 리스트
        public List<GameObject> map_path1 = new List<GameObject>(); 
        public List<GameObject> map_path2 = new List<GameObject>(); 

        public List <eMapType> mapType = new List <eMapType>(); //맵타입 적용 리스트
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();

        public List<Vector3> line_position = new List<Vector3>();//라인위치 저장 리스트

        public List<Sprite> sprite = new List<Sprite>(); //스트라이트 리스트
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public bool elite_exist = false; //엘리트 확인용 부울변수
        public bool elite_exist1 = false;
        public bool elite_exist2 = false;
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] Sprite[] m_sprite;
        [SerializeField] GameObject m_prefab_canvas;
        private static string m_save_path => "Assets/Resources/MapJson/";
        void GetNumber(int num)
        {

        }
        private void Start()
        {
            if (!Directory.Exists(m_save_path)) //디렉토리가 없으면
            {
                Directory.CreateDirectory(m_save_path); //만듦
                MapInfo m_mapInfo = new MapInfo();//맵 인포 객체 생성
                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform); //인스턴스 화
                GameObject map = GameObject.FindGameObjectWithTag("Map");//객체 연결
                for (int i = 0; i < 75; i++) //맵 경로설정
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f))); //위치 랜덤화
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//랜덤화된 위치 저장

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//길이 설정 먼저하기
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);

                    if(i % 3 == 0)
                    {
                        m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject);
                    }
                    else if(i % 3 == 1)
                    {
                        m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i-1, i + 1)).gameObject);
                    }
                    else if(i % 3 == 2)
                    {
                        m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i-2, i)).gameObject);
                    }
                }
                m_mapInfo.map_path.Add(map.transform.GetChild(75).gameObject);//보스 노드 넣기
                m_mapInfo.map_path1.Add(map.transform.GetChild(75).gameObject);
                m_mapInfo.map_path2.Add(map.transform.GetChild(75).gameObject);

                for (int i = 0; i < m_mapInfo.map_path.Count-1; i++) //랜덤 맵타입 삽입
                {
                    m_mapInfo.map_path[i].GetComponent<LineRenderer>().SetPosition(1,m_mapInfo.map_path[i+1].transform.position);
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
                    m_mapInfo.map_path1[i].GetComponent<LineRenderer>().SetPosition(1, m_mapInfo.map_path1[i + 1].transform.position);
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
                    m_mapInfo.map_path2[i].GetComponent<LineRenderer>().SetPosition(1, m_mapInfo.map_path2[i + 1].transform.position);
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
                for(int i=0; i < 75; i++)
                {
                    m_mapInfo.line_position.Add(map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1));
                    if (map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).x == 0 && map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).y == 0)
                    {
                        Destroy(map.transform.GetChild(i).gameObject);
                    }
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

                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform);//저장된 맵 인스턴스화
                GameObject map = GameObject.FindGameObjectWithTag("Map");//다시 찾아서 연결

                for(int i = 0; i < 75; i++)//위치 일치화, 줄 긋기가 필요함
                {
                    map.transform.GetChild(i).transform.position = m_mapInfo.map_position[i];
                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    if (m_mapInfo.line_position[i].x == 0 && m_mapInfo.line_position[i].y == 0)
                    {
                        Destroy(map.transform.GetChild(i).gameObject);//줄 없는애들 삭제  
                    }
                    else
                    {
                        map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);
                        map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(1, m_mapInfo.line_position[i]);
                    }
                }
            }
        }
    }
}