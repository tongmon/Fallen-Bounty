using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerGraphicsComponent : HeroGraphicsComponent
{
    Ranger m_data;

    public RangerGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();

        m_line_renderer = m_data.GetComponent<LineRenderer>();

        m_sprite_seleted_circle = m_data.transform.Find("FocusCircle").GetComponent<SpriteRenderer>();

        m_dragline_fade_speed = 3.0f;
    }

    protected override void OnDrawLine()
    {
        if (m_dragline_alpha < 1.0f)
            m_dragline_alpha = Mathf.Max(0, m_dragline_alpha - m_dragline_fade_speed * Time.deltaTime);

        Color mat_color = m_line_renderer.material.color;
        m_line_renderer.material.color = new Color(mat_color.r, mat_color.g, mat_color.b, m_dragline_alpha);

        m_line_renderer.SetPosition(0, m_sprite_seleted_circle.transform.position);
        m_line_renderer.SetPosition(1, ((HeroInputComponent)m_data.m_input_component).m_dragging_point);
    }
}
