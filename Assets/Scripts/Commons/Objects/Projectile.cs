using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü �⺻ Ŭ����
public class Projectile : MonoBehaviour
{
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

    protected virtual void OnFixedUpdate()
    {
        transform.Translate(m_direction.normalized * m_velocity * Time.deltaTime);
    }

    // �߻�
    public virtual void Shoot()
    {

    }

    // ����ü ����
    public virtual void Destroy()
    {
        ProjectilePool.ReturnObject(this);
    }

    public virtual void OnCollisionEnter(Collision collider)
    {

    }
}
