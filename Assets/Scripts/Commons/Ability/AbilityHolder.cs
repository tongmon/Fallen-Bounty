using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder
{
    public Ability m_ability;

    public enum eAbilityState
    {
        ready,
        active,
        cooldown
    }

    public eAbilityState m_state;

    // ���� ����
    public float m_cooltime;
    public float m_activetime;

    public AbilityHolder()
    {
        m_state = eAbilityState.ready;
    }

    public AbilityHolder(Ability ability, float cooltime, float activetime)
    {
        m_ability = ability;
        m_state = eAbilityState.ready;
        m_cooltime = cooltime;
        m_activetime = activetime;
    }

    public void Update(GameObject hero)
    {
        switch (m_state)
        {
            case eAbilityState.ready:
                m_ability.Activate(hero);
                m_state = eAbilityState.active;
                break;
            case eAbilityState.active:
                if (m_activetime > 0)
                {
                    m_activetime -= Time.deltaTime;
                }
                else
                {
                    m_state = eAbilityState.ready;
                }
                break;
            case eAbilityState.cooldown:
                break;
        }
    }
}
