using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using LitJson;

[Serializable]
public class ItemInfo : Info
{
    public string m_type = string.Empty;
    public float m_damage = 0.0f;
    public float m_range = 0.0f;
    public float m_duration = 0.0f;
    public float m_cooltime = 0.0f;

    public override void InfoSetting(int index, JsonData data)
    {
        base.InfoSetting(index, data);

        m_type = data[index]["m_type"].ToString();
        m_damage = float.Parse(data[index]["m_damage"].ToString());
        m_range = float.Parse(data[index]["m_range"].ToString());
        m_duration = float.Parse(data[index]["m_duration"].ToString());
        m_cooltime = float.Parse(data[index]["m_cooltime"].ToString());
    }

    public IEnumerator Activation(List<Hero> heroes, ItemInfo item, string type) //내 모든 히어로에게 적용
    {
        Debug.Log("아이템 사용 : " + item.m_info);
        while (true)
        {
            yield return null;
            //히어로 공격력 증가등 해야됨.
        }
    }

    public IEnumerator Activation(List <Hero> heroes ,ItemInfo item) //내 모든 히어로에게 적용
    {
        float time = 0.0f;
        Debug.Log("아이템 사용 : " + item.m_info);
        while (true)
        {
            yield return null;
            time+= Time.deltaTime;
            if (time >= item.m_duration) break;
            for (int i = 0; i < heroes.Count; i++)
            {
                heroes[i].m_current_health += item.m_damage;
            }
        }
    }
    public IEnumerator Activation(GameObject obj, ItemInfo item) //떨구는 아이템
    {
        Vector3 vec;
        while (true)
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = new Vector3(vec.x, vec.y, 0);
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                obj.AddComponent<Rigidbody2D>();
                obj.GetComponent<Rigidbody2D>().isKinematic = true;

                obj.AddComponent<CircleCollider2D>();
                obj.GetComponent<CircleCollider2D>().isTrigger = true;
                obj.GetComponent<CircleCollider2D>().radius *= item.m_range;//범위 조정

                obj.name = item.m_damage.ToString();//이름을 데미지로 저장
                obj.transform.tag = "Item";//충돌하는 애들을 위한 검사
                Debug.Log("아이템 사용 : " + item.m_info);
                Destroy(obj, item.m_duration);//지속시간 이후 삭제
                break;
            }
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
        }
    }
}