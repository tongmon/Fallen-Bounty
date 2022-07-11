using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPhysicsComponent : PhysicsComponent
{
    public Vector2 m_move_velocity;

    public float m_angle;

    public ArrowPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Arrow>();

        m_angle = 0;

        m_move_velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        m_velocity += m_move_velocity;
    }

    public void SetSpeed(Vector2 speed, Vector2 dir)
    {
        m_move_velocity = speed * dir.normalized;

        m_angle = Vector2.Angle(Vector2.zero, dir.normalized);
        m_rigidbody.SetRotation(m_angle);
    }
}
