using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
class EnemyData : CreatureData
{
    #region Data from JSON file
    
    #endregion
}

public class Enemy : Creature
{
    protected override void OnAwake()
    {
        base.OnAwake();
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
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            m_current_health -= float.Parse(other.name);
        }
    }
}
