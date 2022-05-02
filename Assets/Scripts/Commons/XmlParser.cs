using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlParser
{
    static XmlDocument m_doc;
    static XmlParser()
    {
        m_doc = new XmlDocument();

    }

    // XmlParser.LoadXml()�� ���
    static public void LoadXml(string filepath, HeroManager hero_manager)
    {
        m_doc.Load(filepath);

        XmlElement nodes = m_doc["Root"];

        foreach (XmlElement node in nodes.ChildNodes)
        {
            string script_name = node.GetAttribute("Name");
            float health = (float)System.Convert.ToDouble(node.GetAttribute("Health"));
            float magic_power = (float)System.Convert.ToDouble(node.GetAttribute("MagicPower"));

            hero_manager.AddHero(script_name /* ������ ���Ŀ� �߰� */);
        }
    }

    static public void LoadXml(string filepath, AbilityManager abillity_manager)
    {
        m_doc.Load(filepath);

        XmlElement nodes = m_doc["Root"];

        foreach (XmlElement node in nodes.ChildNodes)
        {
            string script_name = node.GetAttribute("Name");
            float active_time = (float)System.Convert.ToDouble(node.GetAttribute("ActiveTime"));
            float cooldown_time = (float)System.Convert.ToDouble(node.GetAttribute("CoolDownTime"));

            abillity_manager.AddAbility(script_name, cooldown_time, active_time);
        }
    }
}
