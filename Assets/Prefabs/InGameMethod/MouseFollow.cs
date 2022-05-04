using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // 길을 선택
    private bool m_path_select;

    // 도착함
    private bool m_path_arrived;

    private Vector2 m_between_vec;

    // 자기 자신에서 타겟 까지의 벡터
    private Vector2 m_obj_to_target_vec;

    // 목표 지점 좌표
    private Vector2 m_target_point;

    // 자기 자신을 선택했는지 검사하기 위한 자신의 객체
    private GameObject m_focus_object;

    // 선택된 적
    private GameObject m_focus_enemy;

    // 왼쪽 마우스
    private RaycastHit2D m_hit_left_mouse;

    // 오른쪽 마우스
    private RaycastHit2D m_hit_right_mouse;

    // 수평 속도 (좌우)
    private float m_horizontal_speed;

    // 수직 속도 (상하)
    private float m_vertical_speed;

    // 공격 사거리
    private float m_attack_range;

    // 캐릭터가 적보다 너무 위에 있는 경우 캐릭터를 내리기 위한 각도
    private float m_restrict_angle = 40.0f;

    private float m_restrict_obj_interval_angle = 20.0f;

    // 줄 긋는 객체
    LineRenderer m_lr;

    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;

        m_restrict_angle = 45.0f;
        m_attack_range = 2f;
        m_vertical_speed = 1.5f;
        m_horizontal_speed = 2f;

        // 줄 초기 설정
        m_lr = GetComponent<LineRenderer>();
        m_lr.startWidth = 0.05f;
        m_lr.endWidth = 0.05f;
    }
    void Update()
    {
        // 마우스 좌 클릭
        if (Input.GetMouseButtonDown(0))
        {
            // 선택한 영웅을 바꾸는 경우 기존에 선택된 영웅의 밑 원을 제거함
            if (m_focus_object != null)
                m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); //색 없애기
            
            // 클릭한 곳 월드 좌표로 따옴
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_left_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // 자기자신 선택하면
            if (m_hit_left_mouse.collider != null 
                && m_hit_left_mouse.collider.gameObject == gameObject)
            {
                m_path_select = false;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        // 마우스 우 클릭
        else if (Input.GetMouseButtonDown(1) 
            && m_focus_object != null 
            && m_hit_left_mouse.collider.gameObject == gameObject) //마우스 우 클릭
        {
            m_path_arrived = false;

            // 우측 클릭 좌표 획득
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_right_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // 포커스된 애가 내가 맞으면
            if (m_focus_object == gameObject) 
                m_path_select = true;

            // 적 선택시
            if (m_hit_right_mouse.collider != null 
                && m_hit_right_mouse.transform.tag != "Character")
            {
                m_focus_enemy = m_hit_right_mouse.collider.gameObject;
            }
            // 땅 선택시
            else 
            {
                m_target_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }

        // 길이 선택된 경우
        if (m_path_select)
        {
            if (m_hit_right_mouse.collider != null)
                m_target_point = m_hit_right_mouse.transform.position;

            // 각도에 따른 좌우 구분을 하기 위해 각도 검사 재검토 필요
            if (m_target_point.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else 
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // 가는 방향 설정
            m_obj_to_target_vec = new Vector2(0, 0);

            float distance_to_target = Vector2.Distance(m_focus_object.transform.position, m_target_point);

            // 길이 정해졌는데 적이 선택된 경우
            if (m_hit_right_mouse.collider != null
                && m_hit_right_mouse.collider.gameObject.transform.tag != "Character")
            {
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - m_attack_range) > 0.05f)
                {
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= m_attack_range)
                        m_obj_to_target_vec = (Vector2)(m_hit_right_mouse.transform.position - m_focus_object.transform.position);
                    // 적이 사거리보다 가까운 경우
                    else if (distance_to_target < m_attack_range)
                        m_obj_to_target_vec = (Vector2)(m_focus_object.transform.position - m_hit_right_mouse.transform.position);
                }
                // 적의 위치가 사거리와 맞지만 각도 조정이 필요
                else if (!m_path_arrived)
                {
                    Vector2 enemy_to_obj_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

                    float angle = Mathf.Atan2(enemy_to_obj_vec.y, enemy_to_obj_vec.x) * Mathf.Rad2Deg;

                    if (m_focus_enemy.transform.position.y < m_focus_object.transform.position.y)
                        angle -= 90;
                    else
                        angle += 90;

                    if (m_restrict_angle <= Mathf.Abs(angle))
                        m_path_arrived = true;

                    if (m_obj_to_target_vec == new Vector2(0, 0))
                    {
                        Vector2 move_vec = Quaternion.Euler(0, 0, angle) * enemy_to_obj_vec;
                        move_vec -= enemy_to_obj_vec;
                        move_vec.Normalize();
                        m_obj_to_target_vec = move_vec;
                    }
                }
            }
            // 길이 정해졌는데 땅이 선택된 경우
            else if(distance_to_target > 0.05f)
                m_obj_to_target_vec = m_target_point - (Vector2)m_focus_object.transform.position;

            m_obj_to_target_vec.Normalize();
            m_focus_object.transform.Translate(new Vector2(m_obj_to_target_vec.x * m_horizontal_speed * Time.deltaTime, m_obj_to_target_vec.y * m_vertical_speed * Time.deltaTime), Space.World);

            // 라인 삽입
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit_right_mouse.collider != null && m_focus_enemy != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            }
            else
            {
                m_lr.SetPosition(1, m_target_point);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag != "Character" || m_focus_enemy == null || !m_path_arrived)
            return;

        // 도착을 했는데 충돌하면 부딫힌 히어로와 자기 자신 사이의 각도를 잰다.
        Vector2 enemy_to_other_hero_vec = collision.transform.position - m_focus_enemy.transform.position;

        float interval_angle = Quaternion.FromToRotation(enemy_to_other_hero_vec, -m_obj_to_target_vec).eulerAngles.z;

        interval_angle = Mathf.Min(interval_angle, 360 - interval_angle);

        // 두 영웅의 사잇각이 크면 조절할 필요가 없기에 return
        if (m_restrict_obj_interval_angle < interval_angle)
            return;

        float angle_from_datum = Mathf.Atan2(enemy_to_other_hero_vec.y, enemy_to_other_hero_vec.x) * Mathf.Rad2Deg;

        if (m_focus_enemy.transform.position.y < collision.transform.position.y)
            angle_from_datum -= 90;
        else
            angle_from_datum += 90;

        if (m_restrict_angle <= Mathf.Abs(angle_from_datum))
        {
            // 각도가 맞음
        }
        else
        {
            // 각도가 작음
        }


        // 1사분면
        if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x > m_focus_enemy.transform.position.x)
        {

        }
        // 2사분면
        else if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {

        }
        // 3사분면
        else if (collision.transform.position.y < m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {

        }
        // 4사분면
        else
        {

        }

        //Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_hero_vec;
        
        /*
        Vector2 between_move_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;
        if (m_focus_object.transform.position.y > collision.gameObject.transform.position.y)
        {
            m_between_vec = Quaternion.Euler(0, 0, m_object_between_angle * 0.5f) * between_move_vec;
        }
        else
        {
            m_between_vec = Quaternion.Euler(0, 0, -m_object_between_angle * 0.5f) * between_move_vec;
        }
        m_between_vec -= between_move_vec;
        m_between_vec.Normalize();
        m_focus_object.transform.Translate(new Vector2(m_between_vec.x * m_horizontal_speed * Time.deltaTime, m_between_vec.y * m_vertical_speed * Time.deltaTime), Space.World);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Character" || m_focus_enemy == null || m_path_arrived)
            return;

        
        // 뒤에 오는애가 알아서 피하기.
    }
}