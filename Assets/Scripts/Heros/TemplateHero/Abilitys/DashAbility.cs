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

    // DashAbility�� TemplateHero�� ��밡���� ��ų�̱⿡ �ذ� ���� ����
    public override void Activate(GameObject obj)
    {
        // �뽬 ��ų ����
        TemplateHero hero = obj.GetComponent<TemplateHero>();

    }

    /*
    // ���� ��ų�̸� �ذ� ���� Hero, �ƴϸ� Enemy�� �޾ƾߴ�

    // ���� ���� ��ų
    public override void Activate(GameObject obj)
    {
        // �뽬 ��ų ����
        Hero hero = obj.GetComponent<Hero>();
        
    }

    // �� ���� ��ų
    public override void Activate(GameObject obj)
    {
        // �뽬 ��ų ����
        Enemy hero = obj.GetComponent<Enemy>();
        
    }
    */
}
