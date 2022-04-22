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
    public DashAbility(string name)
    {
        m_name = name;
    }
    public override void Activate(GameObject obj)
    {
        // 대쉬 스킬 로직
        TemplateHero hero = obj.GetComponent<TemplateHero>();
        
    }
    public override void Activate<T>(ref T obj)
    {
        
        return;
    }
}
