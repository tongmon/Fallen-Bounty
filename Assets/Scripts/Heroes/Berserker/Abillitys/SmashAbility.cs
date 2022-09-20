using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SmashAbility : Ability
{

    public SmashAbility()//시전시간 있는 범위기
    {
        //m_category = "Active";
        m_name = "Smash";

        m_base_cooldown_time = 6.0f;
        m_base_active_time = 0.15f;
        m_base_duration_time = 0.15f;

        m_base_physical_coefficient = 1.25f;
        m_base_active_range = 1.5f;
        m_base_range = 3.0f;
    }

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        BerserkerData bdata = obj.GetComponent<Berserker>().berserker_data;

        GameObject skill = new GameObject();
        GameObject skill_range = new GameObject();

        skill.AddComponent<SpriteRenderer>();
        skill.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Image/Circle");
        skill.transform.localScale = new Vector3(m_base_active_range, m_base_active_range, 1);
        skill.tag = "Skill";
        skill.AddComponent<Skill>();
        skill.GetComponent<Skill>().style = "Range";
        skill.GetComponent<SpriteRenderer>().sortingLayerName = "Stage";
        skill.GetComponent<SpriteRenderer>().sortingOrder = 2;
        skill.GetComponent<Skill>().duration_time = m_base_duration_time;
        skill.GetComponent<Skill>().range = m_base_active_range;
        skill.GetComponent<Skill>().active_time = m_base_active_time;
        skill.GetComponent<Skill>().m_character = obj;
        skill.name = (m_base_physical_coefficient * bdata.physic_power).ToString();

        skill_range.name = "SkillRange";
        skill_range.transform.localScale = new Vector3(m_base_range, m_base_range, 1);//스킬 나중에 타원으로 설정해야함
        skill_range.AddComponent<SpriteRenderer>();
        skill_range.GetComponent<SpriteRenderer>().sortingLayerName = "Stage";
        skill_range.GetComponent<SpriteRenderer>().sortingOrder = 2;
        skill_range.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Image/Circle");
        skill_range.AddComponent<CircleCollider2D>();
        skill_range.GetComponent<CircleCollider2D>().isTrigger = true;
        skill_range.AddComponent<Rigidbody2D>();
        skill_range.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        skill_range.AddComponent<Range>();
        skill_range.GetComponent<Range>().obj = obj;
    }
}
