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
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {

    }

    protected override void OnFixedUpdate()
    {

    }
}
