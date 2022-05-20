using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// �������� ��� ��ȣ�ۿ� ó��
public class HeroCommandManager : MonoBehaviour
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

    // ���� ��ġ�� ���
    public Dictionary<string, Vector2> m_enemy_pos;

    // m_group_by_target[�� �̸�] = �� �̸��� Ÿ������ �������� ��� �迭...
    Dictionary<string, List<Hero>> m_group_by_target;

    public float[] m_angles;
    public float[,] m_angle_def;

    // ���콺 ���� ĳ����
    public RaycastHit2D[] m_mouse;

    public float[] m_mouse_hold_time;

    public LineRenderer m_line_renderer;

    void Start()
    {
        m_mouse_hold_time = new float[2];
        m_mouse = new RaycastHit2D[2];

        m_angles = new float[4]; // ���� ���� ���� �迭�� ���۷�...

        // �� or ���� ���� ���⵵�� ���� ����
        m_angle_def = new float[4, 4];
        m_angle_def[0, 0] = 0; // �� or ������ ������ �Ѹ�
        m_angle_def[1, 0] = -20; m_angle_def[1, 1] = 20; // �� or ������ ������ �θ�
        m_angle_def[2, 0] = -25; m_angle_def[2, 1] = 0; m_angle_def[2, 2] = 25; // �� or ������ ������ ����
        m_angle_def[3, 0] = -30; m_angle_def[3, 1] = -15; m_angle_def[3, 2] = 15; m_angle_def[3, 3] = 30; // �� or ������ ������ �׸�

        m_group_by_target = new Dictionary<string, List<Hero>>();
        m_enemy_pos = new Dictionary<string, Vector2>();

        m_heroes = GameObject.FindGameObjectsWithTag("Hero");
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        OnMouseEvent();

        // Ư�� ������ ��쿡�� ���� �����Ͽ� ����ȭ ����
        // ��Ȱ��ȭ �ǰų� ���� ��ü�� ���� �ִ� ���� �߰��ؾߴ�
        //m_heroes = GameObject.FindGameObjectsWithTag("Hero");
        //m_enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // �̵� ���´� �⺻������ None -> Straight -> Rotation(���߿� Straight�� ���� �� ����) -> None ������ ����ȴ�.

        #region ���� ���� �̵� ����
        for (int i = 0; i < m_heroes.Length; i++)
        {
            var hero = m_heroes[i].GetComponent<Hero>();

            if (hero.m_state_move != eMoveState.STATE_MOVE_STRAIGHT)
                continue;

            float distance_to_target = Vector2.Distance(hero.transform.position, hero.m_point_target);

            // ���� �������µ� ���� ���õ� ���
            if (m_mouse[0].collider != null
                && m_mouse[0].collider.gameObject.transform.tag == "Enemy")
            {
                // ���� ��ġ�� ��Ÿ��� ���� ����
                if (Mathf.Abs(distance_to_target - hero.m_attack_range) > 0.05f)
                {
                    hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                    // ���� ��Ÿ� �ȿ� ���� ���
                    if (distance_to_target >= hero.m_attack_range)
                        hero.m_vec_direction = hero.m_point_target - (Vector2)hero.transform.position;
                    // ���� ��Ÿ����� ����� ���
                    else
                        hero.m_vec_direction = (Vector2)hero.transform.position - hero.m_point_target;
                }
                else
                    hero.m_state_move = eMoveState.STATE_MOVE_ROTATION;
            }
            // ���� �������µ� ���� ���õ� ���
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

        #region ���� ���� ����
        m_group_by_target.Clear();
        m_enemy_pos.Clear();

        // ���ŵ� ���� ��ġ ȹ��
        for (int i = 0; i < m_enemies.Length; i++)
            m_enemy_pos[m_enemies[i].name] = m_enemies[i].transform.position;

        // �� Ÿ������ ���� ������ ���� ����
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

        // Ÿ������ ���� �������� ���� ��ġ ���¸� ������
        foreach (var enemy_name in m_enemy_pos.Keys)
        {
            if (!m_group_by_target.TryGetValue(enemy_name, out List<Hero> temp))
                continue;

            List<Hero> right_pos_heros = new List<Hero>();
            List<Hero> left_pos_heros = new List<Hero>();

            List<Hero> heros = m_group_by_target[enemy_name];

            for (int i = 0; i < heros.Count; i++)
            {
                // �����̵� ����� ȸ���̳� ���� ������ ��� ������ ������ �����Ѵ�.
                if (heros[i].m_state_move == eMoveState.STATE_MOVE_NONE || heros[i].m_state_move == eMoveState.STATE_MOVE_ROTATION)
                {
                    float distance_to_target = Vector2.Distance(heros[i].transform.position, heros[i].m_point_target);

                    /*
                    if (distance_to_target > 0.1f)
                    {
                        // ���� ������ �Ϸ��ߴµ� ������ �� ���� �Ÿ��� �ִٸ� �ٽ� Rotation ���¿��� Straight ���·� �����Ѵ�.
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

            // y�� ������ ����
            right_pos_heros = right_pos_heros.OrderBy(y => y.transform.position.y).ToList();
            left_pos_heros = left_pos_heros.OrderBy(y => y.transform.position.y).ToList();

            Vector2 dest_vec;

            // ���� ���� ���� ����
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

            // ���� ���� ���� ����
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
        #region ���� Ŭ�� �ٿ�
        if (Input.GetMouseButtonDown(0))
        {
            m_mouse[0] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity/*, 1 << LayerMask.NameToLayer("Command Layer")*/);

            if (m_mouse[0].collider.gameObject.tag == "Hero")
            {
                m_selected_hero = m_mouse[0].collider.gameObject;
                m_line_renderer = m_selected_hero.GetComponent<Hero>().m_line_renderer;
            }
        }
        #endregion

        #region ���� Ŭ�� �ٿ�
        else if (Input.GetMouseButtonDown(1))
        {
            m_mouse[1] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Command Layer"));
        }
        #endregion

        #region ���� Ŭ����
        else if (Input.GetMouseButtonUp(0))
        {
            m_mouse[0] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity/*, 1 << LayerMask.NameToLayer("Command Layer")*/);

            Hero sel_hero = m_selected_hero != null ? m_selected_hero.GetComponent<Hero>() : null;
            SpriteRenderer circle_below_hero = m_selected_hero != null ? m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>() : null;

            // ���콺�� ��� ���� ������ ���� Ŭ������ ������
            if (m_mouse_hold_time[0] <= 0.5f)
            {
                if (m_mouse[0].collider != null)
                {
                    // �� Ŭ��
                    if (m_mouse[0].collider.gameObject.tag == "Enemy")
                    {
                        // ���� ���� ������ ������� ��
                    }
                    // ���� Ŭ��
                    else if (m_mouse[0].collider.gameObject.tag == "Hero")
                    {
                        if (m_selected_hero != null)
                        {
                            // �����ߴ� ���� �ѹ� �� Ŭ��
                            if (m_mouse[0].collider.gameObject == m_selected_hero)
                            {
                                circle_below_hero.color = new Color(255, 255, 255, 0);
                                m_selected_hero = null;
                            }
                            // ���� ������ �ٲ�� ���
                            else
                            {
                                for (int i = 0; i < m_heroes.Length; i++)
                                {
                                    var hero = m_heroes[i].GetComponent<Hero>();
                                    if (m_heroes[i] == m_mouse[0].collider.gameObject)
                                    {
                                        m_selected_hero = m_mouse[0].collider.gameObject;
                                        m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                                    }
                                    else
                                        m_heroes[i].GetComponent<Hero>().transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                                }
                            }
                        }
                        // ������ �����ߴ� ������ �������� ���� ������ ������
                        else
                        {
                            m_selected_hero = m_mouse[0].collider.gameObject;
                            m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                        }
                    }
                }
                // �� �ٴ��� Ŭ���� ���
                else
                {
                    m_selected_hero = null;
                    for (int i = 0; i < m_heroes.Length; i++)
                    {
                        var hero = m_heroes[i].GetComponent<Hero>();
                        m_heroes[i].GetComponent<Hero>().transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
                    }
                }

                return;
            }

            // ���콺 �巡�� �ϴٰ� �� ����
            if (m_mouse[0].collider != null)
            {
                if (m_mouse[0].collider.gameObject.tag == "Enemy")
                {
                    sel_hero.m_target_enemy = m_mouse[0].collider.gameObject;
                    sel_hero.m_point_target = m_mouse[0].collider.transform.position;
                    sel_hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                }
                else if (m_mouse[0].collider.gameObject.tag == "Hero")
                {

                }
            }
            else
            {
                sel_hero.m_target_enemy = null;
                sel_hero.m_point_target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                sel_hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
            }
                

            m_mouse_hold_time[0] = 0;
        }
        #endregion

        #region ���� Ŭ����
        else if (Input.GetMouseButtonUp(1))
        {
            m_mouse[1] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Command Layer"));

            // ���콺�� ��� ���� ������ ���� Ŭ������ ������
            if (m_mouse_hold_time[1] <= 0.5f)
            {
                return;
            }

            m_mouse_hold_time[1] = 0;
        }
        #endregion

        #region ���� Ȧ��
        else if (Input.GetMouseButton(0))
        {
            m_mouse[0] = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity/*, 1 << LayerMask.NameToLayer("Command Layer")*/);

            m_mouse_hold_time[0] += Time.deltaTime;

            if (m_line_renderer)
            {
                Destroy(m_line_renderer);
                m_line_renderer = m_selected_hero.GetComponent<Hero>().m_line_renderer;

                m_line_renderer.sortingOrder = 1;
                m_line_renderer.material = new Material(Shader.Find("Sprites/Default"));
                m_line_renderer.material.color = Color.red;

                m_line_renderer.SetVertexCount(2);
                m_line_renderer.SetPosition(0, m_selected_hero.transform.Find("FocusCircle").transform.position);
                m_line_renderer.SetPosition(1, m_selected_hero.GetComponent<Hero>().m_point_target);
            }
        }
        #endregion

        #region ���� Ȧ��
        else if (Input.GetMouseButton(1))
        {
            m_mouse_hold_time[1] += Time.deltaTime;
        }
        #endregion
    }
}