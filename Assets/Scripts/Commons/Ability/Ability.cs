using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų
[CreateAssetMenu]
public class Ability : ScriptableObject
{
    // ��ų ī�װ�, ���� ��ų����, � ������ ��ų����...
    // ī�� ������ �� �Ŀ� Ư�� ������ ��ȭ�ϴ� ����, ��ü ������ ��ȭ�ϴ� ���� ������ ������ ô��
    

    public string m_category;

    // ��ų �̸�
    public string m_name;

    // ����� ����, ������
    public float m_base_cooldown_time;
    public float m_base_active_time;//���� �ð�
    public float m_base_duration_time;//���� �ð�

    public float m_base_range;//���� ����
    public float m_base_phhsical_coefficient;//���� ���
    public float m_base_magic_coefficient;//���� ���

    public float m_hit_count;
    public virtual void Activate(GameObject obj) 
    { 
       m_hit_count = 0;
    }
}
