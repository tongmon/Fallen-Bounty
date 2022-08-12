using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainAbility : Ability
{
     //적용 범위, 추가로 스킬범위 알려주는 스크립트 필요.

    public DrainAbility()
    {
        //m_category = "Active";
        m_name = "Drain";

        m_base_cooldown_time = 3.0f;
        m_base_active_time = 0.1f;
        m_base_duration_time = 2.0f;

        m_base_phhsical_coefficient = 1.5f;
        m_base_range = new Vector2(1.5f, 1.5f);
    }

    public override void Activate( GameObject obj)//obj에 자기자신 넣어야할듯
    {
        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;
    
        GameObject skill = Instantiate(new GameObject(), obj.transform.position, Quaternion.Euler(0, 0, 0));
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//트리거로 탐지해야 됨.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range.x;
        skill.name = (m_base_phhsical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        Destroy(skill, m_base_duration_time);//지속시간 이후 삭제
    }
}
