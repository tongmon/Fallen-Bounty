using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų

public class Ability : Component
{
    // ��ų ī�װ�, ���� ��ų����, � ������ ��ų����...
    // ī�� ������ �� �Ŀ� Ư�� ������ ��ȭ�ϴ� ����, ��ü ������ ��ȭ�ϴ� ���� ������ ������ ô��

    public int m_hit_count;
    public string m_category;

    // ��ų �̸�
    public string m_name;

    // ����� ����, ������
    public float m_base_cooldown_time;
    public float m_base_active_time;//���� �ð�
    public float m_base_duration_time;//���� �ð�

    public float m_base_active_range;//Ÿ�� ����
    public float m_base_range;//���� ����
    public float m_base_physical_coefficient;//���� ���
    public float m_base_magic_coefficient;//���� ���


    public virtual void Activate(GameObject obj) 
    {

    }
}
