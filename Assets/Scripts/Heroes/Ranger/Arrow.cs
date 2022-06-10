using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public long m_attribute; // ȭ�� �Ӽ�, 64bit
    public float m_exsist_time; // ȭ�� ���� �ð�

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

        // ȭ�� ���� �ð��� 5�ʰ� �Ѿ�� ����
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
        // Ÿ�ٰ� �浹 �ߴٸ� ����
        if(collider.gameObject == m_target)
        {
            Destroy();
        }
    }
}
