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

    // 적용될 가속도, 가속도가 부여된 시점의 속도, 시간(1초 이상이 되면 리스트에서 제거)
    public List<(Vector2, Vector2, float)> m_accels;

    public PhysicsComponent(GameObject gameobject)
    {
        m_data = gameobject;

        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
        m_collider = gameobject.GetComponent<Collider2D>();
    }

    public virtual void Update()
    {
        /*
        // 마찰력 처리
        Vector2 friction = Vector2.zero; // 마찰력 크기(방향이 포함되면 안됨), 이렇게 하면 안되고 외부에서 얻어와야 됨
        friction *= -m_velocity.normalized; // 마찰력은 가해지는 속도의 반대 방향으로 적용
        Vector2 accel = friction / m_mass;
        Vector2 friction_velocity = m_velocity + accel * Time.deltaTime;
        if (m_velocity.x * friction_velocity.x < 0 && m_velocity.y * friction_velocity.y < 0)
            m_velocity = Vector2.zero;
        else
            m_velocity = friction_velocity;
        */

        m_velocity = Vector2.zero;
    }

    public void AddForce(Vector2 force)
    {
        Vector2 accel = force / m_mass; // 가속도 획득
        m_velocity += Time.deltaTime * accel;
    }
}
