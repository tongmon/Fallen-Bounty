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

    // 이동 상태
    public eMoveState m_move_state;

    // 목표 지점 좌표
    public Vector2 m_target_point;

    // 이동 방향
    public Vector2 m_vec_move_dir;

    // 자기 자신을 선택했는지 검사하기 위한 자신의 객체
    public GameObject m_focus_object;

    // 선택된 적
    public GameObject m_focus_enemy;

    // 왼쪽 마우스
    public RaycastHit2D m_hit_left_mouse;

    // 오른쪽 마우스
    public RaycastHit2D m_hit_right_mouse;

    // 수평 속도 (좌우)
    public float m_horizontal_speed;

    // 수직 속도 (상하)
    public float m_vertical_speed;

    // 공격 사거리
    public float m_attack_range;

    // 캐릭터가 적보다 너무 위에 있는 경우 캐릭터를 내리기 위한 각도
    public float m_restrict_angle = 40.0f;

    public float m_restrict_obj_interval_angle = 20.0f;

    // 줄 긋는 객체
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
                m_move_state = eMoveState.STATE_MOVE_NONE;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        // 마우스 우 클릭
        else if (Input.GetMouseButtonDown(1)
            && m_focus_object != null
            && m_hit_left_mouse.collider.gameObject == gameObject) //마우스 우 클릭
        {
            // 우측 클릭 좌표 획득
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_hit_right_mouse = Physics2D.Raycast(pos, Vector2.zero, 0f);

            // 포커스된 애가 내가 맞으면 m_path_select 여부를 반전
            if (m_focus_object == gameObject)
                m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
            else
                m_move_state = eMoveState.STATE_MOVE_NONE;

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

        // 길이 선택되었는데 도착을 안한 경우
        if (m_move_state != eMoveState.STATE_MOVE_NONE)
        {
            if (m_hit_right_mouse.collider != null)
                m_target_point = m_hit_right_mouse.transform.position;

            float distance_to_target = Vector2.Distance(m_focus_object.transform.position, m_target_point);

            // 각도에 따른 좌우 구분을 하기 위해 각도 검사 재검토 필요
            if (m_target_point.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // 길이 정해졌는데 적이 선택된 경우
            if (m_hit_right_mouse.collider != null 
                && m_hit_right_mouse.collider.gameObject.transform.tag == "Enemy")
            {
                /*
                // 적의 위치와 근거리 영웅의 공격 각도가 맞지 않음
                if (m_move_state == eMoveState.STATE_MOVE_ROTATION)
                {

                }
                */
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - m_attack_range) > 0.05f)
                {
                    m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= m_attack_range)
                        m_vec_move_dir = m_target_point - (Vector2)m_focus_object.transform.position;
                    // 적이 사거리보다 가까운 경우
                    else
                        m_vec_move_dir = (Vector2)m_focus_object.transform.position - m_target_point;
                }
                else
                    m_move_state = eMoveState.STATE_MOVE_NONE;
            }
            // 길이 정해졌는데 땅이 선택된 경우
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

            // 선택 선 그리기
            m_lr.SetPosition(0, m_focus_object.transform.GetChild(0).transform.position);
            if (m_hit_right_mouse.collider != null && m_focus_enemy != null)
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            else
                m_lr.SetPosition(1, m_target_point);
        }

        // 정한 방향에 따라 이동
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

        // 도착을 했는데 충돌하면 부딫힌 히어로와 자기 자신 사이의 각도를 잰다.
        Vector2 enemy_to_other_hero_vec = collision.transform.position - m_focus_enemy.transform.position, rotated_min_vec, rotated_max_vec,
                enemy_to_cur_hero_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

        float interval_angle = Quaternion.FromToRotation(enemy_to_other_hero_vec, enemy_to_cur_hero_vec).eulerAngles.z;

        // 두 영웅의 사잇각이 크면 조절할 필요가 없기에 return
        if (m_restrict_obj_interval_angle < Mathf.Min(interval_angle, 360 - interval_angle))
        {
            m_other_hero_name = "";
            return;
        }

        // 양각을 회전시키는 것이 현재 조종하는 영웅을 덜 움직이게 함
        if (interval_angle < 360 - interval_angle)
        {
            rotated_min_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }
        // 음각을 회전시키는 것이 현재 조종하는 영웅을 덜 움직이게 함
        else
        {
            rotated_min_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }

        float angle_with_y_axis;
        // 1사분면
        if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x > m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, Vector2.up).eulerAngles.z;
        }
        // 2사분면
        else if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(Vector2.up, rotated_min_vec).eulerAngles.z;
        }
        // 3사분면
        else if (collision.transform.position.y < m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {      
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, -Vector2.up).eulerAngles.z;
        }
        // 4사분면
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

        // 도착을 했는데 충돌하면 부딫힌 히어로와 자기 자신 사이의 각도를 잰다.
        Vector2 enemy_to_other_hero_vec = collision.transform.position - m_focus_enemy.transform.position, rotated_min_vec, rotated_max_vec,
                enemy_to_cur_hero_vec = m_focus_object.transform.position - m_focus_enemy.transform.position;

        float interval_angle = Quaternion.FromToRotation(enemy_to_other_hero_vec, enemy_to_cur_hero_vec).eulerAngles.z;

        // 두 영웅의 사잇각이 크면 조절할 필요가 없기에 return
        if (m_restrict_obj_interval_angle < Mathf.Min(interval_angle, 360 - interval_angle))
        {
            m_other_hero_name = "";
            return;
        }

        // 양각을 회전시키는 것이 현재 조종하는 영웅을 덜 움직이게 함
        if (interval_angle < 360 - interval_angle)
        {
            rotated_min_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }
        // 음각을 회전시키는 것이 현재 조종하는 영웅을 덜 움직이게 함
        else
        {
            rotated_min_vec = Quaternion.Euler(0, 0, -m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
            rotated_max_vec = Quaternion.Euler(0, 0, m_restrict_obj_interval_angle) * enemy_to_other_hero_vec;
        }

        float angle_with_y_axis;
        // 1사분면
        if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x > m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, Vector2.up).eulerAngles.z;
        }
        // 2사분면
        else if (collision.transform.position.y > m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(Vector2.up, rotated_min_vec).eulerAngles.z;
        }
        // 3사분면
        else if (collision.transform.position.y < m_focus_enemy.transform.position.y && collision.transform.position.x < m_focus_enemy.transform.position.x)
        {
            angle_with_y_axis = Quaternion.FromToRotation(rotated_min_vec, -Vector2.up).eulerAngles.z;
        }
        // 4사분면
        else
        {
            angle_with_y_axis = Quaternion.FromToRotation(-Vector2.up, rotated_min_vec).eulerAngles.z;
        }

        m_vec_move_dir = -enemy_to_cur_hero_vec + (angle_with_y_axis < m_restrict_angle ? rotated_max_vec : rotated_min_vec);
    }
    */
}