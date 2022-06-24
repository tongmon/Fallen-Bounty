using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    // 물리 강체
    public Rigidbody2D m_rigidbody;

    // 생명체가 향하는 방향
    public Vector2 m_vec_direction;

    public PhysicsComponent(GameObject gameobject)
    {
        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
        m_vec_direction = new Vector2();
    }

    public virtual void Update()
    {

    }
}
