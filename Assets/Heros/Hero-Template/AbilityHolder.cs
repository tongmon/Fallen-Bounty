using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder
{
    public Ability m_ability;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState m_state;

    // 현재 상태
    public float m_cooltime;
    public float m_activetime;

    public AbilityHolder()
    {
        m_state = AbilityState.ready;
    }

    public AbilityHolder(Ability ability, float cooltime, float activetime)
    {
        m_ability = ability;
        m_state = AbilityState.ready;
        m_cooltime = cooltime;
        m_activetime = activetime;
    }

    public void Update(GameObject hero)
    {
        switch (m_state)
        {
            case AbilityState.ready:
                m_ability.Activate(hero);
                m_state = AbilityState.active;
                break;
            case AbilityState.active:
                if (m_activetime > 0)
                {
                    m_activetime -= Time.deltaTime;
                }
                else
                {
                    m_state = AbilityState.ready;
                }
                break;
            case AbilityState.cooldown:
                break;
        }
    }
}
