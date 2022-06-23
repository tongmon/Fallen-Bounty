using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGraphicsComponent : GraphicsComponent
{
    public float m_dragline_alpha;
    public float m_dragline_fade_speed;
    public LineRenderer m_line_renderer;

    public SpriteRenderer m_sprite_seleted_sprite;

    public HeroGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_dragline_alpha = 0.0f;
    }

    public override void Update()
    {
        base.Update();

        OnDrawDragLine();
        OnDrawSelectedSprite();
    }

    protected virtual void OnDrawDragLine()
    {

    }

    protected virtual void OnDrawSelectedSprite()
    {

    }
}
