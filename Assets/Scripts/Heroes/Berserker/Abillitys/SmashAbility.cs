using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class SmashAbility : Ability
{

    public SmashAbility()
    {
        //m_category = "Active";
        m_name = "Smash";

        m_base_cooldown_time = 6.0f;
        m_base_active_time = 0.15f;
        m_base_duration_time = 0.15f;

        m_base_physical_coefficient = 1.25f;
        m_base_range = 2.5f;
    }

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
        skill.AddComponent<SpriteRenderer>();
        skill.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Image/Circle");
        skill.transform.localScale = new Vector3(m_base_range, m_base_range, 1);
        skill.AddComponent<Skill>();
        skill.GetComponent<Skill>().duration_time = m_base_duration_time;
        skill.GetComponent<Skill>().range = m_base_range;
        skill.GetComponent<Skill>().active_time = m_base_active_time;
        skill.GetComponent<Skill>().characterTarget = obj;
        skill.name = (m_base_physical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
    }
}
