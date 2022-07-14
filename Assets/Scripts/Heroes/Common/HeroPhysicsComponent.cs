using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    public Vector2 m_move_velocity;

    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_mass = ((HeroData)((Hero)m_data).m_data).mass;

        m_move_velocity = ((HeroData)((Hero)m_data).m_data).velocity;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        #region 영웅 이동 속도 설정(이동 속도만...)
        // 영웅이 가야 하는 방향으로 현재 속도를 주면 어느 정도 속도가 이동 속도에 가해지는 지가 나옴 (벡터 중 이동 하려는 방향으로의 크기를 추출한다고 보면 됨.)
        Vector2 applied_movement = m_velocity * ((Hero)m_data).m_vec_direction.normalized;
        float x = m_velocity.x, y = m_velocity.y;

        if (Mathf.Abs(applied_movement.x) < Mathf.Abs(((Hero)m_data).m_vec_direction.x * m_move_velocity.x))
            x = ((Hero)m_data).m_vec_direction.normalized.x * m_move_velocity.x;

        if(Mathf.Abs(applied_movement.y) < Mathf.Abs(((Hero)m_data).m_vec_direction.y * m_move_velocity.y))
            y = ((Hero)m_data).m_vec_direction.normalized.y * m_move_velocity.y;

        m_velocity = new Vector2(x, y);
        #endregion

        #region 마찰력 처리(모든 이동속도가 처리된 후에 마지막에 처리 해야 함)
        Vector2 friction = new Vector2(200, 200); // m_affected_friction; // 마찰력 크기(방향이 포함되면 안됨), 이렇게 하면 안되고 외부에서 얻어와야 됨
        friction *= -m_velocity.normalized; // 마찰력은 가해지는 속도의 반대 방향으로 적용
        Vector2 accel = friction / m_mass;
        Vector2 friction_velocity = m_velocity + accel * Time.deltaTime;

        if (m_velocity.x * friction_velocity.x <= 0 && m_velocity.y * friction_velocity.y <= 0)
            m_velocity = Vector2.zero;
        else
            m_velocity = friction_velocity;
        #endregion
    }
}
