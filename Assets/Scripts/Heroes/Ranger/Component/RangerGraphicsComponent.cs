using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerGraphicsComponent : HeroGraphicsComponent
{
    public RangerGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();
    }

    // 드래깅 선 그리기
    protected override void OnDrawDragLine()
    {
        var data = (Ranger)m_data;

        if (m_dragline_alpha < 1.0f)
            m_dragline_alpha = Mathf.Max(0, m_dragline_alpha - m_dragline_fade_speed * Time.deltaTime);

        Color mat_color = m_line_renderer.material.color;
        m_line_renderer.material.color = new Color(mat_color.r, mat_color.g, mat_color.b, m_dragline_alpha);

        m_line_renderer.SetPosition(0, m_seleted_sprite.transform.position);
        m_line_renderer.SetPosition(1, ((HeroInputComponent)data.m_input_component).m_dragging_point);
    }

    // 선택시 밑에 보이는 스프라이트 그리기
    protected override void OnDrawSelectedSprite()
    {
        var data = (Ranger)m_data;

        m_seleted_sprite.color = new Color(255, 255, 255, m_seleted_sprite_alpha);
    }
}
