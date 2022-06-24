using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    // 추가적인 넉백 속도
    public Vector2 m_knockback_velocity;
    // 넉백 속도 감소 수치
    public Vector2 m_knockback_decrease;

    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_knockback_velocity = new Vector2();

        m_knockback_decrease = new Vector2(2.0f, 2.0f);
    }

    public override void Update()
    {
        base.Update();

        UpdateKnockBackVelocity();

        m_rigidbody.velocity += ((Hero)m_data).m_vec_direction.normalized * 
            new Vector2(((HeroData)((Hero)m_data).m_data).x_velocity, ((HeroData)((Hero)m_data).m_data).y_velocity);
    }

    public void AddKnockBack(Vector2 power, Vector2 direction)
    {
        m_knockback_velocity += power * direction;
    }

    public void SetKnockBack(Vector2 power, Vector2 direction)
    {
        m_knockback_velocity = power * direction;
    }

    void UpdateKnockBackVelocity()
    {
        object[] speeds = { m_knockback_velocity.x, m_knockback_velocity.y };
        object[] decrease = { m_knockback_decrease.x, m_knockback_decrease.y };

        for (int i = 0; i < 2; i++)
        {
            if ((int)speeds[i] <= 0)
                speeds[i] = 0;
            else
                speeds[i] = (int)speeds[i] - (int)decrease[i];
        }

        m_rigidbody.velocity += m_knockback_velocity;
    }
}
