using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 투사체 기본 클래스
public class Projectile : MonoBehaviour
{
    public string m_type_name;
    public GameObject m_shooter;
    public GameObject m_target;

    public BoxCollider2D m_collider;

    public Vector2 m_direction;
    public Vector2 m_velocity;

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

    protected virtual void OnAwake()
    {
        m_collider = GetComponent<BoxCollider2D>();
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
        transform.Translate(m_direction.normalized * m_velocity * Time.deltaTime);
    }

    // 발사
    public virtual void Shoot()
    {

    }

    // 투사체 삭제
    public virtual void Destroy()
    {
        ProjectilePool.ReturnObject(m_type_name, this);
    }
}
