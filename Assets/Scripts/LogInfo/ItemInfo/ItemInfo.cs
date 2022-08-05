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

    public IEnumerator Activation(Hero[] heroes, ItemInfo item) //�� ��� ����ο��� ����
    {
        
        yield return null;
    }
    public IEnumerator Activation(Hero hero ,ItemInfo item) //�� ����ο��� ����
    {
        yield return null;
    }
    public IEnumerator Activation(GameObject obj, ItemInfo item) //������ ������
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
                obj.GetComponent<CircleCollider2D>().radius *= item.m_range;//���� ����

                obj.name = item.m_damage.ToString();//�̸��� �������� ����
                obj.transform.tag = "Item";//�浹�ϴ� �ֵ��� ���� �˻�
                Debug.Log("������ ��� : " + item.m_info);
                Destroy(obj, item.m_duration);//���ӽð� ���� ����
                break;
            }
        }
    }
}