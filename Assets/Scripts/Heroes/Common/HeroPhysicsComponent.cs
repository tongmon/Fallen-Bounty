using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    CapsuleCollider2D m_body_collider;

    public Vector2 m_move_velocity;

    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_body_collider = gameobject.GetComponent<CapsuleCollider2D>();

        m_move_velocity = new Vector2(((HeroData)((Hero)m_data).m_data).x_velocity, ((HeroData)((Hero)m_data).m_data).y_velocity);
    }

    public override void Update()
    {
        base.Update();

        // 영웅 이동 속도 더해주기
        m_rigidbody.velocity += ((Hero)m_data).m_vec_direction.normalized * m_move_velocity;
    }

    public Vector2 GetSize()
    {
        return m_body_collider.bounds.size;
    }

    public Vector2 GetBottom()
    {
        return GetPosition() - new Vector2(0, GetSize().y / 2);
    }

    public Vector2 GetTop()
    {
        return GetPosition() + new Vector2(0, GetSize().y / 2);
    }

    public Vector2 GetRight()
    {
        return GetPosition() + new Vector2(GetSize().x / 2, 0);
    }

    public Vector2 GetLeft()
    {
        return GetPosition() - new Vector2(GetSize().x / 2, 0);
    }
}
