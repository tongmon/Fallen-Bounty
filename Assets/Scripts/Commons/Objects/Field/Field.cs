using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    GraphicsComponent m_graphics_component;
    
    float m_friction; // ������ ũ��

    protected void Awake()
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnFieldCollisionEnter(collision);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        OnFieldCollisionStay(collision);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        OnFieldCollisionExit(collision);
    }

    protected virtual void OnAwake()
    {
        m_graphics_component = new FieldGraphicsComponent(gameObject);


        // ������ �׽�Ʈ
        m_friction = 10;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnFixedUpdate()
    {

    }

    protected virtual void OnFieldCollisionEnter(Collision2D collision)
    {
        Creature creature = collision.gameObject.GetComponent<Creature>();
        if (creature)
        {
            // sorting layer ������ ���� �������� �־��ش�.
            creature.m_physics_component.m_affected_frictions.Add(((FieldGraphicsComponent)m_graphics_component).m_field_sprite.sortingOrder, new Vector2(m_friction, m_friction));
        }
    }

    protected virtual void OnFieldCollisionStay(Collision2D collision)
    {

    }

    protected virtual void OnFieldCollisionExit(Collision2D collision)
    {

    }
}
