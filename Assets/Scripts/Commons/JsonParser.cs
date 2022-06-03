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

- Newtonsoft.Json 사용법

// 코드
var json = new JObject();
json.Add("name", "Paladin");
json.Add("attack_power", 1.5);

// json.ToString() 결과
{
  "name": "Paladin",
  "attack_power": 1.5
}

// 코드
var jarray = new JArray();
jarray.Add(1);
jarray.Add("Paladin");

// jarray.ToString() 결과
[
  1,
  "Paladin"
]

// 코드
var jarray = new JArray();
jarray.Add("Witch");
jarray.Add("Cleric");

var json = new JObject();
json.Add("name", "Paladin");
json.Add("friends", jarray);

// json.ToString() 결과
{
  "name": "Paladin",
  "friends":
  [
    "Witch",
    "Cleric"
  ]
}

- derived class에서의 사용은 밑의 링크 참고
https://stackoverflow.com/questions/8513042/json-net-serialize-deserialize-derived-types
https://github.com/manuc66/JsonSubTypes
https://makolyte.com/csharp-deserialize-json-to-a-derived-type/

- 지금 MonoBehaviour 자체를 상속받는 형태로 가고 있는데 이를 따로 분리 시키는 구조 고려해봐야 댐...
위의 이유는 json 파싱할 때 클래스가 뻥튀기되기 때문에 이를 최대한 줄여야댐
*/

public class JsonParser
{
    static JsonParser()
    {

    }

    #region JSON 파일 생성
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

    #region JSON 파일 읽기
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
