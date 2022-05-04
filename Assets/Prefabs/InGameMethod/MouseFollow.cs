using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // ���� ����
    private bool m_path_select;

    // ������
    private bool m_path_arrived;

    //���� �̵� ���� Ȯ��
    private bool m_vec_move_done;

    private Vector2 m_between_vec;
    // ���� ����
    private Vector2 m_obj_to_target_vec;

    // �ڱ� �ڽ��� �����ߴ��� �˻��ϱ� ���� �ڽ��� ��ü
    private GameObject m_focus_object;

    // ���õ� ��
    private GameObject m_focus_enemy;

    // ���� ���콺
    private RaycastHit2D m_hit_left_mouse;

    // ������ ���콺
    private RaycastHit2D m_hit_right_mouse;

    // ���� �ӵ� (�¿�)
    private float m_horizontal_speed;

    // ���� �ӵ� (����)
    private float m_vertical_speed ;

    // ���� ��Ÿ�
    private float m_attack_range;

    // ĳ���Ͱ� ������ �ʹ� ���� �ִ� ��� ĳ���͸� ������ ���� ����
    private float m_restrict_angle = 40.0f;

    private float m_object_between_angle = 20.0f;

    // �� �ߴ� ��ü
    LineRenderer m_lr;

    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;

        m_restrict_angle = 45.0f;
        m_attack_range = 2f;
        m_vertical_speed = 1.5f;
        m_horizontal_speed = 2f;

        m_vec_move_done = false;

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
                m_path_select = false;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        // ���콺 �� Ŭ��
        else if (Input.GetMouseButtonDown(1) 
            && m_focus_object != null 
            && m_hit_left_mouse.collider.gameObject == gameObject) //���콺 �� Ŭ��
        {
            m_path_arrived = false;
            m_vec_move_done = false;

            // ���� Ŭ�� ��ǥ ȹ��
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_right_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // ��Ŀ���� �ְ� ���� ������
            if (m_focus_object == gameObject) 
                m_path_select = true;

            // �� ���ý�
            if (m_hit_right_mouse.collider != null 
                && m_hit_right_mouse.transform.tag != "Character")
            {
                m_focus_enemy = m_hit_right_mouse.collider.gameObject;
            }
            // �� ���ý�
            else 
            {
                m_obj_to_target_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }

        // ���� ���õ� ���
        if (m_path_select)
        {
            if (m_hit_right_mouse.collider != null)
                m_obj_to_target_vec = m_hit_right_mouse.transform.position;

            // ������ ���� �¿� ������ �ϱ� ���� ���� �˻� ����� �ʿ�
            if (m_obj_to_target_vec.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else 
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // ���� ���� ����
            Vector2 dir = new Vector2(0, 0);

            float distance_to_target = Vector2.Distance(m_focus_object.transform.position, m_obj_to_target_vec);

            // ���� �������µ� ���� ���õ� ���
            if (m_hit_right_mouse.collider != null
                && m_hit_right_mouse.collider.gameObject.transform.tag != "Character")
            {
                if (Mathf.Abs(distance_to_target - m_attack_range) > 0.05f)
                {
                    // ���� ��Ÿ� �ȿ� ���� ���
                    if (distance_to_target >= m_attack_range)
                        dir = (Vector2)(m_hit_right_mouse.transform.position - m_focus_object.transform.position);
                    // ���� ��Ÿ����� ����� ���
                    else if (distance_to_target < m_attack_range)
                        dir = (Vector2)(m_focus_object.transform.position - m_hit_right_mouse.transform.position);
                }
                else
                {
                    m_path_arrived = true;
                    if (!m_vec_move_done)
                    {
                        Vector2 enemy_to_obj_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

                        float angle = Mathf.Atan2(enemy_to_obj_vec.y, enemy_to_obj_vec.x) * Mathf.Rad2Deg;

                        if (m_focus_enemy.transform.position.y < m_focus_object.transform.position.y)
                            angle -= 90;
                        else
                            angle += 90;

                        if (m_restrict_angle <= Mathf.Abs(angle))
                            m_vec_move_done = true;

                        Vector2 move_vec = Quaternion.Euler(0, 0, angle) * enemy_to_obj_vec;
                        move_vec -= enemy_to_obj_vec;
                        move_vec.Normalize();
                        dir = move_vec;

                        // m_focus_object.transform.Translate(new Vector2(m_target_vec.x * m_horizontal_speed * Time.deltaTime, m_target_vec.y * m_vertical_speed * Time.deltaTime), Space.World);
                    }
                }
            }
            // ���� �������µ� ���� ���õ� ���
            else if(distance_to_target > 0.05f)
                dir = m_obj_to_target_vec - (Vector2)m_focus_object.transform.position;

            dir.Normalize();
            m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);

            // ���� ����
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit_right_mouse.collider != null && m_focus_enemy != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            }
            else
            {
                m_lr.SetPosition(1, m_obj_to_target_vec);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character" && m_path_arrived)
        {
            Vector2 between_move_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;
            if (m_focus_object.transform.position.y > collision.gameObject.transform.position.y)
            {
                m_between_vec = Quaternion.Euler(0, 0, m_object_between_angle) * between_move_vec;
            }
            else 
            {
                m_between_vec = Quaternion.Euler(0, 0, -m_object_between_angle) * between_move_vec;
            }
            m_between_vec -= between_move_vec;
            m_between_vec.Normalize();
            m_focus_object.transform.Translate(new Vector2(m_between_vec.x * m_horizontal_speed * Time.deltaTime, m_between_vec.y * m_vertical_speed * Time.deltaTime ), Space.World);
        }
    }
}