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
        public eMapType map_type; //����� �� ����

        public int left_index;
        public int right_index;

        public List<TreeNode> children = new List<TreeNode>();
    }
    [System.Serializable]
    public class MapInfo 
    {
        public List<Vector3> map_position = new List<Vector3>();//�� ��ġ ���� ����Ʈ
        public List<Vector3> line_position = new List<Vector3>();//������ġ ���� ����Ʈ

        public TreeNode[] node1 = new TreeNode[10];//�θ��
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


                for (int i = 4; i < 40; i++) //�� ��μ���
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f))); //��ġ ����ȭ
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//����ȭ�� ��ġ ����

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//���� ���� �����ϱ�
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);

                    if(i % 4 == 0)
                    {
                        TreeNode tree = new TreeNode();
                        if(Random.Range(0,2) == 0)
                        {
                            tree.map_object = map.transform.GetChild(Random.Range(i, i + 3)).gameObject;
                            if (Random.Range(0, 1) == 1) m_mapInfo.node1[0].children.Add(tree);
                            //m_mapInfo.node1
                        }
                    }
                    else if(i % 4 == 1)
                    {
                        
                    }
                    else if (i % 4 == 2)
                    {
                       
                    }
                    else if (i % 4 == 3)
                    {
                        
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