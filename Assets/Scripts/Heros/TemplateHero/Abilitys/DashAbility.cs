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
        // �뽬 ��ų ����
        Hero hero = obj.GetComponent<Hero>();
        
    }

    // �̸����� ����
    public override void Activate(string object_name)
    {
        Type type = Type.GetType(object_name);
    }
}
