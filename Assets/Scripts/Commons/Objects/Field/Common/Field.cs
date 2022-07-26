using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GraphicsComponent m_graphics_component;
    public PhysicsComponent m_physics_component;

    // ¸¶Âû·Â Å©±â
    public float m_friction;

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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        OnFieldTriggerEnter(collision);
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        OnFieldTriggerStay(collision);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        OnFieldTriggerExit(collision);
    }

    protected virtual void OnAwake()
    {
        m_graphics_component = new FieldGraphicsComponent(gameObject);
        m_physics_component = new FieldPhysicsComponent(gameObject);

        m_friction = 200;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {
        m_graphics_component.Update();
    }

    protected virtual void OnFixedUpdate()
    {
        m_physics_component.FixedUpdate();
    }

    protected virtual void OnFieldTriggerEnter(Collider2D collider)
    {
        m_physics_component.OnTriggerEnter(collider);
    }

    protected virtual void OnFieldTriggerStay(Collider2D collider)
    {
        m_physics_component.OnTriggerStay(collider);        
    }

    protected virtual void OnFieldTriggerExit(Collider2D collider)
    {
        m_physics_component.OnTriggerExit(collider);
    }
}
