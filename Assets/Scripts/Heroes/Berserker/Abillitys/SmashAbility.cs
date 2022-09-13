using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SmashAbility : Ability
{
    GameObject image;

    public SmashAbility()
    {
        //m_category = "Active";
        m_name = "Smash";

        m_base_cooldown_time = 6.0f;
        m_base_active_time = 0.15f;
        m_base_duration_time = 0.15f;

        m_base_physical_coefficient = 1.25f;
        m_base_range = 2.5f;
    }

    public override void Activate(GameObject obj)//obj�� �ڱ��ڽ� �־���ҵ�
    {
        Vector3 target_tr = Vector3.zero;

        GameObject skill = new GameObject();

        image = new GameObject();//��ȯ
        image.tag = "SkillToolTip";
        image.AddComponent<Image>();
        image.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Image/Circle");
        image.transform.localScale = new Vector3(m_base_range, m_base_range, 1);

        skill.AddComponent<Skill>();
        skill.GetComponent<Skill>().coroutine = SkillAct(obj,skill,target_tr);
    }
    IEnumerator SkillAct(GameObject obj,GameObject skill, Vector3 target_tr)
    {
        //�ִϸ��̼� ����

        yield return new WaitForSecondsRealtime(m_base_active_time); //�ִϸ��̼� �ð� �������.

        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        skill.transform.localPosition = target_tr;
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� Ž���ؾ� ��.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range;
        skill.name = (m_base_physical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
}
