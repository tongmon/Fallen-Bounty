using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public long m_attribute; // 화살 속성, 64bit
    public float m_exsist_time; // 화살 존재 시간

    public override void Shoot()
    {
        m_direction = m_target.transform.position - m_shooter.transform.position;
        m_exsist_time = 0;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        m_exsist_time += Time.deltaTime;

        // 화살 존재 시간이 5초가 넘어가면 삭제
        if (m_exsist_time >= 5)
        {
            Destroy();
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnCollisionEnter(Collision collider)
    {
        // 타겟과 충돌 했다면 삭제
        if(collider.gameObject == m_target)
        {
            Destroy();
        }
    }
}
