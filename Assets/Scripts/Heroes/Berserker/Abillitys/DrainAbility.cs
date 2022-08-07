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

    public override void Activate(GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        Berserker skill_user = obj.GetComponent<Berserker>();

        GameObject skill = Instantiate(new GameObject(), obj.transform.position, Quaternion.Euler(0, 0, 0));
        //skill.AddComponent<Rigidbody2D>();
        //skill.GetComponent<Rigidbody2D>().isKinematic = true;
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� �˻��ؾ� ��.
        //skill.transform.localScale = new Vector2(m_base_range.x, m_base_range.y);
        //skill.name = m_base_phhsical_coefficient * ���ݷ� ���������.
        skill.tag = "Skill";
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
}
