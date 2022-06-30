using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
class GoblinData : EnemyData
{
    #region Data from JSON file

    #endregion
}

public class Goblin : Enemy
{
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
