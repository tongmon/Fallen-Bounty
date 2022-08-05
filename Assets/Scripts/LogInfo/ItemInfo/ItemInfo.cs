using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu]
public class ItemInfo : Info
{
    public string m_type = string.Empty;
    public float m_damage = 0.0f;
    public float m_range = 0.0f;
    public float m_duration = 0.0f;
    public float m_cooltime = 0.0f;

    public IEnumerator Activation(Hero[] heroes, ItemInfo item) //내 모든 히어로에게 적용
    {
        
        yield return null;
    }
    public IEnumerator Activation(Hero hero ,ItemInfo item) //내 히어로에게 적용
    {
        yield return null;
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
        }
    }
}