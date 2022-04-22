using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string m_name;

    // ����� ����, ������
    public float m_base_cooldown_time;
    public float m_base_active_time;

    public virtual void Activate(GameObject obj) { }

    public virtual void Activate(string object_name) { }
}
