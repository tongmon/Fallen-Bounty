using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO; //파일 입출력용 라이브러리

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

    public class TreeNode //트리구현
    {
        public GameObject map { get; set; }
        public List<TreeNode> children;
        public void AddChild(TreeNode child)
        {
            children.Add(child);
        }
    }

    [System.Serializable]
    public class MapInfo 
    {
        public List<Vector3> map_position = new List<Vector3>();//맵 위치 저장 리스트

        public TreeNode[] m_node = new TreeNode[42]; //맵 경로

        public List<eMapType> mapType = new List <eMapType>(); //맵타입 적용 리스트
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();
        public List<eMapType> mapType3 = new List<eMapType>();

        public List<Vector3> line_position = new List<Vector3>();//라인위치 저장 리스트

        public List<Sprite> sprite = new List<Sprite>(); //스트라이트 리스트
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public bool elite_exist = false; //엘리트 확인용 부울 변수
        public bool elite_exist1 = false; //엘리트 확인용 부울 변수
        public bool elite_exist2 = false; //엘리트 확인용 부울 변수
        public bool elite_exist3 = false; //엘리트 확인용 부울 변수

        public int node_count = 1; //노드갯수 확인용 인티져변수
        public int node_count1 = 1;
        public int node_count2 = 1;
        public int node_count3 = 1;
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

                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[1]);//경로 4개 생성, 0은 루트
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[2]);
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[3]);
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[4]);

                m_mapInfo.m_node[1].map = map.transform.GetChild(0).gameObject;//첫번째 맵만 넣어줌
                m_mapInfo.m_node[2].map = map.transform.GetChild(1).gameObject;
                m_mapInfo.m_node[3].map = map.transform.GetChild(2).gameObject;
                m_mapInfo.m_node[4].map = map.transform.GetChild(3).gameObject;

                for (int i = 4; i < 40; i++) //맵 경로설정
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f))); //위치 랜덤화
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//랜덤화된 위치 저장

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//길이 설정 먼저하기
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);
                    //m_mapInfo.m_node[i].children[0].
                    if(i % 4 == 0)
                    {
                        //m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i, i + 3)).gameObject);
                    }
                    else if(i % 4 == 1)
                    {
                        //m_mapInfo.map_path1.Add(map.transform.GetChild(Random.Range(i - 1, i + 2)).gameObject);
                    }
                    else if(i % 4 == 2)
                    {
                        //m_mapInfo.map_path2.Add(map.transform.GetChild(Random.Range(i - 2, i + 1)).gameObject);
                    }
                    else if (i % 4 == 3)
                    {
                        //m_mapInfo.map_path3.Add(map.transform.GetChild(Random.Range(i - 3, i)).gameObject);
                    }
                }
                for (int i=0; i < 40; i++) //선긋기
                {
                    m_mapInfo.line_position.Add(map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1)); //정해진 경로의 포지션 가져오기
                    if (map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).x == 0 && map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).y == 0)
                    { //선이 안 그어진 애들 삭제
                        Destroy(map.transform.GetChild(i).gameObject);
                    }
                }
                m_mapInfo.mapType.Add(eMapType.Boss);//맵종류 보스넣기, 다시해야함
                m_mapInfo.mapType1.Add(eMapType.Boss);
                m_mapInfo.mapType2.Add(eMapType.Boss);
                m_mapInfo.mapType3.Add(eMapType.Boss);

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

                for(int i = 0; i < 40; i++)//위치 일치화, 줄 긋기가 필요함
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