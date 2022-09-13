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

    public override void Activate(GameObject obj)//obj에 자기자신 넣어야할듯
    {
        m_hit_count = 0;//초기화

        Vector3 target_tr;

        image = new GameObject();//소환
        image.transform.localPosition = Input.mousePosition;

        image.AddComponent<Image>();
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Image");
        image.transform.localScale = new Vector3(m_base_range, m_base_range, 1);

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(image);
                target_tr = Input.mousePosition;
                break;
            }
            if (Input.GetMouseButtonDown(1))//오른쪽 버튼시 취소.
            {
                Destroy(image);
                return;
            }
            image.transform.localPosition = Input.mousePosition;//마우스 따라다니기
        }

        float delay = 0;
        //애니메이션 동작
        while (true)//코루틴 대신
        {
            if (delay > m_base_active_time) break;//나중에 애니메이션 시간빼야함.
            delay += Time.deltaTime;
        }

        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
        skill.transform.localPosition = target_tr;
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//트리거로 탐지해야 됨.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range;
        skill.name = (m_base_physical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//맞은애 *10만큼 피회복.
        Destroy(skill, m_base_duration_time);//지속시간 이후 삭제
    }
    
}
