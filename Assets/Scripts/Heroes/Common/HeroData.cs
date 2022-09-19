using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData : CreatureData
{
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
