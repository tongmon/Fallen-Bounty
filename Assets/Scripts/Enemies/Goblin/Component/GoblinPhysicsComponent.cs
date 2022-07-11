using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPhysicsComponent : EnemyPhysicsComponent
{
    public GoblinPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Goblin>();
    }
}
