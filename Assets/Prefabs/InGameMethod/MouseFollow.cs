using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // 길을 선택
    private bool m_path_select;

    // 도착함
    private bool m_path_arrived;

    //각도가 클시 이동할 벡터 한번만 설정
    private bool m_vec_select;

    //벡터 이동 종료 확인
    private bool m_vec_move_done;

    // 방향 벡터
    private Vector2 m_obj_to_target_vec;

    // 

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
    private float m_vertical_speed ;

    // 공격 사거리
    private float m_attack_range;

    // 캐릭터가 적보다 너무 위에 있는 경우 캐릭터를 내리기 위한 각도
    private float m_restrict_angle;

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

        m_vec_move_done = false;
        m_vec_select = false;

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
            m_vec_select = false;
            m_vec_move_done = false;

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
                m_obj_to_target_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }

        // 길이 선택된 경우
        if (m_path_select)
        {
            if (m_hit_right_mouse.collider != null)
                m_obj_to_target_vec = m_hit_right_mouse.transform.position;

            // 각도에 따른 좌우 구분을 하기 위해 각도 검사 재검토 필요
            if (m_obj_to_target_vec.x < 0)
                m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else 
                m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // 가는 방향 설정
            Vector2 dir = new Vector2(0, 0);

            float distance_to_target = Vector2.Distance(m_focus_object.transform.position, m_obj_to_target_vec);

            // 길이 정해졌는데 적이 선택된 경우
            if (m_hit_right_mouse.collider != null
                && m_hit_right_mouse.collider.gameObject.transform.tag != "Character")
            {
                if (Mathf.Abs(distance_to_target - m_attack_range) > 0.05f)
                {
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= m_attack_range)
                        dir = (Vector2)(m_hit_right_mouse.transform.position - m_focus_object.transform.position);
                    // 적이 사거리보다 가까운 경우
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

                        //if (!m_vec_select) //이동벡터는 한번만 설정
                        //{
                            Vector2 move_vec = Quaternion.Euler(0, 0, angle) * enemy_to_obj_vec;
                            move_vec -= enemy_to_obj_vec;
                            move_vec.Normalize();
                            m_vec_select = true;
                            dir = move_vec;
                        //}

                        // m_focus_object.transform.Translate(new Vector2(m_target_vec.x * m_horizontal_speed * Time.deltaTime, m_target_vec.y * m_vertical_speed * Time.deltaTime), Space.World);
                    }
                }
            }
            // 길이 정해졌는데 땅이 선택된 경우
            else if(distance_to_target > 0.05f)
                dir = m_obj_to_target_vec - (Vector2)m_focus_object.transform.position;

            dir.Normalize();
            m_focus_object.transform.Translate(new Vector2(dir.x * m_horizontal_speed * Time.deltaTime, dir.y * m_vertical_speed * Time.deltaTime), Space.World);

            // 라인 삽입
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

    #region 각도 기준 로직
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

//바라보는 방향 각도계산
/*Vector2 m_direction = new Vector2(m_focus_object.transform.position.x - m_focus_enemy.transform.position.x, m_focus_object.transform.position.y - m_focus_enemy.transform.position.y);
float m_angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
Quaternion m_angleAxis = Quaternion.AngleAxis(m_angle - 90f, Vector3.forward);
Quaternion m_rotation = Quaternion.Slerp(m_focus_object.transform.rotation, m_angleAxis, 10f);*/
