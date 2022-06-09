using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using JsonSubTypes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class MapJson : MonoBehaviour
{
    MapNode m_node; //json으로 불러오기전
    public string m_path;//파일경로
    private void Start()
    {
        m_path = "Assets/Resources/MapJson/";//생성가능
        if (!File.Exists(m_path))
        {
            JArray json = new JArray();
            for(int i =0; i < 40; i++)
            {
                JObject temp = new JObject();
                temp.Add("node_num", i);
                temp.Add("map_type", "common");
                JArray jarray1 = new JArray();
                JArray jarray2 = new JArray();
                temp.Add("parent", jarray1);
                temp.Add("children", jarray2);
                json.Add(temp);
            }
            JsonParser.CreateJsonFile(m_path, "MapJson", json.ToString());
        }
        else
        {
            m_node = JsonParser.LoadJsonFile<MapNode>(m_path, "MapJson");
        }
    }
}
