using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬
public class Ability : ScriptableObject
{
    // 스킬 카테고리, 공용 스킬인지, 어떤 직업의 스킬인지...
    public string m_category;

    // 스킬 이름
    public string m_name;

    // 저장된 상태, 고정값
    public float m_base_cooldown_time;
    public float m_base_active_time;

    public virtual void Activate(GameObject obj) { }
}
