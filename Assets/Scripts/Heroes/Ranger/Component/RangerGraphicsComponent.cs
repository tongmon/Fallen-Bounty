using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerGraphicsComponent : GraphicsComponent
{
    Ranger m_data;

    public float m_dragline_alpha;
    public LineRenderer m_line_renderer;

    public SpriteRenderer m_sprite_seleted_circle;

    public RangerGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();

        m_line_renderer = m_data.GetComponent<LineRenderer>();
        m_dragline_alpha = 0.0f;

        m_sprite_seleted_circle = m_data.transform.Find("FocusCircle").GetComponent<SpriteRenderer>();
    }
}
