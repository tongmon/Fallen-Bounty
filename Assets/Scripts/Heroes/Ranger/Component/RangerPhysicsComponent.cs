using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPhysicsComponent : HeroPhysicsComponent
{
    public RangerPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();
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
