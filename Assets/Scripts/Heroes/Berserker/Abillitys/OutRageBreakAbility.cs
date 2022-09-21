using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OutRageBreakAbility : Ability
{
    public OutRageBreakAbility()//타겟팅 스킬
    {
        m_name = "OutRageBreak";

        m_base_cooldown_time = 8.0f;
        m_base_active_time = 0.3f;

        m_base_physical_coefficient = 3.0f;
        m_base_range = 5.0f;
    }
    public override void Activate(GameObject obj)
    {
        BerserkerData bdata = obj.GetComponent<Berserker>().berserker_data;

        GameObject skill = new GameObject();
        GameObject skill_range = new GameObject();

        skill.AddComponent<Skill>();
        skill.GetComponent<Skill>().style = "Target";
        skill.GetComponent<Skill>().damage = m_base_physical_coefficient * bdata.physic_power;
        skill.GetComponent<Skill>().m_character = obj;
        skill.GetComponent<Skill>().hdata = bdata;
        skill.tag = "Skill";
        skill.name = (m_base_physical_coefficient * bdata.physic_power).ToString();//혹시 모르니 이름도 데미지화

        skill_range.name = "SkillRange";
        skill_range.transform.localScale = new Vector3(m_base_range, m_base_range, 1);//스킬 나중에 타원으로 설정해야함
        skill_range.AddComponent<SpriteRenderer>();
        skill_range.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Image/Circle");
        skill_range.GetComponent<SpriteRenderer>().sortingLayerName = "Stage";
        skill_range.GetComponent<SpriteRenderer>().sortingOrder = 2;
        skill_range.AddComponent<CircleCollider2D>();
        skill_range.GetComponent<CircleCollider2D>().isTrigger = true;
        skill_range.AddComponent<Rigidbody2D>();
        skill_range.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        skill_range.AddComponent<Range>();
        skill_range.GetComponent<Range>().obj = obj;
    }
 }
