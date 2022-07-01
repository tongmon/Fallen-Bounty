using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

// �������� ������ ������ �ֱ������� �߰� �ش� ������ ���� ���� ������ �ش� ������������ �������� �����ٰ� ����.
[JsonConverter(typeof(JsonSubtypes))]
public class RangerData : HeroData
{
    #region Data from JSON file
    public int weakness_hit_cnt;
    public float weakness_popup_cooltime;
    public string projectile_type;
    public Vector2 arrow_speed; 
    #endregion
}

public class Ranger : Hero
{
    //private long m_arrow_attribute; // ȭ�� �Ӽ�, 64bit
    //private GameObject m_arrow;

    // �ü��� ���� Ÿ�����ϰ� �ִ��� ����, Ÿ���õ� ���� �״� ��쳪 �ü� �����ÿ��� false�� ��
    public bool m_target_on;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_target_on = false;
    }

    protected override void OnStart()
    {
        // ������Ʈ���� ������ Awake���� Start���� �ʱ�ȭ�ؾ� �Ȳ���
        // ������ m_physics_component �꿡 ����ִ� ����� �̵� �ӵ��� json�� �Ľ��ؼ� ������ ����
        m_input_component = new RangerInputComponent(gameObject);
        m_physics_component = new RangerPhysicsComponent(gameObject);
        m_graphics_component = new RangerGraphicsComponent(gameObject);

        m_movement_state = new HeroIdleStateComponent(gameObject);

        // �ʱ⿡ ȭ�� 5�� ����
        ProjectilePool.InitPool(((RangerData)m_data).projectile_type, 5);
    }

    protected override void OnUpdate()
    {
        // ������Ʈ�� ����� ������ ���� �Ѱ��ִ� ������� ����ϱ⿡ �� ������Ʈ���� Update() ������ �߿��ϴ�.

        // ������ �Է� ó��
        m_input_component.Update();
        // ������ ���� ó��
        m_physics_component.Update();
        // ������ �׷��� ó��
        m_graphics_component.Update();
        
        // �̵� ���� ó��
        m_movement_state.Update();

        /*
        // ���� ���� ó��
        m_attack_state.Update();
        // ���� �̻� ó��
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

    // ���׸���, ��� ������ ���� ��ҿ� �׸��� ������ alpha ���� 0�̶� ������ �ʴ´�.
    // �÷��̾ �巡���ϱ�� �����ϸ� �׶� ���� alpha���� 1�� ���� �÷��̾�� ���̰� ��.
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

        // ���� ��Ÿ���� �Ǿ��� ��Ÿ� ���� ���� �ִٸ� ����ü �߻�
        if (m_cur_attack_cooltime < 0 && distance <= ((RangerData)m_data).ranged_range)
        {
            m_cur_attack_cooltime = ((RangerData)m_data).attack_cooltime;
            var arrow = ProjectilePool.GetObj(((RangerData)m_data).projectile_type);
            
            // �Ѿ� ������ ������ ����, ���� ����
            arrow.m_rigidbody.MovePosition(m_rigidbody.position);

            arrow.m_target = m_target_enemy;
            arrow.m_shooter = gameObject;

            arrow.Shoot();
        }
    }
    */
}
