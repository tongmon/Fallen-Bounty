using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // 물리 강체
    public Rigidbody2D m_rigidbody;

    // 추가적으로 가해질 속도
    public List<Vector2> m_extra_velocity;
    // 추가적으로 가해질 속도의 감소치
    public List<Vector2> m_extra_decrease;

    public PhysicsComponent(GameObject gameobject)
    {
        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();

        m_extra_velocity = new List<Vector2>();
        m_extra_decrease = new List<Vector2>();
    }

    public virtual void Update()
    {
        m_rigidbody.velocity = Vector2.zero;

        #region 넉백 속도 계산
        for (int i = 0; i < m_extra_velocity.Count; i++)
        {
            float extra_x_vel = m_extra_velocity[i].x > 0 ? m_extra_velocity[i].x - m_extra_decrease[i].x : 0;
            float extra_y_vel = m_extra_velocity[i].y > 0 ? m_extra_velocity[i].y - m_extra_decrease[i].y : 0;
            m_extra_velocity[i] = new Vector2(extra_x_vel, extra_y_vel);

            m_rigidbody.velocity += m_extra_velocity[i];

            if (extra_x_vel == 0 && extra_y_vel == 0) {
                m_extra_velocity.RemoveAt(i);
                m_extra_decrease.RemoveAt(i--);
            }
        }
        #endregion
    }

    public void AddVelocity(Vector2 power, Vector2 direction)
    {
        m_rigidbody.velocity += power * direction;
    }

    public void SetVelocity(Vector2 power, Vector2 direction)
    {
        m_rigidbody.velocity = power * direction;
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

    public Vector2 GetPosition()
    {
        return m_rigidbody.position;
    }
}
