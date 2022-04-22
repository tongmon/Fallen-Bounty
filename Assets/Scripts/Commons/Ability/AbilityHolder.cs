using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAbilityState
{
    ready,
    active,
    cooldown,
    disabled
}

public class AbilityData
{
    public eAbilityState m_state;
    public Ability m_ability;
    public float m_cur_cooldown_time;
    public float m_cur_active_time;
    public bool m_is_triggered; // TemplateHero ������ AbilityHolder���� m_is_triggered�� true�� Ȱ��ȭ �����ָ� ��ų �۵�
    public AbilityData(Ability ability, float cur_cooldown_time, float cur_active_time)
    {
        m_state = eAbilityState.disabled;
        m_is_triggered = false;
        m_ability = ability;
        m_cur_cooldown_time = cur_cooldown_time;
        m_cur_active_time = cur_active_time;
    }
}

// �ʿ��� ��ų���� ��� �о� m_abilities ���⿡ �����ϰ� ����
public class AbilityHolder
{
    public List<AbilityData> m_abilities;
    public Dictionary<string, int> m_abilities_dict;
    public GameObject m_game_object;

    public AbilityHolder(GameObject obj)
    {
        // ��ü�̸�(m_game_object.name)�� ���� �����Ǵ� ��ų �������� json���� �о� ������
        // ex) pirate�� ���� ��ų���� �о� �˸��� AbilityData�� �־���
        m_game_object = obj;
    }

    public void AddAbility(string ability_script_name, float base_cool_time, float base_active_time)
    {
        Type type = Type.GetType(ability_script_name);
        object ability_instance = Activator.CreateInstance(type, base_cool_time, base_active_time);
        m_abilities_dict[ability_script_name] = m_abilities.Count;

        AbilityData ability_data = new AbilityData((Ability)ability_instance, base_cool_time, base_active_time);

        m_abilities.Add(ability_data);
    }

    public void TriggerAbility(string ability_script_name)
    {
        if (m_abilities[m_abilities_dict[ability_script_name]].m_state != eAbilityState.disabled)
            m_abilities[m_abilities_dict[ability_script_name]].m_is_triggered = true;
    }

    public void Update()
    {
        // ���� ����� ��ų���� �ε����� ���� �� ������ for�� �˻��ϸ� �� ����ȭ ����
        for (int i = 0; i < m_abilities.Count; i++)
            Update(i);
    }

    void Update(int index)
    {
        switch (m_abilities[index].m_state)
        {
            case eAbilityState.disabled:
                break;
            case eAbilityState.ready:
                if (m_abilities[index].m_is_triggered)
                {
                    m_abilities[index].m_is_triggered = false;
                    m_abilities[index].m_ability.Activate(m_game_object);
                    m_abilities[index].m_state = eAbilityState.active; 
                }
                break;
            case eAbilityState.active:
                if (m_abilities[index].m_cur_active_time > 0) 
                {
                    m_abilities[index].m_cur_active_time -= Time.deltaTime;
                }
                else 
                {
                    m_abilities[index].m_state = eAbilityState.cooldown;
                    m_abilities[index].m_cur_active_time = m_abilities[index].m_ability.m_base_active_time;
                }
                break;
            case eAbilityState.cooldown:
                if (m_abilities[index].m_cur_cooldown_time > 0) 
                {
                    m_abilities[index].m_cur_cooldown_time -= Time.deltaTime;
                }
                else
                {
                    m_abilities[index].m_state = eAbilityState.ready;
                    m_abilities[index].m_cur_cooldown_time = m_abilities[index].m_ability.m_base_cooldown_time;
                }
                break;
        }
    }
}

/*
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
    public float m_cooldown_time;
    public float m_active_time;

    public AbilityHolder()
    {
        m_state = eAbilityState.ready;
    }

    public AbilityHolder(Ability ability, float cooldown_time, float active_time)
    {
        m_ability = ability;
        m_state = eAbilityState.ready;
        m_cooldown_time = cooldown_time;
        m_active_time = active_time;
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
                if (m_active_time > 0)
                {
                    m_active_time -= Time.deltaTime;
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
*/