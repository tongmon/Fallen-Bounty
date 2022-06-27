using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    public object m_data;

    // ���� ��ü
    public Rigidbody2D m_rigidbody;

    // �߰����� �˹� �ӵ�
    public Vector2 m_knockback_velocity;
    // �˹� �ӵ� ���� ��ġ
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

        #region �˹� �ӵ� ���
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
