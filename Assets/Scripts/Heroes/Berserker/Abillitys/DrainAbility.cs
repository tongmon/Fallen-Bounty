using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainAbility : Ability
{
    public Vector2 m_base_range; //���� ����

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

    public override void Activate( GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;
    
        GameObject skill = Instantiate(new GameObject(), obj.transform.position, Quaternion.Euler(0, 0, 0));
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� �˻��ؾ� ��.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range.x;
        skill.name = (m_base_phhsical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
}
