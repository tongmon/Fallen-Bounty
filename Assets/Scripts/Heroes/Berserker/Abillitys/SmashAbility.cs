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
        m_hit_count = 0;//�ʱ�ȭ

        Vector3 target_tr;

        image = new GameObject();//��ȯ
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
            if (Input.GetMouseButtonDown(1))//������ ��ư�� ���.
            {
                Destroy(image);
                return;
            }
            image.transform.localPosition = Input.mousePosition;//���콺 ����ٴϱ�
        }

        float delay = 0;
        //�ִϸ��̼� ����
        while (true)//�ڷ�ƾ ���
        {
            if (delay > m_base_active_time) break;//���߿� �ִϸ��̼� �ð�������.
            delay += Time.deltaTime;
        }

        HeroData heroData = (HeroData)obj.GetComponent<Hero>().m_data;

        GameObject skill = new GameObject();
        skill.transform.localPosition = target_tr;
        skill.AddComponent<CircleCollider2D>();
        skill.GetComponent<CircleCollider2D>().isTrigger = true;//Ʈ���ŷ� Ž���ؾ� ��.
        skill.GetComponent<CircleCollider2D>().radius *= m_base_range;
        skill.name = (m_base_physical_coefficient * heroData.physic_power).ToString();
        skill.tag = "Skill";
        obj.GetComponent<Hero>().m_current_health += m_hit_count * 10;//������ *10��ŭ ��ȸ��.
        Destroy(skill, m_base_duration_time);//���ӽð� ���� ����
    }
    
}
