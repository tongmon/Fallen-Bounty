using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public long m_attribute; // 상태이상, 64bit
    public float m_exsist_time; // 화살 존재 시간

    public override void Shoot()
    {
        m_direction = m_target.transform.position - m_shooter.transform.position;

        m_exsist_time = 0;

        m_velocity.x = 15.0f;
        m_velocity.y = 10.0f;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        m_type_name = GetType().Name;
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        m_exsist_time += Time.deltaTime;

        // 5초 이하로 존재하게
        if (m_exsist_time >= 5)
        {
            Destroy();
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    protected override void OnProjectileCollisionEnter(Collision2D collider)
    {
        // 적과 충돌하면 화살 제거
        if (collider.gameObject == m_target)
        {
            Destroy();
        }
    }
}
