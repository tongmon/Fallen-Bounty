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
        OnUpdate();
    }

    void FixedUpdate()
    {
        OnFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnProjectileCollisionEnter(collision);
    }

    public virtual void OnAwake()
    {
        
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

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
