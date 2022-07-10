using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

/*

- Newtonsoft.Json ����

// �ڵ�
var json = new JObject();
json.Add("name", "Paladin");
json.Add("attack_power", 1.5);

// json.ToString() ���
{
  "name": "Paladin",
  "attack_power": 1.5
}

// �ڵ�
var jarray = new JArray();
jarray.Add(1);
jarray.Add("Paladin");

// jarray.ToString() ���
[
  1,
  "Paladin"
]

// �ڵ�
var jarray = new JArray();
jarray.Add("Witch");
jarray.Add("Cleric");

var json = new JObject();
json.Add("name", "Paladin");
json.Add("friends", jarray);

// json.ToString() ���
{
  "name": "Paladin",
  "friends":
  [
    "Witch",
    "Cleric"
  ]
}

- derived class������ ����� ���� ��ũ ����
https://stackoverflow.com/questions/8513042/json-net-serialize-deserialize-derived-types
https://github.com/manuc66/JsonSubTypes
https://makolyte.com/csharp-deserialize-json-to-a-derived-type/

- ���� MonoBehaviour ��ü�� ��ӹ޴� ���·� ���� �ִµ� �̸� ���� �и� ��Ű�� ���� ����غ��� ��...
���� ������ json �Ľ��� �� Ŭ������ ��Ƣ��Ǳ� ������ �̸� �ִ��� �ٿ��ߴ�
*/

[JsonConverter(typeof(JsonSubtypes))]
public class JsonVector2
{
    public float x;
    public float y;

    public JsonVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(JsonVector2 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public static explicit operator JsonVector2(Vector2 vec)
    {
        return (new JsonVector2(vec.x, vec.y));
    }
}

public class JsonParser
{
    private static JsonParser m_inst = null;

    #region ���� Json ����
    public List<HeroData> m_heroes;
    public Dictionary<string, int> m_heroes_dict;
    #endregion

    JsonParser()
    {
        m_heroes = new List<HeroData>();
        m_heroes_dict = new Dictionary<string, int>();
        m_heroes = LoadJsonArrayToBaseList<HeroData>(Application.dataPath + "/DataFiles/ObjectFiles/HeroList");
        for (int i = 0; i < m_heroes.Count; i++)
            m_heroes_dict[m_heroes[i].type_name] = i;
    }

    public static JsonParser Instance
    {
        get
        {
            if (null == m_inst)
            {
                //���� �ν��Ͻ��� ���ٸ� �ϳ� �����ؼ� �־��ش�.
                m_inst = new JsonParser();
            }
            return m_inst;
        }
    }

    #region JSON ���� ����
    public static void CreateJsonFile(string create_path, string file_name, string json_data) 
    { 
        FileStream file_stream = new FileStream(string.Format("{0}/{1}.json", create_path, file_name), FileMode.Create); 
        byte[] data = Encoding.UTF8.GetBytes(json_data);
        file_stream.Write(data, 0, data.Length);
        file_stream.Close(); 
    }

    public static void CreateJsonFile(string full_path, string json_data)
    {
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json_data);
        file_stream.Write(data, 0, data.Length);
        file_stream.Close();
    }

    public static void CreateJsonFile(string create_path, string file_name, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        string full_path = string.Format("{0}/{1}.json", create_path, file_name);
        File.WriteAllText(full_path, str_json);
    }

    public static void CreateJsonFile(string full_path, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        File.WriteAllText(full_path, str_json);
    }
    #endregion

    #region JSON ���� �б�
    public static T LoadJsonFile<T>(string saved_path, string file_name) 
    { 
        FileStream file_stream = new FileStream(string.Format("{0}/{1}.json", saved_path, file_name), FileMode.Open); 
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close(); 
        string json_data = Encoding.UTF8.GetString(data); 
        return JsonConvert.DeserializeObject<T>(json_data); 
    }

    public static T LoadJsonFile<T>(string full_path)
    {
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Open);
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close();
        string json_data = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(json_data);
    }

    public static List<T> LoadJsonArrayToList<T>(string full_path)
    {
        List<T> ret_list = new List<T>();
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Open);
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close();

        string json_data = Encoding.UTF8.GetString(data);
        JArray jarray = JArray.Parse(json_data);

        foreach (JObject jobject in jarray.Children<JObject>())
        {
            T json_class = jobject.ToObject<T>();
            ret_list.Add(json_class);
        }

        return ret_list;
    }

    public static List<T> LoadJsonArrayToBaseList<T>(string full_path, string type_name_key_str = "type_name")
    {
        List<T> ret_list = new List<T>();
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Open);
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close();

        if(data.Length == 0)
            return ret_list;

        string json_data = Encoding.UTF8.GetString(data);
        JArray jarray = JArray.Parse(json_data);

        foreach (JObject jobject in jarray.Children<JObject>())
        {
            var json_class = jobject.ToObject(Type.GetType(jobject[type_name_key_str].ToString()));
            ret_list.Add((T)json_class);      
        }

        return ret_list;
    }
    #endregion

    // ���� ȹ��
    public static HeroData GetHero(string hero_name)
    {
        return Instance.m_heroes[Instance.m_heroes_dict[hero_name + "Data"]];
    }
}
