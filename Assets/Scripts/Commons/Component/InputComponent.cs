using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent
{
    public object m_data;

    // ȭ�� ũ��
    Vector2 m_screen_border;

    #region ���콺 �Է� ����
    // ��� ���콺 ������ ȭ�� ũ�⺸�� ���� ��ġ�� ������ ��ȿ��
    // ���� ���콺 ��ǥ�� ��ȿ������ ȭ�� ũ�� ���ο� �ִ��� ���η� ������ ��.

    // Ŭ�� ��ǥ �浹 ����
    public RaycastHit2D m_mouse_hit;

    public RaycastHit2D m_mouse_hit_test;//�׽�Ʈ��
    // ���콺 Ȧ�� �ð�
    public float[] m_mouse_hold_time;

    // ���콺 Ŭ�� �ٿ��ϴ� ���� ��ǥ
    public Vector2 m_mouse_r_click_down;
    public Vector2 m_mouse_l_click_down;

    // ���콺 Ŭ�� ���ϴ� ���� ��ǥ
    public Vector2 m_mouse_r_click_up;
    public Vector2 m_mouse_l_click_up;

    // ���콺 Ŭ�� ��ǥ (�ٿ�, �� ��ǥ�� ���� ��, �ٿ� ���� Ŭ�� �ӵ��� Ư�� ��ġ ���̸� ���)
    public Vector2 m_r_point_clicked;
    public Vector2 m_l_point_clicked;

    // ���콺 Ŭ�� ���� �� ����
    float m_time_betw_click;
    #endregion

    public InputComponent(GameObject gameobject)
    {
        m_screen_border = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        m_mouse_hold_time = new float[2] { 0, 0 };
        m_time_betw_click = 0.8f;
        m_l_point_clicked = m_r_point_clicked = m_mouse_l_click_up = m_mouse_r_click_up = m_mouse_l_click_down = m_mouse_r_click_down = m_screen_border;
    }

    public virtual void Update()
    {
        m_mouse_hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        m_mouse_hit_test = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << 6);
        m_l_point_clicked = m_r_point_clicked = m_mouse_l_click_up = m_mouse_r_click_up = m_mouse_l_click_down = m_mouse_r_click_down = m_screen_border;
        
        OnMouseEvent();
    }

    protected void OnMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseLeftDown();

        if (Input.GetMouseButton(0))
            OnMouseLeftDrag();

        if (Input.GetMouseButtonUp(0))
            OnMouseLeftUp();

        if (Input.GetMouseButtonDown(1))
            OnMouseRightDown();

        if (Input.GetMouseButton(1))
            OnMouseRightDrag();

        if (Input.GetMouseButtonUp(1))
            OnMouseRightUp();
    }

    protected virtual void OnMouseLeftDown()
    {
        m_mouse_l_click_down = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_mouse_hold_time[0] = 0;
    }

    protected virtual void OnMouseLeftDrag()
    {
        m_mouse_hold_time[0] += Time.deltaTime;
    }

    protected virtual void OnMouseLeftUp()
    {
        m_mouse_l_click_up = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_mouse_hold_time[0] < m_time_betw_click)
            m_l_point_clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void OnMouseRightDown()
    {
        m_mouse_r_click_down = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_mouse_hold_time[1] = 0;
    }

    protected virtual void OnMouseRightDrag()
    {
        m_mouse_hold_time[1] += Time.deltaTime;
    }

    protected virtual void OnMouseRightUp()
    {
        m_mouse_r_click_up = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_mouse_hold_time[1] < m_time_betw_click)
            m_r_point_clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
