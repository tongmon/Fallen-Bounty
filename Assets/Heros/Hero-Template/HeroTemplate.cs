using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTemplate : MonoBehaviour // 추후에 이 녀석 MonoBehaviour -> Hero로 변경
{
    #region 스킬 로직
    public Dictionary<string, AbilityHolder> m_abilities;
    public List<string> m_ability_order;

    void AddAbility(Ability ability, float cooltime, float activetime)
    {
        m_ability_order.Add(ability.GetType().Name);
        m_abilities[ability.GetType().Name] = new AbilityHolder(ability, cooltime, activetime);
    }

    void SkillUpdate(string name)
    {
        m_abilities[name].Update(gameObject);
    }

    void SkillUpdate(int skill_slot_index)
    {
        m_abilities[m_ability_order[skill_slot_index]].Update(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillUpdate(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SkillUpdate(1);
        }
    }
}
