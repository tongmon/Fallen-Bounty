using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬
public class Ability : ScriptableObject
{
    public string m_name;

    // 저장된 상태, 고정값
    public float m_base_cooldown_time;
    public float m_base_active_time;

    public virtual void Activate(GameObject obj) { }
}
