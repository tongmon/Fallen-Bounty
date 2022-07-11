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

    // ���� �ӵ�
    public float m_velocity_past;

    // ����
    public float m_mass { get { return m_rigidbody.mass; } set { m_rigidbody.mass = value; } }

    // ��ġ
    public Vector2 m_position { get { return m_rigidbody.position; } set { m_rigidbody.position = value; } }

    public Vector2 m_size { get { return m_collider.bounds.size; } }

    public Vector2 m_bottom { get { return m_position - new Vector2(0, m_size.y / 2); } }

    public Vector2 m_top { get { return m_position + new Vector2(0, m_size.y / 2); } }

    public Vector2 m_right { get { return m_position + new Vector2(m_size.x / 2, 0); } }

    public Vector2 m_left { get { return m_position - new Vector2(m_size.x / 2, 0); } }

    // �߰������� ������ �ӵ�
    public List<Vector2> m_extra_velocity;
    // �߰������� ������ �ӵ��� ����ġ
    public List<Vector2> m_extra_decrease;

    public PhysicsComponent(GameObject gameobject)
    {
        m_data = gameobject;

        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
        m_collider = gameobject.GetComponent<Collider2D>();

        m_extra_velocity = new List<Vector2>();
        m_extra_decrease = new List<Vector2>();
    }

    public virtual void Update()
    {
        m_velocity = Vector2.zero;

        #region �߰� �ӵ� ���
        for (int i = 0; i < m_extra_velocity.Count; i++)
        {
            m_velocity += m_extra_velocity[i];

            float extra_x_vel = m_extra_velocity[i].x > 0 ? m_extra_velocity[i].x - m_extra_decrease[i].x : 0;
            float extra_y_vel = m_extra_velocity[i].y > 0 ? m_extra_velocity[i].y - m_extra_decrease[i].y : 0;
            m_extra_velocity[i] = new Vector2(extra_x_vel, extra_y_vel);

            if (extra_x_vel == 0 && extra_y_vel == 0) {
                m_extra_velocity.RemoveAt(i);
                m_extra_decrease.RemoveAt(i--);
            }
        }
        #endregion
    }

    public void AddExtraVelocity(int power, Vector2 direction, Vector2 decrease)
    {
        m_extra_velocity.Add(power * direction.normalized);
        m_extra_decrease.Add(decrease);
    }

    public void SetExtraVelocity(int index, int power, Vector2 direction, Vector2 decrease)
    {
        if (m_extra_velocity.Count >= index)
            return;

        m_extra_velocity[index] = power * direction.normalized;
        m_extra_decrease[index] = decrease;
    }
}
