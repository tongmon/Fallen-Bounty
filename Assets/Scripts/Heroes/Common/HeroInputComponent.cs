using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputComponent : InputComponent
{
    public Vector2 m_dragging_point;

    public HeroInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_dragging_point = new Vector2();
    }

    protected override void OnMouseLeftDown()
    {
        base.OnMouseLeftDown();

        if (m_mouse_hit.collider && m_mouse_hit.collider.gameObject == ((Hero)m_data).gameObject)
            ((Hero)m_data).m_selected = true;
    }

    protected override void OnMouseLeftDrag()
    {
        base.OnMouseLeftDrag();
    }

    protected override void OnMouseLeftUp()
    {
        base.OnMouseLeftUp();
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
