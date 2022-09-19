using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPhysicsComponent : HeroPhysicsComponent
{
    public BerserkerPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Berserker>();

        m_mass = gameobject.GetComponent<Berserker>().berserker_data.mass;

        m_move_velocity = gameobject.GetComponent<Berserker>().berserker_data.velocity;
    }

    public override void OnCollisionEnter(Collision2D collision)
    {

    }

    public override void OnCollisionStay(Collision2D collision)
    {

    }

    public override void OnCollisionExit(Collision2D collision)
    {

    }
}
