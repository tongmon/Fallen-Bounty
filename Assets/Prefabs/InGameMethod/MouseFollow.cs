using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // ���� ����
    private bool m_path_select;

    // ������
    private bool m_path_arrived;

    //������ Ŭ�� �̵��� ���� �ѹ��� ����
    private bool m_vec_select = false;

    //���� �̵� ���� Ȯ��
    private bool m_vec_move_done = false;

    // ���� ����
    private Vector2 m_vec;

    private Vector2 m_target_vec = new Vector2(0,0);

    // �ڱ� �ڽ��� �����ߴ��� �˻��ϱ� ���� �ڽ��� ��ü
    private GameObject m_focus_object;

    // ���õ� ��
    private GameObject m_focus_enemy;

    // ���� ���콺
    private RaycastHit2D m_hit_left_mouse;

    // ������ ���콺
    private RaycastHit2D m_hit_right_mouse;

    // ���� �ӵ� (�¿�)
    private float m_horizontal_speed = 2f;

    // ���� �ӵ� (����)
    private float m_vertical_speed = 1.5f;

    // ���� ��Ÿ�
    private float m_attack_range = 2f;

    private float m_restrict_angle = 30.0f;

    // �� �ߴ� ��ü
    LineRenderer m_lr;

    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;

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
            m_vec_select = false;
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
                m_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }

        // ���� ���õ� ���
        if (m_path_select)
        {
            if (m_hit_right_mouse.collider != null) 
                m_vec = m_hit_right_mouse.transform.position;

            // ������ ���� �¿� ������ �ϱ� ���� ���� �˻� ����� �ʿ�
            if (m_vec.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else 
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // ���� ���� ����
            Vector2 dir = new Vector2(m_vec.x - m_focus_object.transform.position.x, m_vec.y - m_focus_object.transform.position.y);
            dir.Normalize();

            // ���� �������µ� ���� ���õ� ���
            if (m_hit_right_mouse.collider != null 
                && m_hit_right_mouse.collider.gameObject.transform.tag != "Character"
                )
            {
                // ���� ��Ÿ� �ȿ� ���� ���
                if (Vector2.Distance(m_focus_object.transform.position, m_vec) >= m_attack_range)
                {
                    m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);
                }
                /*
                // ���� ��Ÿ����� ����� ���
                else if(Vector2.Distance(m_focus_object.transform.position, m_vec) < m_attack_range)
                {

                }
                */
                else
                {
                    m_path_arrived = true;
                    if (!m_vec_move_done)
                    {
                        Vector2 temp_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

                        float angle = Mathf.Atan2(temp_vec.y, temp_vec.x) * Mathf.Rad2Deg;

                        if (m_focus_enemy.transform.position.y < m_focus_object.transform.position.y)
                            angle = 90 - angle;
                        else
                            angle = angle - 270;

                        if (angle <= m_restrict_angle) 
                            m_vec_move_done = true;
                    
                        Debug.Log(angle);

                        if (!m_vec_select) //�̵����ʹ� �ѹ��� ����
                        {
                            /*
                            if (90 - m_restrict_angle <= angle && angle < 90) 
                                angle = -m_restrict_angle; //���� ����� �ߵǳ� �ޱ����� ��������
                            else if (90 <= angle && angle < 90 + m_restrict_angle) 
                                angle = m_restrict_angle;
                            else if (270 - m_restrict_angle <= angle && angle < 270) 
                                angle = -m_restrict_angle;
                            else if (270 <= angle && angle < 270 + m_restrict_angle) 
                                angle = m_restrict_angle;
                            */

                            m_target_vec = Quaternion.Euler(0, 0, angle) * temp_vec;
                            m_target_vec -= temp_vec;
                            m_vec_select = true;
                        }
                        m_focus_object.transform.Translate(new Vector2(m_target_vec.x * m_horizontal_speed * Time.deltaTime, m_target_vec.y * m_vertical_speed * Time.deltaTime), Space.World);
                    }
                }
            }
            // ���� �������µ� ���� ���õ� ���
            else 
                m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);

            // ���� ����
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit_right_mouse.collider != null && m_focus_enemy != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            }
            else
            {
                m_lr.SetPosition(1, m_vec);
            }
        }
    }

    #region ���� ���� ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character" && m_path_arrived)
        {
            if (m_focus_object.transform.position.y > collision.gameObject.transform.position.y) 
                Invoke("PathMoveUp", 0.2f);
            else if (m_focus_object.transform.position.y <= collision.gameObject.transform.position.y) 
                Invoke("PathMoveDown", 0.2f);
        }
    }
    void PathMoveUp()
    {
        m_focus_object.transform.Translate(new Vector2(0, m_vertical_speed * Time.deltaTime), Space.World);
    }
    void PathMoveDown()
    {
        m_focus_object.transform.Translate(new Vector2(0, -m_vertical_speed * Time.deltaTime), Space.World);
    }
    #endregion
}

//�ٶ󺸴� ���� �������
/*Vector2 m_direction = new Vector2(m_focus_object.transform.position.x - m_focus_enemy.transform.position.x, m_focus_object.transform.position.y - m_focus_enemy.transform.position.y);
float m_angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
Quaternion m_angleAxis = Quaternion.AngleAxis(m_angle - 90f, Vector3.forward);
Quaternion m_rotation = Quaternion.Slerp(m_focus_object.transform.rotation, m_angleAxis, 10f);*/
