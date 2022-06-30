using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphicsComponent : GraphicsComponent
{
    public float m_seleted_sprite_alpha;
    public SpriteRenderer m_seleted_sprite;

    public EnemyGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Enemy>();

        m_seleted_sprite = ((Enemy)m_data).transform.Find("FocusCircle").GetComponent<SpriteRenderer>();

        m_seleted_sprite_alpha = 0.0f;
    }

    public override void Update()
    {
        base.Update();

        OnDrawSelectedSprite();
    }

    protected virtual void OnDrawSelectedSprite()
    {

    }
}
