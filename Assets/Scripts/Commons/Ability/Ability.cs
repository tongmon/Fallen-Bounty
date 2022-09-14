using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬

public class Ability : Component
{
    // 스킬 카테고리, 공용 스킬인지, 어떤 직업의 스킬인지...
    // 카드 선택을 한 후에 특정 영웅을 강화하는 건지, 전체 영웅을 강화하는 건지 로직을 가르는 척도

    public int m_hit_count;
    public string m_category;

    // 스킬 이름
    public string m_name;

    // 저장된 상태, 고정값
    public float m_base_cooldown_time;
    public float m_base_active_time;//시전 시간
    public float m_base_duration_time;//지속 시간

    public float m_base_active_range;//타격 범위
    public float m_base_range;//적용 범위
    public float m_base_physical_coefficient;//물리 계수
    public float m_base_magic_coefficient;//마법 계수


    public virtual void Activate(GameObject obj) 
    {

    }
}
