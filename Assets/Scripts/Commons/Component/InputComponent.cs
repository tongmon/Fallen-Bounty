using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent
{
    public RaycastHit2D m_mouse_hit;
    // 마우스 홀딩 시간
    public float[] m_mouse_hold_time;

    public InputComponent(GameObject gameobject)
    {
        m_mouse_hold_time = new float[2] { 0, 0 };
    }

    public virtual void Update()
    {
        m_mouse_hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        OnMouseEvent();
    }

    protected void OnMouseEvent()
    {
        OnMouseLeftDown();
        OnMouseLeftDrag();
        OnMouseLeftUp();
        OnMouseRightDown();
        OnMouseRightDrag();
        OnMouseRightUp();
    }

    protected virtual void OnMouseLeftDown()
    {

    }

    protected virtual void OnMouseLeftDrag()
    {

    }

    protected virtual void OnMouseLeftUp()
    {

    }

    protected virtual void OnMouseRightDown()
    {

    }

    protected virtual void OnMouseRightDrag()
    {

    }

    protected virtual void OnMouseRightUp()
    {

    }
}
