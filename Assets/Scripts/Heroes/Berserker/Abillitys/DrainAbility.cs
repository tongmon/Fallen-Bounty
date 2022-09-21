using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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
        m_base_range = 2.0f;
    }

    public override void Activate(GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        int m_hit_count = 0;//�ʱ�ȭ

        float delay = 0;
        //�ִϸ��̼� ����
        while (true)//�ڷ�ƾ ���
        {
            if (delay > m_base_active_time) break;//���߿� �ִϸ��̼� �ð�������.
            delay += Time.deltaTime;
        }

        BerserkerData bdata = obj.GetComponent<Berserker>().berserker_data;

        GameObject skill = new GameObject();
        skill.transform.localScale = new Vector3(m_base_range, m_base_range, 1);
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� Ž���ؾ� ��.
        skill.name = (m_base_physical_coefficient * bdata.physic_power).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//������ *10��ŭ ��ȸ��.
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
}
