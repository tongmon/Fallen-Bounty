using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerPhysicsComponent : HeroPhysicsComponent
{
    public BerserkerPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Berserker>();
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
