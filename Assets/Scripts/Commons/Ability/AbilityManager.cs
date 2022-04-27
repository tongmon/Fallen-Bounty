using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager
{
    // 모든 종류의 스킬이 담김
    public List<Ability> m_abilities;
    public Dictionary<string, int> m_abilities_dict;
    public GameObject m_game_object;

    public AbilityManager(GameObject obj)
    {
        m_game_object = obj;
        m_abilities = new List<Ability>();
        m_abilities_dict = new Dictionary<string, int>();

        // json 읽어와서 AddAbility() 함수를 사용해 모든 종류의 스킬(직업 상관없이, 적 상관없이) 초기화 해줘야함

    }

    public void AddAbility(string ability_script_name, float base_cool_time, float base_active_time)
    {
        Type type = Type.GetType(ability_script_name);
        object ability_instance = Activator.CreateInstance(type, base_cool_time, base_active_time);
        m_abilities_dict[ability_script_name] = m_abilities.Count;
        m_abilities.Add((Ability)ability_instance);
    }

    // 이름으로 어빌리티 획득
    public Ability GetAbility(string ability_script_name)
    {
        return m_abilities[m_abilities_dict[ability_script_name]];
    }
}
