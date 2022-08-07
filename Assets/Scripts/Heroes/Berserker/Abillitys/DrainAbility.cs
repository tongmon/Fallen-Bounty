using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainAbility : Ability
{
    public Vector2 m_base_range; //적용 범위

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

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        Berserker skill_user = obj.GetComponent<Berserker>();

        GameObject skill = Instantiate(new GameObject(), obj.transform.position, Quaternion.Euler(0, 0, 0));
        //skill.AddComponent<Rigidbody2D>();
        //skill.GetComponent<Rigidbody2D>().isKinematic = true;
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//트리거로 검새해야 됨.
        //skill.transform.localScale = new Vector2(m_base_range.x, m_base_range.y);
        //skill.name = m_base_phhsical_coefficient * 공격력 곱해줘야함.
        skill.tag = "Skill";
        Destroy(skill, m_base_duration_time);//지속시간 이후 삭제
    }
}
