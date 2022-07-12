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

        // 마찰력 처리
        Vector2 friction = Vector2.zero; // 마찰력 크기(방향이 포함되면 안됨), 이렇게 하면 안되고 외부에서 얻어와야 됨
        friction *= -((Hero)m_data).m_vec_direction.normalized; // 마찰력은 반대 방향으로 적용
        Vector2 accel = friction / m_mass;
        m_velocity += accel * Time.deltaTime;

        // 영웅 이동 속도 더해주기
        m_velocity += ((Hero)m_data).m_vec_direction.normalized * m_move_velocity;
    }
}
