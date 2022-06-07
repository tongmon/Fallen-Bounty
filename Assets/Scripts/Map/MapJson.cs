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
    public class TreeNode //트리로 맵 노드 구현
    {
        public GameObject map_object;//게임오브젝트 정보저장
        public Sprite map_sprite;//저장될 스프라이트
        public eMapType map_type = eMapType.Common; //저장될 맵 종류 - Default : 일반

        public List<TreeNode> children = new List<TreeNode>();

        public TreeNode(string name)
        {
            this.map_object = null;
            this.map_sprite = null;
            this.children = new List<TreeNode>();
        }

    }
    [System.Serializable]
    public class MapInfo 
    {
        public List<Vector3> map_position = new List<Vector3>();//맵 위치 저장 리스트
        public List<Vector3> line_position = new List<Vector3>();//라인위치 저장 리스트

        public TreeNode[] node = new TreeNode[42];//트리
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] Sprite[] m_sprite;
        [SerializeField] GameObject m_prefab_canvas;
        private static string m_save_path => "Assets/Resources/MapJson/";
        private void Start()
        {
            if (!Directory.Exists(m_save_path)) //디렉토리가 없으면
            {
                Directory.CreateDirectory(m_save_path); //만듦
                MapInfo m_mapInfo = new MapInfo();//맵 인포 객체 생성
                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform); //인스턴스 화
                GameObject map = GameObject.FindGameObjectWithTag("Map");//객체 연결

                for (int i = 0; i < 42; i++) {
                    m_mapInfo.node[i] = new TreeNode($"{i}"); //여기서 객체 생성을 따로 해줘야함.
                }

                m_mapInfo.node[0].map_object = map.transform.GetChild(0).gameObject;//부모들
                m_mapInfo.node[1].map_object = map.transform.GetChild(1).gameObject;
                m_mapInfo.node[2].map_object = map.transform.GetChild(2).gameObject;
                m_mapInfo.node[3].map_object = map.transform.GetChild(3).gameObject;

                int pre_index = 0;//이전 인덱스 검사용
                int pre_index1 = 1;
                int pre_index2 = 2;
                int pre_index3 = 3;
                for (int i = 0; i < 40; i++) //맵 경로설정
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f))); //위치 랜덤화
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//랜덤화된 위치 저장

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//길이 설정 먼저하기
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);

                    if (i % 4 == 0 && i != 0)
                    {
                        int a = Random.Range(i,i+3);//노드 넣기용
                        if(a % 4 != 0 && Random.Range(0,1) ==1)
                        {
                            m_mapInfo.node[a].map_object = map.transform.GetChild(a).gameObject;
                            m_mapInfo.node[pre_index].children.Add(m_mapInfo.node[a]);
                        }
                        m_mapInfo.node[i].map_object = map.transform.GetChild(i).gameObject;
                        m_mapInfo.node[pre_index].children.Add(m_mapInfo.node[i]);
                        pre_index = i; //이게 오류가 날거같다
                    }
                    else if (i % 4 == 1 && i != 1 && Random.Range(0, 1) == 1)
                    {
                        int a = Random.Range(i-1, i + 2);//노드 넣기용
                        if (a%4 != 1)
                        {
                            m_mapInfo.node[a].map_object = map.transform.GetChild(a).gameObject;
                            m_mapInfo.node[pre_index1].children.Add(m_mapInfo.node[a]);
                        }
                        m_mapInfo.node[i+1].map_object = map.transform.GetChild(i).gameObject;
                        m_mapInfo.node[pre_index1].children.Add(m_mapInfo.node[i]);
                        pre_index1 = i;
                    }
                    else if (i % 4 == 2 && i != 2 && Random.Range(0, 1) == 1)
                    {
                        int a = Random.Range(i-2, i + 1);//노드 넣기용
                        if (a%4 != 2)
                        {
                            m_mapInfo.node[a+1].map_object = map.transform.GetChild(a).gameObject;
                            m_mapInfo.node[pre_index2].children.Add(m_mapInfo.node[a]);
                        }
                        m_mapInfo.node[i+1].map_object = map.transform.GetChild(i).gameObject;
                        m_mapInfo.node[pre_index2].children.Add(m_mapInfo.node[i]);
                        pre_index2 = i;
                    }
                    else if (i % 4 == 3 && i != 3 && Random.Range(0, 1) == 1)
                    {
                        int a = Random.Range(i-3, i);//노드 넣기용
                        if (a % 4 != 3)
                        {
                            m_mapInfo.node[a+1].map_object = map.transform.GetChild(a).gameObject;
                            m_mapInfo.node[pre_index3].children.Add(m_mapInfo.node[a]);
                        }
                        m_mapInfo.node[i+1].map_object = map.transform.GetChild(i).gameObject;
                        m_mapInfo.node[pre_index3].children.Add(m_mapInfo.node[i]);
                        pre_index3 = i;
                    }

                }

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