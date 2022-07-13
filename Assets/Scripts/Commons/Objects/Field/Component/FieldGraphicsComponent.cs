using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGraphicsComponent : GraphicsComponent
{
    public SpriteRenderer m_field_sprite;

    public FieldGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Field>();
        m_field_sprite = gameobject.GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

    }
}
