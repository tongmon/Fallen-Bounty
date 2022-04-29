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

    // 다른데서 XmlParser.LoadXml 이렇게 사용
    static public void LoadXml(string filepath, string type, ref object obj)
    {
        m_doc.Load(filepath);

        XmlElement nodes = m_doc["Root"];
        
        foreach (XmlElement node in nodes.ChildNodes)
        {
            switch(type)
            {
                case "Hero":
                    SetHeroFromXml((HeroHolder)obj, node);
                    break;
                case "Ability":
                    SetAbilityFromXml((AbilityHolder)obj, node);
                    break;
                case "Enemy":
                    break;
                case "Creature":
                    break;
                case "Card":
                    break;
            }

            foreach(XmlElement sub_node in node)
            {

            }
        }
    }

    static public void LoadXml(string filepath, ref HeroManager hero_manager)
    {
        m_doc.Load(filepath);

        XmlElement nodes = m_doc["Root"];

        foreach (XmlElement node in nodes.ChildNodes)
        {
            

            
        }
    }

    static void SetHeroFromXml(HeroHolder hero_holder, XmlElement node)
    {
        Hero hero = new Hero();

        hero.m_name = node.GetAttribute("Name");
        hero.m_health = (float)System.Convert.ToDouble(node.GetAttribute("Health"));
        hero.m_magic_power = (float)System.Convert.ToDouble(node.GetAttribute("MagicPower"));

        hero_holder.AddHero(hero);
    }

    static void SetAbilityFromXml(AbilityHolder ability_holder, XmlElement node)
    {
        Ability ability = new Ability();

        ability.m_name = node.GetAttribute("Name");
        ability.m_base_active_time = (float)System.Convert.ToDouble(node.GetAttribute("ActiveTime"));
        ability.m_base_cooldown_time = (float)System.Convert.ToDouble(node.GetAttribute("CoolDownTime"));

        ability_holder.AddAbility(ability);
    }
}
