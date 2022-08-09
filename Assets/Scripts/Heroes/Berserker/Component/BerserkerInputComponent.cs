using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerInputComponent : HeroInputComponent
{
    public BerserkerInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Berserker>();
    }
    protected override void OnMouseLeftDown()
    {
        base.OnMouseLeftDown();
    }
    
    protected override void OnMouseLeftDrag()
    {
        base.OnMouseLeftDrag();

        var data = (Berserker)m_data;

        if (data.m_selected)
        {
            if (!m_mouse_hit.collider || m_mouse_hit.collider.gameObject != data.gameObject)
            {
                ((BerserkerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 1.0f;

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
                ((BerserkerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 0.0f;
        }
    }
    protected override void OnMouseLeftUp()
    {
        base.OnMouseLeftUp();

        var data = (Berserker)m_data;

        if (data.m_selected)
        {
            // 드래깅 라인 관련 변수 조정
            if (((BerserkerGraphicsComponent)data.m_graphics_component).m_dragline_alpha == 1.0f)
            {
                ((BerserkerGraphicsComponent)data.m_graphics_component).m_dragline_alpha = 0.99f;

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
                // 마우스를 뗀 위치가 적
                if (m_mouse_hit.collider.gameObject.tag == "Enemy")
                {
                    data.m_target = m_mouse_hit.collider.gameObject.GetComponent<Enemy>();
                    data.m_movement_state = new BerserkerRunStateComponent(data.gameObject);
                    ((HeroGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
                }
                // 마우스를 뗀 위치가 영웅
                else if (m_mouse_hit.collider.gameObject.tag == "Hero")
                {
                    if (m_mouse_hit.collider.gameObject == data.gameObject)
                    {
                        // 선택한 영웅 다시 선택
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
            
            else
            {
                //data.m_target = null;
                data.m_point_target = m_mouse_l_click_up;
                data.m_movement_state = new BerserkerRunStateComponent(data.gameObject);
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
