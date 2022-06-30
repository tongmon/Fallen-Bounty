using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(RangerData), "weakness_popup_cooltime")]
public class HeroData : CreatureData
{
    #region Data from JSON file
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
    #endregion
}

public class Hero : Creature
{
    // 공격 속도
    public float m_cur_attack_cooltime;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_point_target = Vector2.zero;

        m_cur_attack_cooltime = 0;
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {

    }

    protected override void OnFixedUpdate()
    {
        
    }
}