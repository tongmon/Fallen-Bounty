using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : Hero
{
    public TemplateHero()
    {
        m_name = GetType().Name;
        m_ability_holder = new AbilityHolder(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_ability_holder.TriggerAbility("DashAbility");
        }

        // 스킬 로직 업데이트
        m_ability_holder.Update();
    }
}
