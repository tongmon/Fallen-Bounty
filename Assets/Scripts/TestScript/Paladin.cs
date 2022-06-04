using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
public class PaladinData : HeroData
{
    #region Data from JSON file
    public int holy_state;
    #endregion
}

public class Paladin : Hero
{
    protected override void OnAwake()
    {
        base.OnAwake();

        List<HeroData> hero_list = JsonParser.LoadJsonArrayToBaseList<HeroData>(Application.dataPath + "/DataFiles/ObjectFiles/hero_list");

        for (int i = 0; i < hero_list.Count; i++)
        {
            if (hero_list[i].type_name == "PaladinData")
            {
                m_data = hero_list[i];
                break;
            }
        }
    }

    protected override void OnStart()
    {

    }

    protected override void OnUpdate()
    {
        DrawLine();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    protected override void OnMouseLeftDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        var mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        if (!mouse.collider)
            return;

        if (mouse.collider.gameObject != gameObject)
            return;

        m_selected = true;
    }

    protected override void OnMouseLeftDrag()
    {
        if (!Input.GetMouseButton(0))
            return;

        var mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        if (m_selected)
        {
            if (!mouse.collider || mouse.collider.gameObject != gameObject)
            {
                m_dragline_alpha = 1.0f;

                if (mouse.collider && mouse.collider.tag == "Enemy")
                {
                    m_dragging_point = mouse.collider.transform.Find("FocusCircle").transform.position;
                }
                else
                {
                    m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
                m_dragline_alpha = 0f;
        }

        m_mouse_hold_time[0] += Time.deltaTime;
    }

    protected override void OnMouseLeftUp()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        var mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        if (m_dragline_alpha == 1.0f)
        {
            m_dragline_alpha = 0.99f;

            if (mouse.collider && mouse.collider.tag == "Enemy")
            {
                m_dragging_point = mouse.collider.transform.Find("FocusCircle").transform.position;
            }
            else
            {
                m_dragging_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        m_selected = false;
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

    // 선그리기, 모든 영웅은 선을 평소에 그리고 있지만 alpha 값이 0이라 보이진 않는다.
    // 플레이어가 드래그하기로 선택하면 그때 선의 alpha값이 1로 변해 플레이어에게 보이게 됨.
    protected void DrawLine()
    {
        m_line_renderer = GetComponent<LineRenderer>();

        if (m_dragline_alpha < 1.0f)
            m_dragline_alpha = Mathf.Max(0, m_dragline_alpha - 3f * Time.deltaTime);

        Color mat_color = m_line_renderer.material.color;
        m_line_renderer.material.color = new Color(mat_color.r, mat_color.g, mat_color.b, m_dragline_alpha);

        m_line_renderer.SetPosition(0, m_sprite_seleted_circle.transform.position);
        m_line_renderer.SetPosition(1, m_dragging_point);
    }
}