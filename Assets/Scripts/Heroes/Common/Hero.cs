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
    // Ÿ�� ��ġ
    public Vector2 m_point_target;
    // ���콺 Ȧ�� �ð�
    public float[] m_mouse_hold_time;
    // ������ ����
    public HeroCommandManager.eMoveState m_state_move;
    // ĳ���� Ÿ���� ���
    public GameObject m_target_enemy;
    // ĳ���� �巡�� ��
    public LineRenderer m_line_renderer;
    // �巡�� �� ����
    public float m_dragline_alpha;
    // �巡�� �� ��ǥ��
    public Vector2 m_dragging_point;
    // ���� �ӵ�
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