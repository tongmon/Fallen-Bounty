using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInputComponent : EnemyInputComponent
{
    public GoblinInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Goblin>();
    }

    protected override void OnMouseLeftDown()
    {
        base.OnMouseLeftDown();
    }

    protected override void OnMouseLeftDrag()
    {
        base.OnMouseLeftDrag();
    }

    protected override void OnMouseLeftUp()
    {
        base.OnMouseLeftUp();

        var data = (Goblin)m_data;

        if (data.m_selected)
        {
            if (m_mouse_hit.collider)
            {
                if(m_mouse_hit.collider.gameObject == data.gameObject)
                {
                    // 선택한 적 다시 선택
                    if (((EnemyGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha > 0)
                    {
                        ((EnemyGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 0;
                    }
                    else
                    {
                        ((EnemyGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 255;
                    }
                }
            }
        }
        else
            ((EnemyGraphicsComponent)data.m_graphics_component).m_seleted_sprite_alpha = 0;
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
