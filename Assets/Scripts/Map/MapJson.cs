using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MapJson : MonoBehaviour
{
    [SerializeField] GameObject m_PrefabCanvas;//인스턴스화 위치
    [SerializeField] GameObject m_map_prefab;
    public string m_path;//파일경로
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
            for (int i = 0; i < 40; i++)//초기화
            {
                MapNode temp = new MapNode();//노드에 넣을 녀석들 생성
                temp.m_num = i;
                temp.m_mapType = eMapType.Common;
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.Translate(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                temp.m_position = obj.transform.position;
                m_node.Add(temp);
            }
            for (int i =0; i < m_node.Count; i++) //가끔 선택안된애들이 생긴다 - 아이디어 : 한번 포문 더 돌아서 랜덤 부모에게 연결.
            {
                int node_num = 0;
                int map_num = Random.Range(0, 6);
                if (i == 36 || i == 37 || i == 38 || i == 39)
                {
                    node_num = 40;
                }
                else
                {
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i + 4, i + 7);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i + 3, i + 6);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i + 2, i + 5);

                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i + 1, i + 4);
                    }
                    m_node[node_num].m_parent_num = i;
                }
                m_node[i].m_child_num.Add(node_num);
                if (i > 3)
                {
                    if (m_node[i].m_child_num[0] == 40 || m_node[m_node[i].m_parent_num].m_mapType == eMapType.Elite || m_node[m_node[i].m_child_num[0]].m_mapType == eMapType.Elite)
                    {
                        while (map_num != 1)
                        {
                            map_num = Random.Range(0, 6);
                        }
                    }
                }
                m_node[i].m_mapType = (eMapType)map_num;
                jarray.Add(JObject.Parse(JsonUtility.ToJson(m_node[i], true)));
            }//부모 자식 관계생성
            for (int i = 4; i < m_node.Count; i++) { 
                if(m_node[i].m_parent_num == 0) //부모가 없다.
                {
                    int node_num = 0;
                    if (i % 4 == 0)
                    {
                        node_num = Random.Range(i - 1, i - 4);
                    }
                    else if (i % 4 == 1)
                    {
                        node_num = Random.Range(i -2 , i - 5);
                    }
                    else if (i % 4 == 2)
                    {
                        node_num = Random.Range(i -3, i -6);

                    }
                    else if (i % 4 == 3)
                    {
                        node_num = Random.Range(i -4, i -7);
                    }
                    m_node[i].m_parent_num = node_num;
                    m_node[node_num].m_child_num.Add(i);
                }
            } //함더 포문 돌기.
            for (int i = 0; i < m_node.Count; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                if(m_node[i].m_child_num.Count > 1)
                {
                    obj.GetComponent<LineRenderer>().SetPosition(2, map.transform.GetChild(m_node[i].m_child_num[1]).position);
                }
                else obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).position);
            } //줄긋기
            JsonParser.CreateJsonFile(m_path, "MapJson", jarray.ToString());
        }
        else
        {
            m_node = JsonParser.LoadJsonArrayToList<MapNode>(m_path + "MapJson");
            for (int i =0; i< m_node.Count; i++)//인스턴스와 일치화
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.transform.position = m_node[i].m_position;
                obj.GetComponent<LineRenderer>().startWidth = 0.05f;
                obj.GetComponent<LineRenderer>().endWidth = 0.05f;
                obj.GetComponent<LineRenderer>().SetPosition(1, obj.transform.position);
                obj.GetComponent<LineRenderer>().SetPosition(2, obj.transform.position);
            }
            for (int i = 0; i < m_node.Count; i++)
            {
                GameObject obj = map.transform.GetChild(i).gameObject;
                obj.GetComponent<LineRenderer>().SetPosition(0, map.transform.GetChild(m_node[i].m_child_num[0]).transform.position);
            }
        }
    }
}