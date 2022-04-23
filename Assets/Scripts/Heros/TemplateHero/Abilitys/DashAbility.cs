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

    // DashAbility는 TemplateHero만 사용가능한 스킬이기에 밑과 같이 구현
    public override void Activate(GameObject obj)
    {
        // 대쉬 스킬 로직
        TemplateHero hero = obj.GetComponent<TemplateHero>();

    }

    /*
    // 공용 스킬이면 밑과 같이 Hero, 아니면 Enemy로 받아야댐

    // 영웅 공통 스킬
    public override void Activate(GameObject obj)
    {
        // 대쉬 스킬 로직
        Hero hero = obj.GetComponent<Hero>();
        
    }

    // 적 공통 스킬
    public override void Activate(GameObject obj)
    {
        // 대쉬 스킬 로직
        Enemy hero = obj.GetComponent<Enemy>();
        
    }
    */
}
