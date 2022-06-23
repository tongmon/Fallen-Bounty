using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputComponent : InputComponent
{
    public Vector2 m_dragging_point;

    public HeroInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_dragging_point = new Vector2();
    }
}
