using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JsonSubTypes;
using Newtonsoft.Json;


[JsonConverter(typeof(JsonSubtypes))]
public class BerserkerData : HeroData
{
    #region Data from JSON file
    //아직미정
    
    #endregion
}


public class Berserker : Hero
{
    protected override void OnAwake()
    {
        base.OnAwake();

        m_data = JsonParser.GetHero("Berserker");
        
        m_input_component = new BerserkerInputComponent(gameObject);
        m_physics_component = new BerserkerPhysicsComponent(gameObject);
        m_graphics_component = new BerserkerGraphicsComponent(gameObject);
        
        m_movement_state = new HeroIdleStateComponent(gameObject);

        m_attack_state = new BerserkerAutoAttackStateComponent(gameObject);
    }

    protected override void OnStart()
    {
        BerserkerData berserker_data = (BerserkerData)m_data;
        berserker_data.m_info = "공격력, 체력이 높다. 방어, 마법저항은 평균치.";
        DrainAbility a = new DrainAbility();
        abilities.Add(a);
    }

    protected override void OnUpdate()
    {
        // 컴포넌트간 통신을 변수를 직접 넘겨주는 방식으로 사용하기에 각 컴포넌트간의 Update() 순서가 중요하다.

        // 입력 처리
        m_input_component.Update();
        // 물리 처리
        m_physics_component.Update();
        // 그래픽 처리
        m_graphics_component.Update();

        // 이동 상태 처리
        m_movement_state.Update();
        // 공격 상태 처리
        m_attack_state.Update();

        /*
        // 상태 이상 처리
        m_buff_debuff_state.Update();
        */
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    /*
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
    protected void OnDrawLine()
    {
        m_line_renderer = GetComponent<LineRenderer>();

        if (m_dragline_alpha < 1.0f)
            m_dragline_alpha = Mathf.Max(0, m_dragline_alpha - 3f * Time.deltaTime);

        Color mat_color = m_line_renderer.material.color;
        m_line_renderer.material.color = new Color(mat_color.r, mat_color.g, mat_color.b, m_dragline_alpha);

        m_line_renderer.SetPosition(0, m_sprite_seleted_circle.transform.position);
        m_line_renderer.SetPosition(1, m_dragging_point);
    }
    */

    /*
    protected void OnAttack()
    {
        m_cur_attack_cooltime -= Time.deltaTime;

        if (!m_target_enemy)
            return;

        float distance = Vector2.Distance(m_target_enemy.transform.position, transform.position);

        // 공격 쿨타임이 되었고 사거리 내에 적이 있다면 투사체 발사
        if (m_cur_attack_cooltime < 0 && distance <= ((RangerData)m_data).ranged_range)
        {
            m_cur_attack_cooltime = ((RangerData)m_data).attack_cooltime;
            var arrow = ProjectilePool.GetObj(((RangerData)m_data).projectile_type);
            
            // 총알 나가는 시작점 결정, 추후 수정
            arrow.m_rigidbody.MovePosition(m_rigidbody.position);

            arrow.m_target = m_target_enemy;
            arrow.m_shooter = gameObject;

            arrow.Shoot();
        }
    }
    */
}
