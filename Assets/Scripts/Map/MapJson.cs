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
        public GameObject map_object; //���ӿ�����Ʈ ��������
        public Sprite map_sprite; //����� ��������Ʈ
        public eMapType map_type; //����� �� ����

        public List<TreeNode> children = new List<TreeNode>(); //�ڽ����� �����ձ�
    }
    [System.Serializable]
    public class MapInfo 
    {
        public List<Vector3> map_position = new List<Vector3>();//�� ��ġ ���� ����Ʈ
        public List<Vector3> line_position = new List<Vector3>();//������ġ ���� ����Ʈ

        public int [] root = new int[2];
        public int [] root1 = new int[2];
        public int [] root2 = new int[2];
        public int [] root3 = new int[2];

        public TreeNode node = new TreeNode();

        public bool elite_exist = false; //����Ʈ Ȯ�ο� �οﺯ��
        public bool elite_exist1 = false;
        public bool elite_exist2 = false;
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

                TreeNode tree = new TreeNode();

                tree.map_object = map.transform.GetChild(0).gameObject;

                m_mapInfo.node.children.Add(tree);
              

                for (int i = 4; i < 40; i++) //�� ��μ���
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f))); //��ġ ����ȭ
                    m_mapInfo.map_position.Add(map.transform.GetChild(i).transform.position);//����ȭ�� ��ġ ����

                    map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;//���� ���� �����ϱ�
                    map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                    map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(i).transform.position);

                    if(i % 4 == 0)
                    {
         
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