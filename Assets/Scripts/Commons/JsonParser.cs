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

public class JsonParser
{
    static JsonParser()
    {

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

    public static List<T> LoadJsonArrayFile<T>(string full_path)
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

    public static List<T> LoadJsonArrayFileTest<T>(string full_path)
    {
        List<T> ret_list = new List<T>();
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Open);
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close();

        string json_data = Encoding.UTF8.GetString(data);
        JArray jarray = JArray.Parse(json_data);

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
            .Of(typeof(CreatureData))
            .RegisterSubtypeWithProperty(typeof(HeroData), "physic_power")
            .RegisterSubtypeWithProperty(typeof(WitchData), "gained_soul_num")
            .Build());

        foreach (JObject jobject in jarray.Children<JObject>())
        {
            // var json_class = jobject.ToObject(Type.GetType(jobject["type_name"].ToString()));
            //ret_list.Add(json_class);

            string temp_data = jobject.ToString();
            WitchData k = JsonConvert.DeserializeObject<WitchData>(temp_data, settings);
          
        }

        return ret_list;
    }
    #endregion
}
