using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager
{
    // ��� ������ ��ų�� ���
    public List<Ability> m_abilities;
    public Dictionary<string, int> m_abilities_dict;
    public GameObject m_game_object;

    public AbilityManager(GameObject obj)
    {
        m_game_object = obj;
        m_abilities = new List<Ability>();
        m_abilities_dict = new Dictionary<string, int>();

        // json �о�ͼ� AddAbility() �Լ��� ����� ��� ������ ��ų(���� �������, �� �������) �ʱ�ȭ �������

    }

    public void AddAbility(string ability_script_name, float base_cool_time, float base_active_time)
    {
        Type type = Type.GetType(ability_script_name);
        object ability_instance = Activator.CreateInstance(type, base_cool_time, base_active_time);
        m_abilities_dict[ability_script_name] = m_abilities.Count;
        m_abilities.Add((Ability)ability_instance);
    }

    // �̸����� �����Ƽ ȹ��
    public Ability GetAbility(string ability_script_name)
    {
        return m_abilities[m_abilities_dict[ability_script_name]];
    }
}
