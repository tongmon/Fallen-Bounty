using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // 물리 강체
    public Rigidbody2D m_rigidbody;

    // 추가적인 넉백 속도
    public Vector2 m_knockback_velocity;
    // 넉백 속도 감소 수치
    public Vector2 m_knockback_decrease;

    public PhysicsComponent(GameObject gameobject)
    {
        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();

        m_knockback_velocity = new Vector2();

        m_knockback_decrease = new Vector2(2.0f, 2.0f);
    }

    public virtual void Update()
    {
        m_rigidbody.velocity = Vector2.zero;

        #region 넉백 속도 계산
        m_knockback_velocity.x = m_knockback_velocity.x > 0 ? m_knockback_velocity.x - m_knockback_decrease.x : 0;
        m_knockback_velocity.y = m_knockback_velocity.y > 0 ? m_knockback_velocity.y - m_knockback_decrease.y : 0;
        m_rigidbody.velocity += m_knockback_velocity;
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

    public void AddKnockBack(Vector2 power, Vector2 direction)
    {
        m_knockback_velocity += power * direction;
    }

    public void SetKnockBack(Vector2 power, Vector2 direction)
    {
        m_knockback_velocity = power * direction;
    }

    public Vector2 GetPosition()
    {
        return m_rigidbody.position;
    }
}
