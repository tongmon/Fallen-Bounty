using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent
{
    public object m_data;

    // 화면 크기
    Vector2 m_screen_border;

    #region 마우스 입력 정보
    // 모든 마우스 정보는 화면 크기보다 작은 위치에 찍혀야 유효함
    // 따라서 마우스 좌표가 유효한지는 화면 크기 내부에 있는지 여부로 따지면 됨.

    // 클릭 좌표 충돌 정보
    public RaycastHit2D m_mouse_hit;

    public RaycastHit2D m_mouse_hit_test;//테스트용
    // 마우스 홀딩 시간
    public float[] m_mouse_hold_time;

    // 마우스 클릭 다운하는 순간 좌표
    public Vector2 m_mouse_r_click_down;
    public Vector2 m_mouse_l_click_down;

    // 마우스 클릭 업하는 순간 좌표
    public Vector2 m_mouse_r_click_up;
    public Vector2 m_mouse_l_click_up;

    // 마우스 클릭 좌표 (다운, 업 좌표가 같고 업, 다운 사이 클릭 속도가 특정 수치 사이면 기록)
    public Vector2 m_r_point_clicked;
    public Vector2 m_l_point_clicked;

    // 마우스 클릭 사이 초 간격
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
