using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsComponent : PhysicsComponent
{
    public Collider2D m_body_collider;

    public Vector2 m_move_velocity;

    public EnemyPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Enemy>();

        // m_move_velocity = new Vector2(((EnemyData)((Enemy)m_data).m_data).x_velocity, ((EnemyData)((Enemy)m_data).m_data).y_velocity);

        m_body_collider = gameobject.GetComponent<Collider2D>();
    }

    public override void Update()
    {
        base.Update();

        m_rigidbody.velocity += ((Enemy)m_data).m_vec_direction.normalized * m_move_velocity;
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
