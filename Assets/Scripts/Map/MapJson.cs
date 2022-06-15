using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MapJson : MonoBehaviour
{
    [SerializeField] GameObject m_PrefabCanvas;//�ν��Ͻ�ȭ ��ġ
    [SerializeField] GameObject m_map_prefab;
    public string m_path;//���ϰ��
    List<MapNode> m_node;
    private void Start()
    {
        m_node = new List<MapNode>();
        m_path = "Assets/Resources/MapJson/";
        Instantiate(m_map_prefab, m_PrefabCanvas.transform);
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        JArray jarray = new JArray();
        if (!File.Exists(m_path + "MapJson.json"))
        {
            for (int i = 0; i < 40; i++)//�ʱ�ȭ
            {
                MapNode temp = new MapNode();//��忡 ���� �༮�� ����
                temp.m_num = i;
                temp.m_mapType = eMapType.Common;
                map.transform.GetChild(i).gameObject.transform.Translate(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f), 0);
                map.transform.GetChild(i).GetComponent<LineRenderer>().startWidth = 0.05f;
                map.transform.GetChild(i).GetComponent<LineRenderer>().endWidth = 0.05f;
                map.transform.GetChild(i).GetComponent<LineRenderer>().SetPosition(1, map.transform.GetChild(i).transform.position);
                temp.m_object = map.transform.GetChild(i).gameObject;
                m_node.Add(temp);
            }
            for (int i =0; i < m_node.Count; i++)
            {
                int node_num = 0;
                int map_num = Random.Range(0, 6);
                if (i % 4 == 0)
                {
                    node_num = Random.Range(i, i + 3);
                }
                else if (i % 4 == 1)
                {
                    node_num = Random.Range(i - 1, i + 2);
                }
                else if (i % 4 == 2)
                {
                    node_num = Random.Range(i - 2, i + 1);

                }
                else if (i % 4 == 3)
                {
                    node_num = Random.Range(i - 3, i);
                }
                m_node[i].m_children = m_node[node_num];
                m_node[node_num].m_parent = m_node[i];
                m_node[i].m_object.GetComponent<LineRenderer>().SetPosition(0, m_node[node_num].m_object.transform.position);//������ �ϳ���
                if (i > 3)
                {
                    if (m_node[i].m_parent.m_mapType == eMapType.Elite || m_node[i].m_children.m_mapType == eMapType.Elite)//�̰� �� �־ȵ�
                    {
                        while (map_num != 1)
                        {
                            map_num = Random.Range(0, 6);
                        }
                    }
                }
                
                m_node[i].m_mapType = (eMapType)map_num;

                jarray.Add(JObject.Parse(JsonUtility.ToJson(m_node[i], true)));
            }//�θ� �ڽ� �������
            JsonParser.CreateJsonFile(m_path, "MapJson", jarray.ToString());
        }
        else
        {
            m_node = JsonParser.LoadJsonArrayToList<MapNode>(m_path + "MapJson");//�Ѵ� �ȵ�

            jarray = JsonParser.LoadJsonFile<JArray>(m_path + "MapJson");
            foreach (JObject jobject in jarray.Children<JObject>())
            {
                MapNode node = jobject.ToObject<MapNode>();
                m_node.Add(node);
            }
            for (int i =0; i< m_node.Count; i++)
            {
                map.transform.GetChild(i).transform.position = m_node[i].m_object.transform.position;
            }
        }
    }
}
