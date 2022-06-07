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
    public class TreeNode //Ʈ���� �� ��� ����
    {
        public GameObject map_object;//���ӿ�����Ʈ ��������
        public Sprite map_sprite;//����� ��������Ʈ
        public eMapType map_type = eMapType.Common; //����� �� ���� - Default : �Ϲ�

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
        public List<Vector3> map_position = new List<Vector3>();//�� ��ġ ���� ����Ʈ
        public List<Vector3> line_position = new List<Vector3>();//������ġ ���� ����Ʈ

        public TreeNode[] node = new TreeNode[42];//Ʈ��
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] Sprite[] m_sprite;
        [SerializeField] GameObject m_prefab_canvas;
        private static string m_save_path => "Assets/Resources/MapJson/";
        private void Start()
        {
            if (!Directory.Exists(m_save_path)) //���丮�� ������
            {
                Directory.CreateDirectory(m_save_path); //����
                MapInfo m_mapInfo = new MapInfo();//�� ���� ��ü ����
                Instantiate(Resources.Load<GameObject>("MapPrefab"), m_prefab_canvas.transform); //�ν��Ͻ� ȭ
                GameObject map = GameObject.FindGameObjectWithTag("Map");//��ü ����

                for (int i = 0; i < 42; i++) {
                    m_mapInfo.node[i] = new TreeNode($"{i}"); //���⼭ ��ü ������ ���� �������.
                }

                m_mapInfo.node[0].map_object = map.transform.GetChild(0).gameObject;//�θ��
                m_mapInfo.node[1].map_object = map.transform.GetChild(1).gameObject;
                m_mapInfo.node[2].map_object = map.transform.GetChild(2).gameObject;
                m_mapInfo.node[3].map_object = map.transform.GetChild(3).gameObject;

                int pre_index = 0;//���� �ε��� �˻��
                int pre_index1 = 1;
                int pre_index2 = 2;
                int pre_index3 = 3;
                for (int i = 0; i < 40; i++) //�� ��μ���
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f))); //��ġ ����ȭ
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//����ȭ�� ��ġ ����

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//���� ���� �����ϱ�
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);

                    if (i % 4 == 0 && i != 0)
                    {
                        int a = Random.Range(i,i+3);//��� �ֱ��
                        if(a % 4 != 0 && Random.Range(0,1) ==1)
                        {
                            m_mapInfo.node[a].map_object = map.transform.GetChild(a).gameObject;
                            m_mapInfo.node[pre_index].children.Add(m_mapInfo.node[a]);
                        }
                        m_mapInfo.node[i].map_object = map.transform.GetChild(i).gameObject;
                        m_mapInfo.node[pre_index].children.Add(m_mapInfo.node[i]);
                        pre_index = i; //�̰� ������ ���Ű���
                    }
                    else if (i % 4 == 1 && i != 1 && Random.Range(0, 1) == 1)
                    {
                        int a = Random.Range(i-1, i + 2);//��� �ֱ��
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
                        int a = Random.Range(i-2, i + 1);//��� �ֱ��
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
                        int a = Random.Range(i-3, i);//��� �ֱ��
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