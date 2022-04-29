using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlParser
{
    public Dictionary<string, XmlNode> nodes;
    static public void LoadXml(string filepath, string type)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
    }
}
