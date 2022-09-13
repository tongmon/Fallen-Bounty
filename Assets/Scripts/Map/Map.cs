using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class Map : FadeInOut
{
    [SerializeField] GameObject m_PrefabCanvas;//�ν��Ͻ�ȭ ��ġ
    [SerializeField] GameObject m_map_prefab;
    [SerializeField] GameObject m_map_type;

    //public string m_path;//���ϰ��

    public List<MapNode> m_node;

    IEnumerator MapLoadC()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
        for (int i = 0; i < m_map_prefab.transform.childCount; i++)
        {
            m_map_prefab.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        m_map_type.name = m_node[int.Parse(EventSystem.current.currentSelectedGameObject.name)].m_mapType.ToString();//�� Ÿ������
        m_map_type.transform.GetChild(0).name = int.Parse(EventSystem.current.currentSelectedGameObject.name).ToString();
        DontDestroyOnLoad(m_map_type);
        SceneManager.LoadScene("Scene_test_copy");
    }
    IEnumerator MapCreateScriptable(GameObject map)
    {
        yield return new WaitForSecondsRealtime(0f);
        if (m_node[40].m_num == 0)//������ �����Ƴ�
        {
            MapReset(map);
            for (int i = 0; i < map.transform.childCount - 1; i++)//�ʱ�ȭ
            {
                m_node[i].m_num = i;
                m_node[i].m_mapType = eMapType.Common;
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.Translate(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                m_node[i].m_position = obj.transform.position;
            }
            m_node[40].m_num = map.transform.childCount;
            m_node[40].m_mapType = eMapType.Boss;
            
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                int node_num = 0;
                int map_num = Random.Range(0, 7);
                if (i == 36 || i == 37 || i == 38 || i == 39)
                {
                    node_num = 40;
                }
                else
                {
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i + 4, i + 8);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i + 3, i + 7);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i + 2, i + 6);
                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i + 1, i + 5);
                    }
                    m_node[node_num].m_parent_num.Add(i);
                }
                m_node[i].m_child_num.Add(node_num);
                if (i > 3)
                {
                    bool isElite = false;
                    for (int j = 0; j < m_node[j].m_parent_num.Count; j++)
                    {
                        if (m_node[m_node[j].m_parent_num[j]].m_mapType == eMapType.Elite) isElite = true;
                    }
                    if (m_node[i].m_child_num[0] == 40 || isElite || m_node[m_node[i].m_child_num[0]].m_mapType == eMapType.Elite)
                    {
                        while (map_num != 1)
                        {
                            map_num = Random.Range(0, 7);
                        }
                    }
                }
                m_node[i].m_mapType = (eMapType)map_num;
            }//�θ� �ڽ� �������
            for (int i = 4; i < m_node.Count - 1; i++)
            {
                if (m_node[i].m_parent_num.Count == 0) 
                {
                    int node_num = 0;
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i - 4, i);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i - 5, i - 1);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i - 6, i - 2);
                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i - 7, i - 3);
                    }
                    m_node[i].m_parent_num.Add(node_num);
                    m_node[node_num].m_child_num.Add(i);
                }
            } //�Դ� ���� ����.
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                if (m_node[i].m_child_num.Count > 1)
                {
                    obj.GetComponent<LineRenderer>().SetPosition(2, map.transform.GetChild(m_node[i].m_child_num[1]).position);
                }
                else obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).position);
            } //�ٱ߱�
        }
        else
        {
            for (int i = 0; i < m_node.Count - 1; i++)//�ν��Ͻ��� ��ġȭ
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.position = m_node[i].m_position;
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
            }
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).transform.position);
                if (m_node[i].m_child_num.Count > 1)
                {
                    obj.GetComponent<LineRenderer>().SetPosition(2, map.transform.GetChild(m_node[i].m_child_num[1]).transform.position);
                }
            }
        }
    }
    /*IEnumerator MapCreate(GameObject map, JArray jarray)
    {
        yield return new WaitForSecondsRealtime(0f);
        if (!File.Exists(m_path + "MapJson.json"))
        {
            MapReset(map);
            for (int i = 0; i < map.transform.childCount-1; i++)//�ʱ�ȭ
            {
                MapNode temp = new MapNode();//��忡 ���� �༮�� ����
                temp.m_num = i;
                temp.m_mapType = eMapType.Common;
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.Translate(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                temp.m_position = obj.transform.position;
                m_node.Add(temp);
            }
            MapNode boss = new MapNode();//���� ����
            boss.m_num = map.transform.childCount;
            boss.m_mapType = eMapType.Boss;
            m_node.Add(boss);
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                int node_num = 0;
                int map_num = Random.Range(0, 7);
                if (i == 36 || i == 37 || i == 38 || i == 39)
                {
                    node_num = 40;
                }
                else
                {
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i + 4, i + 8);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i + 3, i + 7);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i + 2, i + 6);
                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i + 1, i + 5);
                    }
                    m_node[node_num].m_parent_num.Add(i);
                }
                m_node[i].m_child_num.Add(node_num);
                if (i > 3)
                {
                    bool isElite = false;
                    for (int j = 0; j < m_node[j].m_parent_num.Count; j++)
                    {
                        if (m_node[m_node[j].m_parent_num[j]].m_mapType == eMapType.Elite) isElite = true;
                    }
                    if (m_node[i].m_child_num[0] == 40 || isElite || m_node[m_node[i].m_child_num[0]].m_mapType == eMapType.Elite)
                    {
                        while (map_num != 1)
                        {
                            map_num = Random.Range(0, 7);
                        }
                    }
                }
                m_node[i].m_mapType = (eMapType)map_num;
            }//�θ� �ڽ� �������
            for (int i = 4; i < m_node.Count - 1; i++)
            {
                if (m_node[i].m_parent_num.Count == 0) //�˻� ���ϴµ� ���� ������ ����.
                {
                    int node_num = 0;
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i - 4, i);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i - 5, i - 1);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i - 6, i - 2);
                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i - 7, i - 3);
                    }
                    m_node[i].m_parent_num.Add(node_num);
                    m_node[node_num].m_child_num.Add(i);
                }
            } //�Դ� ���� ����.
            for (int i = 0; i < m_node.Count; i++) jarray.Add(JObject.Parse(JsonUtility.ToJson(m_node[i], true))); //���⼭ ����
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                if (m_node[i].m_child_num.Count > 1)
                {
                    obj.GetComponent<LineRenderer>().SetPosition(2, map.transform.GetChild(m_node[i].m_child_num[1]).position);
                }
                else obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).position);
            } //�ٱ߱�
            JsonParser.CreateJsonFile(m_path, "MapJson", jarray.ToString());
        }
        else
        {
            m_node = JsonParser.LoadJsonArrayToList<MapNode>(m_path + "MapJson");
            for (int i = 0; i < m_node.Count - 1; i++)//�ν��Ͻ��� ��ġȭ
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.position = m_node[i].m_position;
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
            }
            for (int i = 0; i < m_node.Count - 1; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).transform.position);
                if (m_node[i].m_child_num.Count > 1)
                {
                    obj.GetComponent<LineRenderer>().SetPosition(2, map.transform.GetChild(m_node[i].m_child_num[1]).transform.position);
                }
            }
        }
    }*/
    private void Start()
    {
        FadeInM();
        //m_node = new List<MapNode>();
        //m_path = "Assets/Resources/MapJson/";
        GameObject map = Instantiate(m_map_prefab, m_PrefabCanvas.transform); //�� �ν��Ͻ�ȭ
        for (int i = 0; i < map.transform.childCount; i++)
        {
            map.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(MapLoad);
        }
        JArray jarray = new JArray(); //����Ʈ������ Json ����
        //StartCoroutine(MapCreate(map, jarray));
        StartCoroutine(MapCreateScriptable(map));
    }
    public void MapLoad()
    {
        StartCoroutine(MapLoadC());
    }
    public void MapReset(GameObject obj)//�� �ʱ�ȭ
    {
        for (int i = 0; i < 4; i++)//��ư ���� ���ֱ�
        {
            if (i < 4)
            {
                obj.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
            else obj.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
}