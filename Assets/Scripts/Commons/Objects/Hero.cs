using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Creature
{
    public float m_attack_power;
    public float m_magic_power;
    public float m_attack_speed;
    
    // 공격 범위
    public float m_attack_range;

    // 타겟 위치
    public Vector2 m_point_target;

    // 움직임 상태
    public HeroCommandManager.eMoveState m_state_move;

    public GameObject m_target_enemy;

    public LineRenderer m_line_renderer;

    protected new void Awake()
    {
        base.Awake();

        m_line_renderer = GetComponent<LineRenderer>();
        m_state_move = HeroCommandManager.eMoveState.STATE_MOVE_NONE;
        m_target_enemy = null;
        m_x_velocity = 1f;
        m_y_velocity = 1.5f;
        m_attack_range = 3f;
        m_point_target = transform.position;
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

        /*
        #region 선그리기
        if (m_state_move != HeroCommandManager.eMoveState.STATE_MOVE_NONE)
        {
            m_line_renderer.SetPosition(0, transform.Find("FocusCircle").transform.position);
            m_line_renderer.SetPosition(1, m_point_target);
        }
        #endregion
        */
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        if (m_state_move != HeroCommandManager.eMoveState.STATE_MOVE_NONE)
        {
            m_vec_direction.Normalize();
            transform.Translate(new Vector2(m_vec_direction.x * m_y_velocity * Time.deltaTime, m_vec_direction.y * m_x_velocity * Time.deltaTime), Space.World);
        }
    }
}
