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

    // ±×·¡ÇÈ ÄÄÆ÷³ÍÆ®¶û Ä¿ÇÃ¸µ µÇ¾î ÀÖ´Âµ¥ ³öµÖµµ µÇÁö¸¸... ¸Õ°¡ ²¬²ô·¯¿ò -- Ä¿ÇÃÀÌ¶ó¼­ ²¬²ô·¯¿öÇÏ´Â°Å¾ß? ±×·³ ¾ÈµÅ
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
                    m_dragging_point = ((EnemyGraphicsComponent)m_mouse_hit.collider.GetComponent<Enemy>().m_graphics_component).m_seleted_sprite.transform.position;
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

        if (data.m_selected)
        {
            // µå·¡±ë ¶óÀÎ °ü·Ã º¯¼ö Á¶Á¤
            if (((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha == 1.0f)
            {
                ((RangerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 0.99f;

                if (m_mouse_hit.collider && m_mouse_hit.collider.tag == "Enemy")
                {
                    m_dragging_point = ((EnemyGraphicsComponent)m_mouse_hit.collider.GetComponent<Enemy>().m_graphics_component).m_seleted_sprite.transform.position;
                }
                else
                {
                    m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }

            if (m_mouse_hit.collider)
            {
                // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ Àû
                if (m_mouse_hit.collider.gameObject.tag == "Enemy")
                {
                    data.m_target = m_mouse_hit.collider.gameObject.GetComponent<Enemy>();
                    data.m_movement_state = new RangerRunStateComponent(data.gameObject);
                    ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
                }
                // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ ¿µ¿õ
                else if (m_mouse_hit.collider.gameObject.tag == "Hero")
                {
                    if (m_mouse_hit.collider.gameObject == data.gameObject)
                    {
                        // ¼±ÅÃÇÑ ¿µ¿õ ´Ù½Ã ¼±ÅÃ
                        if (((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha > 0)
                        {
                            ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 0;
                        }
                        else
                        {
                            ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
                        }
                    }
                    else
                    {
                        //data.m_target = m_mouse_hit.collider.gameObject.GetComponent<Hero>();
                        data.m_point_target = m_mouse_l_click_up;
                        ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
                    }
                }
            }
            // ¸¶¿ì½º¸¦ ¶¾ À§Ä¡°¡ ¶¥¶¥¶¥ »§~
            else
            {
                //data.m_target = null;
                data.m_point_target = m_mouse_l_click_up;
                data.m_movement_state = new RangerRunStateComponent(data.gameObject);
                ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
            }
        }
        else
            ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 0;
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
