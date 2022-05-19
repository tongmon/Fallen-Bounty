using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// �������� ��� ��ȣ�ۿ� ó��
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

    // ���� ��ġ�� ���
    public Dictionary<string, Vector2> m_enemy_pos;

    // m_group_by_target[�� �̸�] = �� �̸��� Ÿ������ �������� ��� �迭...
    Dictionary<string, List<Hero>> m_group_by_target;

    public float[] m_angles;
    public float[,] m_angle_def;

    // ���� ���콺
    public RaycastHit2D m_left_mouse;

    // ���� ���콺
    public RaycastHit2D m_right_mouse;

    LineRenderer m_line_renderer;

    void Start()
    {
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
            if (m_left_mouse.collider != null
                && m_left_mouse.collider.gameObject.transform.tag == "Enemy")
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
        #region ���� Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            m_left_mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);

            Hero sel_hero = m_selected_hero != null ? m_selected_hero.GetComponent<Hero>() : null;
            SpriteRenderer circle_below_hero = m_selected_hero != null ? m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>() : null;

            // �浹ü�� ������ �ִ� �༮�� Ŭ���� ���...[ex) ��, ����]
            if (m_left_mouse.collider != null)
            {
                // �� Ŭ��
                if (m_left_mouse.collider.gameObject.tag == "Enemy")
                {
                    if(m_selected_hero != null)
                    {
                        sel_hero.m_target_enemy = m_left_mouse.collider.gameObject;
                        sel_hero.m_point_target = m_left_mouse.collider.transform.position;
                        sel_hero.m_state_move = eMoveState.STATE_MOVE_STRAIGHT;
                        sel_hero.m_target_enemy.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); // ���õ� �� ���� ���� �׸�
                    }
                    else
                    {
                        m_left_mouse.collider.gameObject.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); // ���õ� �� ���� ���� �׸�
                    }
                }
                // ���� Ŭ��
                else if (m_left_mouse.collider.gameObject.tag == "Hero")
                {
                    if (m_selected_hero != null)
                    {
                        // �����ߴ� ���� �ѹ� �� Ŭ��
                        if (m_left_mouse.collider.gameObject == m_selected_hero)
                        {
                            /*
                            // �� �ҽ� �����ϸ� ���� �ѹ� �� Ŭ���� ��� ��� �ൿ�� ����
                            sel_hero.m_target_enemy = null;
                            sel_hero.m_point_target = sel_hero.transform.position;
                            sel_hero.m_state_move = eMoveState.STATE_MOVE_NONE;
                            circle_below_hero.color = new Color(255, 255, 255, 0);
                            */
                            circle_below_hero.color = new Color(255, 255, 255, 0);
                            m_selected_hero = null;
                        }
                        // ���� ������ �ٲ�� ���
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
                    // ������ �����ߴ� ������ �������� ���� ������ ������
                    else
                    {
                        m_selected_hero = m_left_mouse.collider.gameObject;
                        m_selected_hero.transform.Find("FocusCircle").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    }
                }
            }
            // �� �ٴ��� Ŭ���� ���
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

        #region ���� Ŭ��
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
