using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerInputComponent : HeroInputComponent
{
    public RangerInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();
    }

    protected override void OnMouseLeftDown()
    {
        base.OnMouseLeftDown();
    }

    // ±×·¡ÇÈ ÄÄÆ÷³ÍÆ®¶û Ä¿ÇÃ¸µ µÇ¾î ÀÖ´Âµ¥ ³öµÖµµ µÇÁö¸¸... ¸Õ°¡ ²¬²ô·¯¿ò
    protected override void OnMouseLeftDrag()
    {
        base.OnMouseLeftDrag();

        var data = (Ranger)m_data;

        if (data.m_selected)
        {
            if (!m_mouse_hit.collider || m_mouse_hit.collider.gameObject != data.gameObject)
            {
                ((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 1.0f;

                if (m_mouse_hit.collider && m_mouse_hit.collider.tag == "Enemy")
                {
                    m_dragging_point = ((EnemyGraphicsComponent)m_mouse_hit.collider.GetComponent<Enemy>().m_graphics_component).m_sprite_seleted_sprite.transform.position;
                }
                else
                {
                    m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
                ((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 0.0f;
        }
    }

    protected override void OnMouseLeftUp()
    {
        base.OnMouseLeftUp();

        var data = (Ranger)m_data;

        if (((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha == 1.0f && data.m_selected)
        {
            ((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 0.99f;

            if(m_mouse_hit.collider && m_mouse_hit.collider.tag == "Enemy")
            {
                m_dragging_point = ((EnemyGraphicsComponent)m_mouse_hit.collider.GetComponent<Enemy>().m_graphics_component).m_sprite_seleted_sprite.transform.position;
            }
            else
            {
                m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if(m_mouse_hit.collider && data.m_selected)
        {
            // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ Àû
            if (m_mouse_hit.collider.gameObject.tag == "Enemy")
            {
                data.m_target = m_mouse_hit.collider.gameObject.GetComponent<Enemy>();
                data.m_point_target = data.m_target.m_physics_component.GetPosition();
                data.m_movement_state = new HeroMoveStateComponent(data.gameObject);
            }
            // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ ¿µ¿õ
            else if (m_mouse_hit.collider.gameObject.tag == "Hero")
            {
                data.m_target = m_mouse_hit.collider.gameObject.GetComponent<Hero>();
                data.m_point_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                data.m_selected = false;
            }
        }
        // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ ¶¥
        else
        {
            data.m_selected = false;
        }
    }

    protected override void OnMouseRightDown()
    {
        base.OnMouseRightDown();
    }

    protected override void OnMouseRightDrag()
    {
        base.OnMouseRightDrag();
    }

    protected override void OnMouseRightUp()
    {
        base.OnMouseRightUp();
    }
}
