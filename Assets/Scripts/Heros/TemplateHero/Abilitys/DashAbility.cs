using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public DashAbility(float base_cool_time, float base_active_time)
    {
        m_name = GetType().Name;
        m_base_active_time = base_active_time;
        m_base_cooldown_time = base_cool_time;
    }

    public override void Activate(GameObject obj)
    {
        // 대쉬 스킬 로직
        Hero hero = obj.GetComponent<Hero>();
        
    }

    // 이름으로 접근
    public override void Activate(string object_name)
    {
        Type type = Type.GetType(object_name);
    }
}
