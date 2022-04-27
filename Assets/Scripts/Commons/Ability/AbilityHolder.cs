using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų ����
public enum eAbilityState
{
    ready,
    active,
    cooldown,
    disabled
}

// �߰� ��ų ������
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

public class AbilityHolder
{
    // AbilityHolder�� ����ִ� ������ ���� ����ϴ� ��ų
    public List<AbilityData> m_abilities;

    // AbilityHolder�� ����ִ� ������ ���� ��������� ������ �����ϰ� �ִ� ��ų
    public List<AbilityData> m_sub_abilities;

    public Dictionary<string, int> m_abilities_dict;

    public Dictionary<string, int> m_sub_abilities_dict;

    public int m_abilities_limit;

    // ��ǻ� Creature
    public GameObject m_game_object;

    public AbilityHolder(GameObject obj)
    {
        // ��ü�̸�(m_game_object.name)�� ���� �����Ǵ� ��ų �������� json���� �о� ������
        // ex) pirate�� ���� ��ų���� �о� �˸��� AbilityData�� �־���
        m_game_object = obj;

        Creature creature = obj.GetComponent<Creature>();       
        m_abilities_limit = creature.m_abilities_limit;
        
        m_abilities_dict = new Dictionary<string, int>();
        m_sub_abilities_dict = new Dictionary<string, int>();
        
        m_abilities = new List<AbilityData>();
        m_sub_abilities = new List<AbilityData>();
    }

    // �ɷ� �߰�
    public void AddAbility(Ability ability)
    {
        AbilityData ability_data = new AbilityData(ability, ability.m_base_cooldown_time, ability.m_base_active_time);

        if(m_abilities.Count < m_abilities_limit)
        {
            m_abilities_dict[ability.m_name] = m_abilities.Count;
            m_abilities.Add(ability_data);
        }
        else
        {
            m_sub_abilities_dict[ability.m_name] = m_sub_abilities.Count;
            m_sub_abilities.Add(ability_data);
        }
    }

    // ��ũ��Ʈ �̸� ability_script_name�� �����ϴ� ��ų ����
    public void TriggerAbility(string ability_script_name)
    {
        if (m_abilities[m_abilities_dict[ability_script_name]].m_state == eAbilityState.ready)
            m_abilities[m_abilities_dict[ability_script_name]].m_is_triggered = true;
    }

    // �� �����Ӵ� ��ų ���� ����
    public void Update()
    {
        for (int i = 0; i < m_abilities.Count; i++)
            Update(i);
    }

    // Ư�� ��ų ����
    public void Update(int index)
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

    // Ȱ��ȭ ��ų ��ü
    public void SwitchAbility(string ability_script_name_one, string ability_script_name_two)
    {
        int one_abillity_index, two_abillity_index;
        AbilityData abilitydata_temp;
        if (m_abilities_dict.TryGetValue(ability_script_name_one, out one_abillity_index))
        {
            two_abillity_index = m_sub_abilities_dict[ability_script_name_two];

            m_abilities_dict[ability_script_name_one] = two_abillity_index;
            m_sub_abilities_dict[ability_script_name_two] = one_abillity_index;

            abilitydata_temp = m_abilities[one_abillity_index];
            m_abilities[one_abillity_index] = m_sub_abilities[two_abillity_index];
            m_sub_abilities[two_abillity_index] = abilitydata_temp;
            
            return;
        }
        one_abillity_index = m_sub_abilities_dict[ability_script_name_one];
        two_abillity_index = m_abilities_dict[ability_script_name_two];

        m_abilities_dict[ability_script_name_two] = one_abillity_index;
        m_sub_abilities_dict[ability_script_name_one] = two_abillity_index;

        abilitydata_temp = m_sub_abilities[one_abillity_index];
        m_sub_abilities[one_abillity_index] = m_abilities[two_abillity_index];
        m_abilities[two_abillity_index] = abilitydata_temp;
    }
}