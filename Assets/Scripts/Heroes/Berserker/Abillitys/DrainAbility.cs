using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainAbility : Ability
{
    public DrainAbility()
    {
        //m_category = "Active";
        m_name = "Drain";

        m_base_cooldown_time = 3.0f;
        m_base_active_time = 0.1f;
        m_base_duration_time = 2.0f;

        m_base_phhsical_coefficient = 1.5f;
        m_base_range = 3.0f;
    }

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//트리거로 탐지해야 됨.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range;
        skill.name = (m_base_phhsical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//맞은애 *10만큼 피회복.
        Destroy(skill, m_base_duration_time);//지속시간 이후 삭제
    }
}
