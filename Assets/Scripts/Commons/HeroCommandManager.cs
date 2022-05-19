using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// 영웅들의 모든 상호작용 처리
public class HeroCommandManager : MonoBehaviour, IDragHandler
{
    public enum eMoveState
    {
        STATE_MOVE_STRAIGHT,
        STATE_MOVE_ROTATION,
        STATE_MOVE_NONE,
    }

    public GameObject[] m_enemies;

    public GameObject[] m_heroes;

    public GameObject m_selected_hero;

    // 적의 위치가 담김
    public Dictionary<string, Vector2> m_enemy_pos;

    // m_group_by_target[적 이름] = 적 이름을 타겟팅한 영웅들이 담긴 배열...
    Dictionary<string, List<Hero>> m_group_by_target;

    public float[] m_angles;
    public float[,] m_angle_def;

    // 좌측 마우스
    public RaycastHit2D m_left_mouse;

    // 우측 마우스
    public RaycastHit2D m_right_mouse;

    LineRenderer m_line_renderer;

    void Start()
    {
        m_angles = new float[4]; // 많이 쓰는 각도 배열은 버퍼로...

        // 좌 or 우측 영웅 편향도에 따른 각도
        m_angle_def = new float[4, 4];
        m_angle_def[0, 0] = 0; // 좌 or 우측에 영웅이 한명
        m_angle_def[1, 0] = -20; m_angle_def[1, 1] = 20; // 좌 or 우측에 영웅이 두명
        m_angle_def[2, 0] = -25; m_angle_def[2, 1] = 0; m_angle_def[2, 2] = 25; // 좌 or 우측에 영웅이 세명
        m_angle_def[3, 0] = -30; m_angle_def[3, 1] = -15; m_angle_def[3, 2] = 15; m_angle_def[3, 3] = 30; // 좌 or 우측에 영웅이 네명

        m_group_by_target = new Dictionary<string, List<Hero>>();
        m_enemy_pos = new Dictionary<string, Vector2>();

        m_heroes = GameObject.FindGameObjectsWithTag("Hero");
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        OnMouseEvent();

        // 특정 조건인 경우에만 밑을 수행하여 최적화 가능
        // 비활성화 되거나 죽은 개체들 빼고 넣는 로직 추가해야댐
        //m_heroes = GameObject.FindGameObjectsWithTag("Hero");
        //m_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 이동 상태는 기본적으로 None -> Straight -> Rotation(도중에 Straight로 빠질 수 있음) -> None 순으로 진행된다.

        #region 영웅 직선 이동 조정
        for (int i = 0; i < m_heroes.Length; i++)
        {
            var hero = m_heroes[i].GetComponent<Hero>();

            if (hero.m_state_move != eMoveState.STATE_MOVE_STRAIGHT)
                continue;

            float distance_to_target = Vector2.Distance(hero.transform.position, hero.m_point_target);

            // 길이 정해졌는데 적이 선택된 경우
            if (m_left_mouse.collider != null
                && m_left_mouse.collider.gameObject.transform.tag == "Enemy")
            {
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - hero.m_attack_range) > 0.05f)
                {
                    hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= hero.m_attack_range)
                        hero.m_vec_direction = hero.m_point_target - (Vector2)hero.transform.position;
                    // 적이 사거리보다 가까운 경우
                    else
                        hero.m_vec_direction = (Vector2)hero.transform.position - hero.m_point_target;
                }
                else
                    hero.m_state_move = eMoveState.STATE_MOVE_ROTATION;
            }
            // 길이 정해졌는데 땅이 선택된 경우
            else
            {
                if (distance_to_target > 0.05f)
                {
                    hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                    hero.m_vec_direction = hero.m_point_target - (Vector2)hero.transform.position;
                }
                else
                    hero.m_state_move = eMoveState.STATE_MOVE_NONE;
            }
        }
        #endregion

        #region 영웅 각도 조정
        m_group_by_target.Clear();
        m_enemy_pos.Clear();

        // 갱신된 적의 위치 획득
        for (int i = 0; i < m_enemies.Length; i++)
            m_enemy_pos[m_enemies[i].name] = m_enemies[i].transform.position;

        // 적 타겟팅이 같은 영웅들 끼리 묶음
        for (int i = 0; i < m_heroes.Length; i++)
        {
            var hero = m_heroes[i].GetComponent<Hero>();
            if (hero.m_target_enemy != null)
            {
                if (!m_group_by_target.TryGetValue(hero.m_target_enemy.name, out List<Hero> temp))
                    m_group_by_target[hero.m_target_enemy.name] = new List<Hero>();
                m_group_by_target[hero.m_target_enemy.name].Add(hero);
            }
        }

        // 타겟팅이 같은 영웅들의 현재 위치 상태를 조사함
        foreach (var enemy_name in m_enemy_pos.Keys)
        {
            if (!m_group_by_target.TryGetValue(enemy_name, out List<Hero> temp))
                continue;

            List<Hero> right_pos_heros = new List<Hero>();
            List<Hero> left_pos_heros = new List<Hero>();

            List<Hero> heros = m_group_by_target[enemy_name];

            for (int i = 0; i < heros.Count; i++)
            {
                // 영웅이동 방법이 회전이나 정지 상태인 경우 영웅의 각도를 조절한다.
                if (heros[i].m_state_move == eMoveState.STATE_MOVE_NONE || heros[i].m_state_move == eMoveState.STATE_MOVE_ROTATION)
                {
                    float distance_to_target = Vector2.Distance(heros[i].transform.position, heros[i].m_point_target);

                    /*
                    if (distance_to_target > 0.1f)
                    {
                        // 각도 조정을 하려했는데 영웅과 적 사이 거리가 멀다면 다시 Rotation 상태에서 Straight 상태로 변경한다.
                        heros[i].m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                        continue;
                    }
                    */

                    if (heros[i].transform.position.x < m_enemy_pos[enemy_name].x)
                        left_pos_heros.Add(heros[i]);
                    else
                        right_pos_heros.Add(heros[i]);
                }
            }

            // y축 순으로 정렬
            right_pos_heros = right_pos_heros.OrderBy(y => y.transform.position.y).ToList();
            left_pos_heros = left_pos_heros.OrderBy(y => y.transform.position.y).ToList();

            Vector2 dest_vec;

            // 우측 영웅 각도 조절
            for (int i = 0; i < right_pos_heros.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    m_angles[j] = Quaternion.FromToRotation(Vector2.right, (Vector2)right_pos_heros[j].transform.position - m_enemy_pos[enemy_name]).eulerAngles.z;
                    m_angles[j] = Mathf.Min(360 - m_angles[j], m_angles[j]);

                    if (Mathf.Abs(Mathf.Abs(m_angles[j]) - Mathf.Abs(m_angle_def[i, j])) >= 1.0f)
                    {
                        right_pos_heros[j].m_state_move = eMoveState.STATE_MOVE_ROTATION;
                        dest_vec = Quaternion.Euler(0, 0, m_angle_def[i, j]) * new Vector2(right_pos_heros[j].m_attack_range, 0);
                        right_pos_heros[j].m_vec_direction = m_enemy_pos[enemy_name] - (Vector2)right_pos_heros[j].transform.position + dest_vec;
                    }
                    else
                    {
                        right_pos_heros[j].m_state_move = eMoveState.STATE_MOVE_NONE;
                    }
                }
            }

            // 좌측 영웅 각도 조절
            for (int i = 0; i < left_pos_heros.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    m_angles[j] = Quaternion.FromToRotation(Vector2.left, (Vector2)left_pos_heros[j].transform.position - m_enemy_pos[enemy_name]).eulerAngles.z;
                    m_angles[j] = Mathf.Min(360 - m_angles[j], m_angles[j]);

                    if (Mathf.Abs(Mathf.Abs(m_angles[j]) - Mathf.Abs(m_angle_def[i, j])) >= 1.0f)
                    {
                        left_pos_heros[j].m_state_move = eMoveState.STATE_MOVE_ROTATION;
                        dest_vec = Quaternion.Euler(0, 0, 180 - m_angle_def[i, j]) * new Vector2(left_pos_heros[j].m_attack_range, 0);
                        left_pos_heros[j].m_vec_direction = m_enemy_pos[enemy_name] - (Vector2)left_pos_heros[j].transform.position + dest_vec;
                    }
                    else
                        left_pos_heros[j].m_state_move = eMoveState.STATE_MOVE_NONE;
                }
            }
        }
        #endregion
    }

    void OnMouseEvent()
    {
        #region 좌측 클릭
        if (Input.GetMouseButtonDown(0))
        {
            m_left_mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);

            Hero sel_hero = m_selected_hero != null ? m_selected_hero.GetComponent<Hero>() : null;
            SpriteRenderer circle_below_hero = m_selected_hero != null ? m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>() : null;

            // 충돌체를 가지고 있는 녀석을 클릭한 경우...[ex) 적, 영웅]
            if (m_left_mouse.collider != null)
            {
                // 적 클릭
                if (m_left_mouse.collider.gameObject.tag == "Enemy")
                {
                    if(m_selected_hero != null)
                    {
                        sel_hero.m_target_enemy = m_left_mouse.collider.gameObject;
                        sel_hero.m_point_target = m_left_mouse.collider.transform.position;
                        sel_hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                        sel_hero.m_target_enemy.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); // 선택된 적 밑의 원을 그림
                    }
                    else
                    {
                        m_left_mouse.collider.gameObject.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); // 선택된 적 밑의 원을 그림
                    }
                }
                // 영웅 클릭
                else if (m_left_mouse.collider.gameObject.tag == "Hero")
                {
                    if (m_selected_hero != null)
                    {
                        // 선택했던 영웅 한번 더 클릭
                        if (m_left_mouse.collider.gameObject == m_selected_hero)
                        {
                            /*
                            // 밑 소스 적용하면 영웅 한번 더 클릭한 경우 모든 행동을 멈춤
                            sel_hero.m_target_enemy = null;
                            sel_hero.m_point_target = sel_hero.transform.position;
                            sel_hero.m_state_move = eMoveState.STATE_MOVE_NONE;
                            circle_below_hero.color = new Color(255, 255, 255, 0);
                            */
                            circle_below_hero.color = new Color(255, 255, 255, 0);
                            m_selected_hero = null;
                        }
                        // 선택 영웅이 바뀌는 경우
                        else
                        {
                            for (int i = 0; i < m_heroes.Length; i++)
                            {
                                var hero = m_heroes[i].GetComponent<Hero>();
                                if(m_heroes[i] == m_left_mouse.collider.gameObject)
                                {
                                    m_selected_hero = m_left_mouse.collider.gameObject;
                                    m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                                }
                                else
                                    m_heroes[i].GetComponent<Hero>().transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                            }
                        }
                    }
                    // 예전에 선택했던 영웅이 없었으면 선택 영웅을 지정함
                    else
                    {
                        m_selected_hero = m_left_mouse.collider.gameObject;
                        m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    }
                }
            }
            // 맨 바닥을 클릭한 경우
            else
            {
                if (m_selected_hero != null)
                {
                    sel_hero.m_target_enemy = null;
                    sel_hero.m_point_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    sel_hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                }
            }
        }
        #endregion

        #region 우측 클릭
        if (Input.GetMouseButtonDown(1))
        {
            m_right_mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);
        }
        #endregion
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouse_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        m_line_renderer = m_selected_hero != null ? m_selected_hero.GetComponent<LineRenderer>() : null;

        if (m_line_renderer)
        {
            m_line_renderer.SetPosition(0, m_selected_hero.transform.GetChild(0).transform.position);
            if (m_selected_hero.GetComponent<Hero>().m_target_enemy != null)
                m_line_renderer.SetPosition(1, m_selected_hero.GetComponent<Hero>().m_target_enemy.transform.GetChild(0).transform.position);
            else
                m_line_renderer.SetPosition(1, m_selected_hero.GetComponent<Hero>().m_point_target);
        }

        throw new System.NotImplementedException();
    }
}
