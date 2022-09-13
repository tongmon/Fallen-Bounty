using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrainAbility : Ability
{
    public DrainAbility()
    {
        //m_category = "Active";
        m_name = "Drain";

        m_base_cooldown_time = 5.0f;
        m_base_active_time = 0.1f;
        m_base_duration_time = 0.15f;

        m_base_physical_coefficient = 1.0f;
        m_base_range = 3.0f;
    }

    public override void Activate(GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        m_hit_count = 0;//�ʱ�ȭ

        float delay = 0;
        //�ִϸ��̼� ����
        while (true)//�ڷ�ƾ ���
        {
            if (delay > m_base_active_time) break;//���߿� �ִϸ��̼� �ð�������.
            delay += Time.deltaTime;
        }

        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� Ž���ؾ� ��.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range;
        skill.name = (m_base_physical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//������ *10��ŭ ��ȸ��.
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
}
