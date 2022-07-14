using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // 물리 강체
    public Rigidbody2D m_rigidbody;

    // 충돌체
    public Collider2D m_collider;

    // 속도
    public Vector2 m_velocity { get { return m_rigidbody.velocity; } set { m_rigidbody.velocity = value; } }

    // 질량
    public float m_mass { get { return m_rigidbody.mass; } set { m_rigidbody.mass = value; } }

    // 위치
    public Vector2 m_position { get { return m_rigidbody.position; } set { m_rigidbody.position = value; } }

    public Vector2 m_size { get { return m_collider.bounds.size; } }

    public Vector2 m_bottom { get { return m_position - new Vector2(0, m_size.y / 2); } }

    public Vector2 m_top { get { return m_position + new Vector2(0, m_size.y / 2); } }

    public Vector2 m_right { get { return m_position + new Vector2(m_size.x / 2, 0); } }

    public Vector2 m_left { get { return m_position - new Vector2(m_size.x / 2, 0); } }

    // 영향 받는 마찰력들... (sorting layer가 높은 녀석의 마찰력이 우선적으로 적용됨)
    public SortedList m_affected_frictions;

    public Vector2 m_affected_friction { get { return (Vector2)m_affected_frictions.GetByIndex(0); } }

    public PhysicsComponent(GameObject gameobject)
    {
        m_data = gameobject;

        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
        m_collider = gameobject.GetComponent<Collider2D>();
        m_affected_frictions = new SortedList();
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public void AddForce(Vector2 force)
    {
        Vector2 accel = force / m_mass; // 가속도 획득
        m_velocity += Time.deltaTime * accel;
    }
}
