using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 상태
public enum eAbilityState
{
    ready,
    active,
    cooldown,
    disabled
}

// 추가 스킬 데이터
public class AbilityData
{
    public eAbilityState m_state;
    public Ability m_ability;
    public float m_cur_cooldown_time;
    public float m_cur_active_time;
    public bool m_is_triggered; // TemplateHero 여기의 AbilityHolder에서 m_is_triggered만 true로 활성화 시켜주면 스킬 작동
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
    // AbilityHolder를 들고있는 영웅이 현재 사용하는 스킬
    public List<AbilityData> m_abilities;

    // AbilityHolder를 들고있는 영웅이 현재 사용하지는 않지만 보관하고 있는 스킬
    public List<AbilityData> m_sub_abilities;

    public Dictionary<string, int> m_abilities_dict;

    public Dictionary<string, int> m_sub_abilities_dict;

    public int m_abilities_limit;

    // 사실상 Creature
    public GameObject m_game_object;

    public AbilityHolder(GameObject obj)
    {
        // 객체이름(m_game_object.name)에 따라 대응되는 스킬 변수들을 json에서 읽어 가져옴
        // ex) pirate은 해적 스킬셋을 읽어 알맞은 AbilityData에 넣어줌
        m_game_object = obj;

        Creature creature = obj.GetComponent<Creature>();       
        m_abilities_limit = creature.m_abilities_limit;
        
        m_abilities_dict = new Dictionary<string, int>();
        m_sub_abilities_dict = new Dictionary<string, int>();
        
        m_abilities = new List<AbilityData>();
        m_sub_abilities = new List<AbilityData>();
    }

    // 능력 추가
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

    // 스크립트 이름 ability_script_name에 대응하는 스킬 시전
    public void TriggerAbility(string ability_script_name)
    {
        if (m_abilities[m_abilities_dict[ability_script_name]].m_state == eAbilityState.ready)
            m_abilities[m_abilities_dict[ability_script_name]].m_is_triggered = true;
    }

    // 매 프레임당 스킬 상태 갱신
    public void Update()
    {
        for (int i = 0; i < m_abilities.Count; i++)
            Update(i);
    }

    // 특정 스킬 갱신
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

    // 활성화 스킬 교체
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