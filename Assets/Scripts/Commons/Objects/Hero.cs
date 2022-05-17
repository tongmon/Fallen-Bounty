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

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // 영웅 좌우 방향 바꾸기
        if (m_point_target.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
