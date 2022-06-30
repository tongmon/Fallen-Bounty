using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInputComponent : EnemyInputComponent
{
    public GoblinInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Goblin>();
    }
}
