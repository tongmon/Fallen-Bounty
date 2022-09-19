using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goblin : Enemy
{
    [SerializeField] GoblinData goblin_data;
    protected override void OnAwake()
    {
        base.OnAwake();

        m_input_component = new GoblinInputComponent(gameObject);
        m_graphics_component = new GoblinGraphicsComponent(gameObject);
        m_physics_component = new GoblinPhysicsComponent(gameObject);

        // m_movement_state = new HeroIdleStateComponent(gameObject);
    }

    protected override void OnStart()
    {
        m_current_health = goblin_data.health;
    }

    protected override void OnUpdate()
    {
        m_input_component.Update();
        m_physics_component.Update();
        m_graphics_component.Update();
    }

    protected override void OnFixedUpdate()
    {

    }
}
