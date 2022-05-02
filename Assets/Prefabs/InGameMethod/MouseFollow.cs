using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private bool m_path_select;
    private bool m_path_arrived;
    private Vector2 m_vec;
    private GameObject m_focus_object;
    private GameObject m_focus_enemy;
    private RaycastHit2D m_hit1, m_hit2;
    private float m_horizontal_speed = 2f;
    private float m_vertical_speed = 1.5f;
    private float m_attack_range = 2f;

    LineRenderer m_lr;
    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;
        m_lr = GetComponent<LineRenderer>();
        m_lr.startWidth = 0.05f;
        m_lr.endWidth = 0.05f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 좌 클릭
        {
            if (m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); //색 없애기
            Vector2 m_pos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit1 = Physics2D.Raycast(m_pos1, Vector2.zero, 0f);
            if (m_hit1.collider != null && m_hit1.collider.gameObject == gameObject && m_hit1.collider.transform.tag != "Enemy") //자기자신 선택하면
            {
                m_path_select = false;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        else if (Input.GetMouseButtonDown(1) && m_focus_object != null && m_hit1.collider.gameObject == gameObject) //마우스 우 클릭
        {
            m_path_arrived = false;
            if (m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Vector2 m_pos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit2 = Physics2D.Raycast(m_pos2, Vector2.zero, 0f);
            if (m_focus_object == gameObject) m_path_select = true;  //포커스된 애가 내가 맞으면
            if (m_hit2.collider != null && m_hit2.transform.tag != "Character")
            {
                m_focus_enemy = m_hit2.collider.gameObject;
                m_hit2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else //땅 클릭
            {
                m_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }
        if (m_path_select)
        {
            if (m_hit2.collider != null) m_vec = m_hit2.transform.position;
            if (m_vec.x < 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0); //추후 다시 수정
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector2 dir = new Vector2(m_vec.x - m_focus_object.transform.position.x, m_vec.y - m_focus_object.transform.position.y);
            dir.Normalize();
            if (m_hit2.collider != null && m_hit2.collider.gameObject.transform.tag != "Character")
            {
                if (Vector2.Distance(m_focus_object.transform.position, m_vec) > m_attack_range)
                {
                    m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);
                    m_path_arrived = false;
                }
                else m_path_arrived = true; //도착함
            }
            else m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);
            if(m_path_arrived && m_focus_enemy != null) //도착 했음
            {
                Vector2 temp_vec = new Vector2(0,0);
                if (m_focus_object.transform.position.y - m_focus_enemy.transform.position.y > 0.8f) //너무 높이 있음
                {
                    if(m_focus_object.transform.position.x > m_focus_enemy.transform.position.x) temp_vec = new Vector2(-m_focus_enemy.transform.position.x, 0.8f);
                    else temp_vec = new Vector2(m_focus_enemy.transform.position.x, 0.8f);
                }
                else if (m_focus_enemy.transform.position.y - m_focus_object.transform.position.y > 0.8f)
                {
                    if (m_focus_object.transform.position.x > m_focus_enemy.transform.position.x) temp_vec = new Vector2(-m_focus_enemy.transform.position.x, -0.8f);
                    else temp_vec = new Vector2(m_focus_enemy.transform.position.x, -0.8f);
                }
                temp_vec.Normalize();
                m_focus_object.transform.Translate(new Vector2(temp_vec.x * m_horizontal_speed * Time.deltaTime * 1.5f, temp_vec.y * m_vertical_speed * Time.deltaTime), Space.World);
            }
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit2.collider != null && m_focus_enemy != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            }
            else
            {
                m_lr.SetPosition(1, m_vec);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Character" && m_path_arrived)
        {
            if (m_focus_object.transform.position.y > collision.gameObject.transform.position.y) Invoke("PathMoveUp", 0.2f);
            else if (m_focus_object.transform.position.y <= collision.gameObject.transform.position.y) Invoke("PathMoveDown", 0.2f);
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
}
//바라보는 방향 각도계산
/*Vector2 m_direction = new Vector2(m_focus_object.transform.position.x - m_focus_enemy.transform.position.x, m_focus_object.transform.position.y - m_focus_enemy.transform.position.y);
float m_angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
Quaternion m_angleAxis = Quaternion.AngleAxis(m_angle - 90f, Vector3.forward);
Quaternion m_rotation = Quaternion.Slerp(m_focus_object.transform.rotation, m_angleAxis, 10f);*/
