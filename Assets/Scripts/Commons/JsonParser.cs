using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonParser
{
    static JsonParser()
    {

    }

    static public void LoadJson(string file_path)
    {
        File.ReadAllText(file_path);
    }
}
