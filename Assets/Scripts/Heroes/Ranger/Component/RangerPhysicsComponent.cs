using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerPhysicsComponent : HeroPhysicsComponent
{
    public RangerPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();
    }
}
