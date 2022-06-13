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
        if (!File.Exists(m_path))
        {
            for (int i = 0; i < 40; i++)
            {
                MapNode temp = new MapNode();//��忡 ���� �༮�� ����
                temp.m_num = i;
                temp.m_mapType = eMapType.Common;
                temp.m_object = map.transform.GetChild(i).gameObject;
                m_node.Add(temp);
            }
            for (int i =0; i < m_node.Count; i++)
            {
                int j = 0;
                if (i % 4 == 0)
                {
                    j = Random.Range(i, i + 3);
                    m_node[i].m_children.Add(m_node[j]);
                }
                else if (i % 4 == 1)
                {
                    j = Random.Range(i - 1, i + 2);
                    m_node[i].m_children.Add(m_node[j]);
                }
                else if (i % 4 == 2)
                {
                    j = Random.Range(i - 2, i + 1);
                    m_node[i].m_children.Add(m_node[j]);
                }
                else if (i % 4 == 3)
                {
                    j = Random.Range(i - 3, i);
                    m_node[i].m_children.Add(m_node[j]);
                }

                if (i > 3)
                {
                    m_node[j].m_parent.Add(m_node[i]);
                }
                jarray.Add(JsonUtility.ToJson(m_node[i]));
            }//�θ� �ڽ� �������
            JsonParser.CreateJsonFile(m_path, "MapJson", jarray.ToString());//����� ��ü�� ����
        }
        else
        {
            m_node = JsonParser.LoadJsonArrayToList<MapNode>(m_path);
        }
    }
}
