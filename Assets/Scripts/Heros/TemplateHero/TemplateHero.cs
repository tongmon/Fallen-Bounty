using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : Hero
{
    AbilityHolder m_ability_holder;

    void Start()
    {
        m_ability_holder = new AbilityHolder(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_ability_holder.TriggerAbility("DashAbility");
        }

        // ��ų ���� ������Ʈ
        m_ability_holder.Update();
    }
}
