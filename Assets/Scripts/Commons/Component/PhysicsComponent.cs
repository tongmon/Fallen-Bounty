using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // 물리 강체
    public Rigidbody2D m_rigidbody;

    public PhysicsComponent(GameObject gameobject)
    {
        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        m_rigidbody.velocity = new Vector2();
    }

    public void AddVelocity(Vector2 power, Vector2 direction)
    {
        m_rigidbody.velocity += power * direction;
    }

    public void SetVelocity(Vector2 power, Vector2 direction)
    {
        m_rigidbody.velocity = power * direction;
    }
}
