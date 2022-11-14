using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData : CreatureData
{
    //공격 물리계수 -- 카드로 업글가능
    public float physic_coefficient;
    //마법 물리계수
    public float magic_coefficient;
    // 물리 공격력
    public int physic_power;
    // 마법 공격력
    public int magic_power;
    // 평타 속도, 초 단위
    public float attack_cooltime;
    // 공격 범위
    public float melee_range;
    // 공격 범위
    public float ranged_range;

    public string m_info;
}
