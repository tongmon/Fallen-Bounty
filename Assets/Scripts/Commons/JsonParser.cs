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
]?

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
*/

public class JsonParser
{
    static JsonParser()
    {

    }

    #region JSON 파일 생성
    void CreateJsonFile(string create_path, string file_path, string json_data) 
    { 
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", create_path, file_path), FileMode.Create); 
        byte[] data = Encoding.UTF8.GetBytes(json_data); 
        fileStream.Write(data, 0, data.Length); 
        fileStream.Close(); 
    }

    void CreateJsonFile(string full_path, string json_data)
    {
        FileStream fileStream = new FileStream(string.Format("{0}.json", full_path), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(json_data);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    void CreateJsonFile(string create_path, string file_path, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        string full_path = string.Format("{0}/{1}.json", create_path, file_path);
        File.WriteAllText(full_path, str_json);
    }

    void CreateJsonFile(string full_path, JObject json_data)
    {
        string str_json = JsonConvert.SerializeObject(json_data, Formatting.None);
        File.WriteAllText(full_path, str_json);
    }
    #endregion

    T LoadJsonFile<T>(string loadPath, string fileName) 
    { 
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open); 
        byte[] data = new byte[fileStream.Length]; 
        fileStream.Read(data, 0, data.Length); 
        fileStream.Close(); 
        string jsonData = Encoding.UTF8.GetString(data); 
        return JsonConvert.DeserializeObject<T>(jsonData); 
    }
}
