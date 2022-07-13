using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // ���� ��ü
    public Rigidbody2D m_rigidbody;

    // �浹ü
    public Collider2D m_collider;

    // �ӵ�
    public Vector2 m_velocity { get { return m_rigidbody.velocity; } set { m_rigidbody.velocity = value; } }

    // ����
    public float m_mass { get { return m_rigidbody.mass; } set { m_rigidbody.mass = value; } }

    // ��ġ
    public Vector2 m_position { get { return m_rigidbody.position; } set { m_rigidbody.position = value; } }

    public Vector2 m_size { get { return m_collider.bounds.size; } }

    public Vector2 m_bottom { get { return m_position - new Vector2(0, m_size.y / 2); } }

    public Vector2 m_top { get { return m_position + new Vector2(0, m_size.y / 2); } }

    public Vector2 m_right { get { return m_position + new Vector2(m_size.x / 2, 0); } }

    public Vector2 m_left { get { return m_position - new Vector2(m_size.x / 2, 0); } }

    // ����� ���ӵ�, ���ӵ��� �ο��� ������ �ӵ�, �ð�(1�� �̻��� �Ǹ� ����Ʈ���� ����)
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
        // ������ ó��
        Vector2 friction = Vector2.zero; // ������ ũ��(������ ���ԵǸ� �ȵ�), �̷��� �ϸ� �ȵǰ� �ܺο��� ���;� ��
        friction *= -m_velocity.normalized; // �������� �������� �ӵ��� �ݴ� �������� ����
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
        Vector2 accel = force / m_mass; // ���ӵ� ȹ��
        m_velocity += Time.deltaTime * accel;
    }
}
