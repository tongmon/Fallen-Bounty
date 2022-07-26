using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGraphicsComponent : GraphicsComponent
{
    public float m_dragline_alpha;
    public float m_dragline_fade_speed;
    public LineRenderer m_line_renderer;

    public float m_seleted_sprite_alpha;
    public SpriteRenderer m_seleted_sprite;

    public SpriteMask m_sprite_mask;

    public HeroGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_seleted_sprite_alpha = m_dragline_alpha = 0.0f;

        m_line_renderer = ((Hero)m_data).GetComponent<LineRenderer>();

        m_seleted_sprite = ((Hero)m_data).transform.Find("FocusCircle").GetComponent<SpriteRenderer>();

        m_dragline_fade_speed = 3.0f;

        m_sprite_mask = ((Hero)m_data).GetComponent<SpriteMask>();
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

