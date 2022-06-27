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
    // ���� ���ݷ�
    public int physic_power;
    // ���� ���ݷ�
    public int magic_power;
    // ��Ÿ �ӵ�, �� ����
    public float attack_cooltime;
    // ���� ����
    public float melee_range;
    // ���� ����
    public float ranged_range;
    #endregion
}

public class Hero : Creature
{
    // ������ ����
    public HeroCommandManager.eMoveState m_state_move;
    // ĳ���� Ÿ����
    public Creature m_target;
    // Ÿ�� ��ġ
    public Vector2 m_point_target;
    // ���� �ӵ�
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