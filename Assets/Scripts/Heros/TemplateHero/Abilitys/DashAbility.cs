using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public DashAbility()
    {
        m_name = GetType().Name;
    }
    public DashAbility(string name)
    {
        m_name = name;
    }
    public override void Activate(GameObject hero)
    {
        // 대쉬 스킬 로직
    }
}
