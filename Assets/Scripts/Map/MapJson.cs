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
        public List<GameObject> map_path = new List<GameObject>(); //�� ��� ���� ����Ʈ
        public List<GameObject> map_path1 = new List<GameObject>(); 
        public List<GameObject> map_path2 = new List<GameObject>(); 

        public List <Sprite> sprite = new List<Sprite>(); //��Ʈ����Ʈ ����Ʈ
        public List<Sprite> sprite1 = new List<Sprite>();
        public List<Sprite> sprite2 = new List<Sprite>();

        public List <eMapType> mapType = new List <eMapType>(); //��Ÿ�� ���� ����Ʈ
        public List<eMapType> mapType1 = new List<eMapType>();
        public List<eMapType> mapType2 = new List<eMapType>();

        public bool elite_exist = false; //����Ʈ Ȯ�ο� �οﺯ��
        public bool elite_exist1 = false;
        public bool elite_exist2 = false;
    }
    public class MapJson : MonoBehaviour
    {
        [SerializeField] Sprite[] m_sprite;
        [SerializeField] GameObject m_prefab_canvas;
        [SerializeField] GameObject m_saved_map;
        LineRenderer l;//�̰ɽẸ��
        private static string m_save_path => "Assets/Resources/MapJson/";

        private void Start()
        {
            Instantiate(l, m_prefab_canvas.transform);//������ ���� �������� �̰� �̿��ؾ���
            if (!Directory.Exists(m_save_path)) //���丮�� ������
            {
                Directory.CreateDirectory(m_save_path); //����
                MapInfo m_mapInfo = new MapInfo();//�� ���� ��ü ����

                for(int i =0; i < 30; i++) //����� �ʱ�ȭ
                {
                    m_saved_map.transform.GetChild(i).transform.position = Resources.Load<GameObject>("MapPrefab").transform.GetChild(i).transform.position;
                }
                Instantiate(m_saved_map, m_prefab_canvas.transform); //�ν��Ͻ� ȭ
                GameObject map = GameObject.FindGameObjectWithTag("Map");
                for (int i = 0; i < 30; i += 3) //�� ��μ���
                {
                    map.transform.GetChild(i).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))); //�ʱ�ȭ�� ���ÿ� ��ġ ����ȭ
                    map.transform.GetChild(i+1).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f))); 
                    map.transform.GetChild(i+2).transform.Translate(new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)));
                    m_mapInfo.map_path.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject); //��� ���� �ؾߵ�.
                    m_mapInfo.map_path1.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject);
                    m_mapInfo.map_path2.Add(map.transform.GetChild(Random.Range(i, i + 2)).gameObject);
                }
                m_mapInfo.map_path.Add(map.transform.GetChild(30).gameObject);//���� ��� �ֱ�
                m_mapInfo.map_path1.Add(map.transform.GetChild(30).gameObject);
                m_mapInfo.map_path2.Add(map.transform.GetChild(30).gameObject);

                for (int i = 0; i < m_mapInfo.map_path.Count-1; i++) //���� ��Ÿ�� ����
                {
                    int map_number = Random.Range(0, 6);
                    if (map_number == 1 && m_mapInfo.elite_exist == true)
                    {
                        while (map_number == 1) //����Ʈ�� �����ؾ���
                        {
                            map_number = Random.Range(0, 6);
                        }
                    }
                    else if (map_number == 1) m_mapInfo.elite_exist = true;//����Ʈ ����
                    m_mapInfo.mapType.Add((eMapType)map_number); //�� ��Ÿ�� ����
                    //m_mapInfo.sprite.Add(m_sprite[map_number]); //UI�����ε� ���� ��������Ʈ�� ����
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
                m_mapInfo.mapType.Add(eMapType.Boss);//������ �����ֱ�
                m_mapInfo.mapType1.Add(eMapType.Boss);
                m_mapInfo.mapType2.Add(eMapType.Boss);

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
                Instantiate(m_saved_map, m_prefab_canvas.transform);//����� �� �ν��Ͻ�ȭ
            }
        }
    }
}