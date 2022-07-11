using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsComponent : PhysicsComponent
{
    public Vector2 m_move_velocity;

    public EnemyPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Enemy>();

        // m_move_velocity = new Vector2(((EnemyData)((Enemy)m_data).m_data).x_velocity, ((EnemyData)((Enemy)m_data).m_data).y_velocity);
    }

    public override void Update()
    {
        base.Update();

        m_rigidbody.velocity += ((Enemy)m_data).m_vec_direction.normalized * m_move_velocity;
    }
}
