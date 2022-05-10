using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MouseFollow : MonoBehaviour
{
    public enum eMoveState 
    {
        STATE_MOVE_STRAIGHT,
        STATE_MOVE_ROTATION,
        STATE_MOVE_NONE,
    }

    // �̵� ����
    public eMoveState m_move_state;

    // ��ǥ ���� ��ǥ
    public Vector2 m_target_point;

    // �̵� ����
    public Vector2 m_vec_move_dir;

    // �ڱ� �ڽ��� �����ߴ��� �˻��ϱ� ���� �ڽ��� ��ü
    public GameObject m_focus_object;

    // ���õ� ��
    public GameObject m_focus_enemy;

    // ���� ���콺
    public RaycastHit2D m_hit_left_mouse;

    // ������ ���콺
    public RaycastHit2D m_hit_right_mouse;

    // ���� �ӵ� (�¿�)
    public float m_horizontal_speed;

    // ���� �ӵ� (����)
    public float m_vertical_speed;

    // ���� ��Ÿ�
    public float m_attack_range;

    // ĳ���Ͱ� ������ �ʹ� ���� �ִ� ��� ĳ���͸� ������ ���� ����
    public float m_restrict_angle = 40.0f;

    public float m_restrict_obj_interval_angle = 20.0f;

    // �� �ߴ� ��ü
    LineRenderer m_lr;

    void Start()
    {
        m_move_state = eMoveState.STATE_MOVE_NONE;

        m_focus_object = null;
        m_focus_enemy = null;

        m_restrict_angle = 45.0f;
        m_attack_range = 2f;
        m_vertical_speed = 1.5f;
        m_horizontal_speed = 2f;

        // �� �ʱ� ����
        m_lr = GetComponent<LineRenderer>();
        m_lr.startWidth = 0.05f;
        m_lr.endWidth = 0.05f;
    }
    void Update()
    {
        // ���콺 �� Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            // ������ ������ �ٲٴ� ��� ������ ���õ� ������ �� ���� ������
            if (m_focus_object != null)
                m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); //�� ���ֱ�

            // Ŭ���� �� ���� ��ǥ�� ����
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_left_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // �ڱ��ڽ� �����ϸ�
            if (m_hit_left_mouse.collider != null
                && m_hit_left_mouse.collider.gameObject == gameObject)
            {
                m_move_state = eMoveState.STATE_MOVE_NONE;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        // ���콺 �� Ŭ��
        else if (Input.GetMouseButtonDown(1)
            && m_focus_object != null
            && m_hit_left_mouse.collider.gameObject == gameObject) //���콺 �� Ŭ��
        {
            // ���� Ŭ�� ��ǥ ȹ��
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_right_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // ��Ŀ���� �ְ� ���� ������ m_path_select ���θ� ����
            if (m_focus_object == gameObject)
                m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
            else
                m_move_state = eMoveState.STATE_MOVE_NONE;

            // �� ���ý�
            if (m_hit_right_mouse.collider != null
                && m_hit_right_mouse.transform.tag != "Character")
            {
                m_focus_enemy = m_hit_right_mouse.collider.gameObject;
            }
            // �� ���ý�
            else
            {
                m_target_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }

        // ���� ���õǾ��µ� ������ ���� ���
        if (m_move_state != eMoveState.STATE_MOVE_NONE)
        {
            if (m_hit_right_mouse.collider != null)
                m_target_point = m_hit_right_mouse.transform.position;

            float distance_to_target = Vector2.Distance(m_focus_object.transform.position, m_target_point);

            // ������ ���� �¿� ������ �ϱ� ���� ���� �˻� ����� �ʿ�
            if (m_target_point.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // ���� �������µ� ���� ���õ� ���
            if (m_hit_right_mouse.collider != null 
                && m_hit_right_mouse.collider.gameObject.transform.tag == "Enemy")
            {
                /*
                // ���� ��ġ�� �ٰŸ� ������ ���� ������ ���� ����
                if (m_move_state == eMoveState.STATE_MOVE_ROTATION)
                {

                }
                */
                // ���� ��ġ�� ��Ÿ��� ���� ����
                if (Mathf.Abs(distance_to_target - m_attack_range) > 0.05f)
                {
                    m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    // ���� ��Ÿ� �ȿ� ���� ���
                    if (distance_to_target >= m_attack_range)
                        m_vec_move_dir = m_target_point - (Vector2)m_focus_object.transform.position;
                    // ���� ��Ÿ����� ����� ���
                    else
                        m_vec_move_dir = (Vector2)m_focus_object.transform.position - m_target_point;
                }
                else
                    m_move_state = eMoveState.STATE_MOVE_NONE;
            }
            // ���� �������µ� ���� ���õ� ���
            else
            {
                if (distance_to_target > 0.05f)
                {
                    m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    m_vec_move_dir = m_target_point - (Vector2)m_focus_object.transform.position;
                }
                else
                    m_move_state = eMoveState.STATE_MOVE_NONE;
            }

            // ���� �� �׸���
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit_right_mouse.collider != null && m_focus_enemy != null)
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            else
                m_lr.SetPosition(1, m_target_point);
        }

        // ���� ���⿡ ���� �̵�
        if (m_move_state != eMoveState.STATE_MOVE_NONE)
        {
            m_vec_move_dir.Normalize();
            m_focus_object.transform.Translate(new Vector2(m_vec_move_dir.x * m_horizontal_speed * Time.deltaTime, m_vec_move_dir.y * m_vertical_speed * Time.deltaTime), Space.World);
        }
    }

    // private string m_other_hero_name;

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag != "Character" || m_focus_enemy == null || m_other_hero_name.Length != 0)
            return;

        m_other_hero_name = collision.name;

        // ������ �ߴµ� �浹�ϸ� �΋H�� ����ο� �ڱ� �ڽ� ������ ������ ���.
        Vector2 enemy_to_other_hero_vec = collision.transform.position - m_focus_enemy.transform.position, rotated_min_vec, rotated_max_vec,
                enemy_to_cur_hero_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

        float interval_angle = Quaternion.FromToRotation(enemy_to_other_hero_vec, enemy_to_cur_hero_vec).eulerAngles.z;

        // �� ������ ���հ��� ũ�� ������ �ʿ䰡 ���⿡ return
        if (m_restrict_obj_interval_angle < Mathf.Min(interval_angle, 360 - interval_angle))
        {
            m_other_hero_name = "";
            return;
        }

        // �簢�� ȸ����Ű�� ���� ���� �����ϴ� ������ �� �����̰� ��
        if (interval_angle < 360 - interval_angle)
        {
            rotated_min_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }
        // ������ ȸ����Ű�� ���� ���� �����ϴ� ������ �� �����̰� ��
        else
        {
            rotated_min_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }

        float angle_with_y_axis;
        // 1��и�
        if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x > m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, Vector2.up).eulerAngles.z;
        }
        // 2��и�
        else if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(Vector2.up, rotated_min_vec).eulerAngles.z;
        }
        // 3��и�
        else if (collision.transform.position.y < m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {      
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, -Vector2.up).eulerAngles.z;
        }
        // 4��и�
        else
        {
            angle_with_y_axis = Quaternion.FromToRotation(-Vector2.up, rotated_min_vec).eulerAngles.z;
        }

        m_vec_move_dir = -enemy_to_cur_hero_vec + (angle_with_y_axis < m_restrict_angle ? rotated_max_vec : rotated_min_vec);
    }
    */

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Character" || m_focus_enemy == null || m_other_hero_name.Length != 0)
            return;

        m_other_hero_name = collision.name;

        // ������ �ߴµ� �浹�ϸ� �΋H�� ����ο� �ڱ� �ڽ� ������ ������ ���.
        Vector2 enemy_to_other_hero_vec = collision.transform.position - m_focus_enemy.transform.position, rotated_min_vec, rotated_max_vec,
                enemy_to_cur_hero_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

        float interval_angle = Quaternion.FromToRotation(enemy_to_other_hero_vec, enemy_to_cur_hero_vec).eulerAngles.z;

        // �� ������ ���հ��� ũ�� ������ �ʿ䰡 ���⿡ return
        if (m_restrict_obj_interval_angle < Mathf.Min(interval_angle, 360 - interval_angle))
        {
            m_other_hero_name = "";
            return;
        }

        // �簢�� ȸ����Ű�� ���� ���� �����ϴ� ������ �� �����̰� ��
        if (interval_angle < 360 - interval_angle)
        {
            rotated_min_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }
        // ������ ȸ����Ű�� ���� ���� �����ϴ� ������ �� �����̰� ��
        else
        {
            rotated_min_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }

        float angle_with_y_axis;
        // 1��и�
        if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x > m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, Vector2.up).eulerAngles.z;
        }
        // 2��и�
        else if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(Vector2.up, rotated_min_vec).eulerAngles.z;
        }
        // 3��и�
        else if (collision.transform.position.y < m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, -Vector2.up).eulerAngles.z;
        }
        // 4��и�
        else
        {
            angle_with_y_axis = Quaternion.FromToRotation(-Vector2.up, rotated_min_vec).eulerAngles.z;
        }

        m_vec_move_dir = -enemy_to_cur_hero_vec + (angle_with_y_axis < m_restrict_angle ? rotated_max_vec : rotated_min_vec);
    }
    */
}