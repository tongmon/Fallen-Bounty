using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Field
{
    protected override void OnAwake()
    {
        base.OnAwake();

        m_graphics_component = new WaterGraphicsComponent(gameObject);
        m_physics_component = new WaterPhysicsComponent(gameObject);

        m_friction = 2000;
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnUpdate()
    {
        m_graphics_component.Update();
    }

    protected override void OnFixedUpdate()
    {
        m_physics_component.Update();
    }
}
