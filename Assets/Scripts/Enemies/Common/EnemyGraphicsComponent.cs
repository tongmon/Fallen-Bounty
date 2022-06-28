using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphicsComponent : GraphicsComponent
{
    public float m_seleted_sprite_alpha;
    public SpriteRenderer m_seleted_sprite;

    public EnemyGraphicsComponent(GameObject gameobject) : base(gameobject)
    {

    }
}
