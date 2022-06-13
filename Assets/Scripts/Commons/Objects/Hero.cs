using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

/*
[JsonConverter(typeof(JsonSubtypes))]
public class Hero : Creature
{
    #region Data from JSON file
    // 물리 공격력
    public float physic_power;
    // 마법 공격력
    public float magic_power;
    // 평타 속도
    public float attack_speed;   
    // 공격 범위
    public float attack_range;
    #endregion

    // 타겟 위치
    public Vector2 m_point_target;
    // 마우스 홀딩 시간
    public float[] m_mouse_hold_time;
    // 움직임 상태
    public HeroCommandManager.eMoveState m_state_move;
    // 캐릭터 타게팅 상태
    public GameObject m_target_enemy;
    // 캐릭터 드래깅 선
    public LineRenderer m_line_renderer;
    // 드래깅 선 투명도
    public float m_dragline_alpha;
    // 드래깅 선 목표점
    public Vector2 m_dragging_point;

    protected new void Awake()
    {
        base.Awake();

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

        x_velocity = 1f;
        y_velocity = 1.5f;
        attack_range = 3f;
    }

    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();

        // 영웅 좌우 방향 바꾸기
        if (m_point_target.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        if (m_state_move != HeroCommandManager.eMoveState.STATE_MOVE_NONE)
        {
            m_vec_direction.Normalize();
            transform.Translate(new Vector2(m_vec_direction.x * y_velocity * Time.deltaTime, m_vec_direction.y * x_velocity * Time.deltaTime), Space.World);
        }
    }

    protected virtual void OnMouseLeftDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
    }

    protected virtual void OnMouseLeftDrag()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
    }

    protected virtual void OnMouseLeftUp()
    {

    }

    protected virtual void OnMouseRightDown()
    {

    }

    protected virtual void OnMouseRightDrag()
    {

    }

    protected virtual void OnMouseRightUp()
    {

    }
}
*/

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(WitchData), "gained_soul_num")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(PaladinData), "holy_state")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(WarriorData), "armor_inchant_power")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(ClericData), "heal_power")]
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
    public float attack_range;
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
    // 캐릭터 타게팅 상태
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
            m_vec_direction.Normalize();
            transform.Translate(new Vector2(m_vec_direction.x * ((HeroData)m_data).y_velocity * Time.deltaTime,
                m_vec_direction.y * ((HeroData)m_data).x_velocity * Time.deltaTime), Space.World);
        }
    }

    protected override void OnMouseLeftDown()
    {

    }
}