using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : MonoBehaviour // 추후에 이 녀석 MonoBehaviour -> Hero로 변경
{
    #region 스킬 로직
    public Dictionary<string, AbilityHolder> m_abilities;
    public List<string> m_ability_order;

    void AddAbility(Ability ability, float cooltime, float activetime)
    {
        m_ability_order.Add(ability.m_name);
        m_abilities[ability.m_name] = new AbilityHolder(ability, cooltime, activetime);
    }

    void AbilityUpdate(string name)
    {
        m_abilities[name].Update(gameObject);
    }

    void AbilityUpdate(int skill_slot_index)
    {
        m_abilities[m_ability_order[skill_slot_index]].Update(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        AddAbility(new DashAbility(), 3.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AbilityUpdate(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AbilityUpdate(1);
        }
    }
}
