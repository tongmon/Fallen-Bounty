using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų
public class Ability : ScriptableObject
{
    // ��ų ī�װ�, ���� ��ų����, � ������ ��ų����...
    // ī�� ������ �� �Ŀ� Ư�� ������ ��ȭ�ϴ� ����, ��ü ������ ��ȭ�ϴ� ���� ������ ������ ô��
    public string m_category;

    // ��ų �̸�
    public string m_name;

    // ����� ����, ������
    public float m_base_cooldown_time;
    public float m_base_active_time;

    public virtual void Activate(GameObject obj) { }
}
