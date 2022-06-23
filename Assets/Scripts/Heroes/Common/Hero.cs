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
    // 타겟 위치
    public Vector2 m_point_target;
    // 마우스 홀딩 시간
    public float[] m_mouse_hold_time;
    // 움직임 상태
    public HeroCommandManager.eMoveState m_state_move;
    // 캐릭터 타겟팅 상대
    public GameObject m_target_enemy;
    // 캐릭터 드래깅 선
    public LineRenderer m_line_renderer;
    // 드래깅 선 투명도
    public float m_dragline_alpha;
    // 드래깅 선 목표점
    public Vector2 m_dragging_point;
    // 공격 속도
    public float m_cur_attack_cooltime;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_line_renderer = GetComponent<LineRenderer>();
        m_line_renderer.startWidth = 0.05f;
        m_line_renderer.endWidth = 0.05f;

        m_sprite_seleted_circle = transform.Find("FocusCircle").GetComponent<SpriteRenderer>();

        m_state_move = HeroCommandManager.eMoveState.STATE_MOVE_NONE;
        m_target_enemy = null;

        m_point_target = transform.position;
        m_mouse_hold_time = new float[2];

        m_dragline_alpha = 0.0f;
        m_dragging_point = new Vector2();

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
        if (m_state_move != HeroCommandManager.eMoveState.STATE_MOVE_NONE)
        {
            m_rigidbody.velocity = m_vec_direction.normalized * new Vector2(((HeroData)m_data).x_velocity, ((HeroData)m_data).y_velocity);
        }
        else
        {
            m_rigidbody.velocity = new Vector2(0, 0);
        }
    }
}