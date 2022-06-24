using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {

    }

    public override void Update()
    {
        base.Update();
    }

    void AddForce(Vector2 power, Vector2 direction)
    {

    }
}
