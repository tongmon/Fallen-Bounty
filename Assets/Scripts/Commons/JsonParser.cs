using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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


[

{
  "name" : "preload map setting 1"
  0 : [1,2]
  1 : []
}

{
  "name" : "preload map setting 2"
  "attack_power": 1.5
}

{
  "name": "Paladin",
  "attack_power": 1.5
}

{
  "name": "Paladin",
  "attack_power": 1.5
}

]

*/

public class JsonParser
{
    static JsonParser()
    {

    }

    #region JSON 파일 생성
    void CreateJsonFile(string create_path, string file_name, string json_data) 
    { 
        FileStream file_stream = new FileStream(string.Format("{0}/{1}.json", create_path, file_name), FileMode.Create); 
        byte[] data = Encoding.UTF8.GetBytes(json_data);
        file_stream.Write(data, 0, data.Length);
        file_stream.Close(); 
    }

    void CreateJsonFile(string full_path, string json_data)
    {
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json_data);
        file_stream.Write(data, 0, data.Length);
        file_stream.Close();
    }

    void CreateJsonFile(string create_path, string file_name, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        string full_path = string.Format("{0}/{1}.json", create_path, file_name);
        File.WriteAllText(full_path, str_json);
    }

    void CreateJsonFile(string full_path, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        File.WriteAllText(full_path, str_json);
    }
    #endregion

    #region JSON 파일 읽기
    T LoadJsonFile<T>(string saved_path, string file_name) 
    { 
        FileStream file_stream = new FileStream(string.Format("{0}/{1}.json", saved_path, file_name), FileMode.Open); 
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close(); 
        string jsonData = Encoding.UTF8.GetString(data); 
        return JsonConvert.DeserializeObject<T>(jsonData); 
    }

    T LoadJsonFile<T>(string full_path)
    {
        FileStream file_stream = new FileStream(string.Format("{0}.json", full_path), FileMode.Open);
        byte[] data = new byte[file_stream.Length];
        file_stream.Read(data, 0, data.Length);
        file_stream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    #endregion
}
