using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    // public long m_attribute; // 상태이상, 64bit

    public void SetPosition(Vector2 position)
    {
        m_physics_component.m_rigidbody.position = position;
        //m_physics_component.m_rigidbody.MovePosition(position);
    }

    public void Shoot(Vector2 speed)
    {
        m_direction = m_target.m_physics_component.m_position - m_shooter.m_physics_component.m_position;

        ((ArrowPhysicsComponent)m_physics_component).SetSpeed(speed, m_direction);

        m_shooted = true;
    }

    // 쏘는 물체, 타겟 물체, 투사체 속도, 화살 발사 시작점
    public void Shoot(Creature shooter, Creature target, Vector2 speed, Vector2? start_point = null)
    {
        m_shooter = shooter;
        m_target = target;

        SetPosition(start_point == null ? m_shooter.m_physics_component.m_position : start_point.Value);

        m_direction = m_target.m_physics_component.m_position - (start_point == null ? m_shooter.m_physics_component.m_position : start_point.Value);

        ((ArrowPhysicsComponent)m_physics_component).SetSpeed(speed, m_direction);

        m_shooted = true;
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
