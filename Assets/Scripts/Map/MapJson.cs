using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO; //���� ����¿� ���̺귯��

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

    public class TreeNode //Ʈ������
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
        public List<Vector3> map_position = new List<Vector3>();//�� ��ġ ���� ����Ʈ

        public TreeNode[] m_node = new TreeNode[42]; //�� ���

        public List<eMapType> mapType = new List <eMapType>(); //��Ÿ�� ���� ����Ʈ
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();
        public List<eMapType> mapType3 = new List<eMapType>();

        public List<Vector3> line_position = new List<Vector3>();//������ġ ���� ����Ʈ

        public List<Sprite> sprite = new List<Sprite>(); //��Ʈ����Ʈ ����Ʈ
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public bool elite_exist = false; //����Ʈ Ȯ�ο� �ο� ����
        public bool elite_exist1 = false; //����Ʈ Ȯ�ο� �ο� ����
        public bool elite_exist2 = false; //����Ʈ Ȯ�ο� �ο� ����
        public bool elite_exist3 = false; //����Ʈ Ȯ�ο� �ο� ����

        public int node_count = 1; //��尹�� Ȯ�ο� ��Ƽ������
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
            if (!Directory.Exists(m_save_path)) //���丮�� ������
            {
                Directory.CreateDirectory(m_save_path); //����
                MapInfo m_mapInfo = new MapInfo();//�� ���� ��ü ����
                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform); //�ν��Ͻ� ȭ
                GameObject map = GameObject.FindGameObjectWithTag("Map");//��ü ����

                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[1]);//��� 4�� ����, 0�� ��Ʈ
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[2]);
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[3]);
                m_mapInfo.m_node[0].AddChild(m_mapInfo.m_node[4]);

                m_mapInfo.m_node[1].map = map.transform.GetChild(0).gameObject;//ù��° �ʸ� �־���
                m_mapInfo.m_node[2].map = map.transform.GetChild(1).gameObject;
                m_mapInfo.m_node[3].map = map.transform.GetChild(2).gameObject;
                m_mapInfo.m_node[4].map = map.transform.GetChild(3).gameObject;

                for (int i = 4; i < 40; i++) //�� ��μ���
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f))); //��ġ ����ȭ
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//����ȭ�� ��ġ ����

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//���� ���� �����ϱ�
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
                for (int i=0; i < 40; i++) //���߱�
                {
                    m_mapInfo.line_position.Add(map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1)); //������ ����� ������ ��������
                    if (map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).x == 0 && map.transform.GetChild(i).GetComponent<LineRenderer>().GetPosition(1).y == 0)
                    { //���� �� �׾��� �ֵ� ����
                        Destroy(map.transform.GetChild(i).gameObject);
                    }
                }
                m_mapInfo.mapType.Add(eMapType.Boss);//������ �����ֱ�, �ٽ��ؾ���
                m_mapInfo.mapType1.Add(eMapType.Boss);
                m_mapInfo.mapType2.Add(eMapType.Boss);
                m_mapInfo.mapType3.Add(eMapType.Boss);

                string save_json = JsonUtility.ToJson(m_mapInfo, true); //json���� ��ȯ
                string save_file_path = m_save_path + "MapJson.json"; //��μ���
                File.WriteAllText(save_file_path, save_json); //json ����
                Debug.Log(save_json);//Ȯ�ο�
            }
            else
            {
                string save_file_path = m_save_path + "MapJson.json";//json ���
                string save_file = File.ReadAllText(save_file_path); //json �б�
                MapInfo m_mapInfo = JsonUtility.FromJson<MapInfo>(save_file); //json ����
                Debug.Log(save_file);

                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform);//����� �� �ν��Ͻ�ȭ
                GameObject map = GameObject.FindGameObjectWithTag("Map");//�ٽ� ã�Ƽ� ����

                for(int i = 0; i < 40; i++)//��ġ ��ġȭ, �� �߱Ⱑ �ʿ���
                {
                    map.transform.GetChild(i).transform.position = m_mapInfo.map_position[i];
                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    if (m_mapInfo.line_position[i].x == 0 && m_mapInfo.line_position[i].y == 0)
                    {
                        Destroy(map.transform.GetChild(i).gameObject);//�� ���¾ֵ� ����  
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