using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerInputComponent : InputComponent
{
    Ranger m_data;

    public Vector2 m_dragging_point;

    public RangerInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();
        m_dragging_point = new Vector2();
    }

    protected override void OnMouseLeftDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (m_mouse_hit.collider && m_mouse_hit.collider.gameObject == m_data.gameObject)
            m_data.m_selected = true;
    }

    // ±×·¡ÇÈ ÄÄÆ÷³ÍÆ®¶û Ä¿ÇÃ¸µ µÇ¾î ÀÖ´Âµ¥ ³öµÖµµ µÇÁö¸¸... ¸Õ°¡ ²¬²ô·¯¿ò
    protected override void OnMouseLeftDrag()
    {
        if (!Input.GetMouseButton(0))
            return;

        m_mouse_hold_time[0] += Time.deltaTime;

        if (m_data.m_selected)
        {
            if (!m_mouse_hit.collider || m_mouse_hit.collider.gameObject != m_data.gameObject)
            {
                ((RangerGraphicsComponent)m_data.m_graphics_component).m_dragline_alpha = 1.0f;

                if (m_mouse_hit.collider && m_mouse_hit.collider.tag == "Enemy")
                {
                    m_dragging_point = ((RangerGraphicsComponent)m_data.m_graphics_component).m_sprite_seleted_circle.transform.position;
                }
                else
                {
                    m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
                ((RangerGraphicsComponent)m_data.m_graphics_component).m_dragline_alpha = 0.0f;
        }
    }

    protected override void OnMouseLeftUp()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        if (((RangerGraphicsComponent)m_data.m_graphics_component).m_dragline_alpha == 1.0f)
        {
            ((RangerGraphicsComponent)m_data.m_graphics_component).m_dragline_alpha = 0.99f;
        }
    }

    protected override void OnMouseRightDown()
    {

    }

    protected override void OnMouseRightDrag()
    {

    }

    protected override void OnMouseRightUp()
    {

    }
}
