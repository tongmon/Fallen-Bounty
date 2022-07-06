using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    // public long m_attribute; // 상태이상, 64bit
    public float m_exsist_time; // 화살 존재 시간

    public void SetPosition(Vector2 position)
    {
        m_physics_component.m_rigidbody.MovePosition(position);
    }

    public void Shoot(Vector2 speed)
    {
        m_direction = m_target.m_physics_component.GetPosition() - m_shooter.m_physics_component.GetPosition();

        m_exsist_time = 0;

        ((ArrowPhysicsComponent)m_physics_component).SetSpeed(speed, m_direction);
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void OnAwake()
    {
        base.OnAwake();

        m_type_name = GetType().Name;

        m_physics_component = new ArrowPhysicsComponent(gameObject);
        m_graphics_component = new ArrowGraphicComponent(gameObject);
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

        m_physics_component.Update();
        m_graphics_component.Update();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    protected override void OnProjectileCollisionEnter(Collision2D collider)
    {
        // 적과 충돌하면 화살 제거
        if (collider.gameObject == m_target.gameObject)
        {
            Destroy();
        }
    }
}
