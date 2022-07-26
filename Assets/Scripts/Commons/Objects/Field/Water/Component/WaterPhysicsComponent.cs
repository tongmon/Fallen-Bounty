using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysicsComponent : FieldPhysicsComponent
{
    public WaterPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Water>();
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
    }

    public override void OnTriggerStay(Collider2D collision)
    {
        base.OnTriggerStay(collision);

        Vector2 vec_center = m_collider.bounds.center;

        foreach (Creature creature in m_collisions)
        {
            creature.m_graphics_component.OnWalkInPool((Water)m_data);
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);
    }
}
