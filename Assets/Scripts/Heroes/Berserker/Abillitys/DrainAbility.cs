using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DrainAbility : Ability
{
    public DrainAbility()
    {
        m_category = "Active";
        m_name = "Drain";

        m_base_cooldown_time = 5.0f;
        m_base_active_time = 0.1f;
        m_base_duration_time = 0.15f;

        m_base_physical_coefficient = 1.0f;
        m_base_range = 2.0f;
    }

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        Player player = obj.GetComponent<Player>();
        
        int m_hit_count = 0;//초기화

        float delay = 0;
        //애니메이션 동작
        while (true)//코루틴 대신
        {
            if (delay > m_base_active_time) break;//나중에 애니메이션 시간빼야함.
            delay += Time.deltaTime;
        }

        BerserkerData bdata = obj.GetComponent<Berserker>().berserker_data;
        
        GameObject skill = new GameObject();
        skill.transform.localScale = new Vector3(m_base_range, m_base_range, 1);
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//트리거로 탐지해야 됨.
        skill.name = (m_base_physical_coefficient * bdata.physic_power * player.m_all_stat_coefficent).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//맞은애 *10만큼 피회복.
        Destroy(skill, m_base_duration_time);//지속시간 이후 삭제
    }
}
