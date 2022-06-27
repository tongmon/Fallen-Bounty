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
    // 움직임 상태
    public HeroCommandManager.eMoveState m_state_move;
    // 캐릭터 타겟팅
    public Creature m_target;
    // 타겟 위치
    public Vector2 m_point_target;
    // 공격 속도
    public float m_cur_attack_cooltime;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_state_move = HeroCommandManager.eMoveState.STATE_MOVE_NONE;
        m_target = null;

        m_point_target = transform.position;

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