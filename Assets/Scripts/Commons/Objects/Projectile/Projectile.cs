using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 투사체 기본 클래스
public class Projectile : MonoBehaviour
{
    public string m_type_name;
    public Creature m_shooter;
    public Creature m_target;

    public PhysicsComponent m_physics_component;
    public GraphicsComponent m_graphics_component;

    public Vector2 m_direction;

    public bool m_shooted;

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        if (m_shooted)
            OnUpdate();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_shooted)
            OnProjectileCollisionEnter(collision);
    }

    public virtual void OnAwake()
    {
        m_shooted = false;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    // 총알이 쏴진 후에 충돌 검사한다. (m_shooted 얘가 true인 경우만 발동됨)
    protected virtual void OnProjectileCollisionEnter(Collision2D collider)
    {

    }

    protected virtual void OnFixedUpdate()
    {    

    }

    // 투사체 삭제
    public virtual void Destroy()
    {
        ProjectilePool.ReturnObject(m_type_name, this);
    }
}
