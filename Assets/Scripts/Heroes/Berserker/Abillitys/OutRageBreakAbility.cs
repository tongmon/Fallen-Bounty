using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutRageBreakAbility : Ability
{
    public OutRageBreakAbility()//Ÿ���� ��ų
    {
        m_name = "OutRageBreak";

        m_base_cooldown_time = 8.0f;
        m_base_active_time = 0.3f;
        m_base_duration_time = 0.1f;

        m_base_physical_coefficient = 3.0f;
        m_base_range = 2.0f;
    }
    public override void Activate(GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
    }
 }
