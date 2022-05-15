using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public enum eMoveState
    {
        STATE_MOVE_STRAIGHT,
        STATE_MOVE_ROTATION,
        STATE_MOVE_NONE,
    }

    GameObject[] m_enemies;

    GameObject[] m_heroes;

    Dictionary<string, Vector2> m_enemy_pos;

    Dictionary<string, List<MouseFollow>> m_group_by_target;

    float[] m_angles;
    float[,] m_angle_def;

    // Start is called before the first frame update
    void Start()
    {
        m_angles = new float[4];
        m_angle_def = new float[4, 4];
        m_angle_def[0, 0] = 0;
        m_angle_def[1, 0] = -20; m_angle_def[1, 1] = 20;
        m_angle_def[2, 0] = -25; m_angle_def[2, 1] = 0; m_angle_def[2, 2] = 25;
        m_angle_def[3, 0] = -30; m_angle_def[3, 1] = -15; m_angle_def[3, 2] = 15; m_angle_def[3, 3] = 30;

        m_group_by_target = new Dictionary<string, List<MouseFollow>>();
        m_enemy_pos = new Dictionary<string, Vector2>();

        m_heroes = GameObject.FindGameObjectsWithTag("Character");
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        #region ���� ���� �̵� ����
        for (int i = 0; i < m_heroes.Length; i++)
        {
            var hero = m_heroes[i].GetComponent<MouseFollow>();

            if (hero.m_move_state != eMoveState.STATE_MOVE_STRAIGHT)
                continue;

            float distance_to_target = Vector2.Distance(hero.m_focus_object.transform.position, hero.m_target_point);

            // ������ ���� �¿� ������ �ϱ� ���� ���� �˻� ����� �ʿ�
            if (hero.m_target_point.x < 0)
                hero.m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                hero.m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // ���� �������µ� ���� ���õ� ���
            if (hero.m_hit_right_mouse.collider != null
                && hero.m_hit_right_mouse.collider.gameObject.transform.tag == "Enemy")
            {
                // ���� ��ġ�� ��Ÿ��� ���� ����
                if (Mathf.Abs(distance_to_target - hero.m_attack_range) > 0.05f)
                {
                    hero.m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    // ���� ��Ÿ� �ȿ� ���� ���
                    if (distance_to_target >= hero.m_attack_range)
                        hero.m_vec_move_dir = hero.m_target_point - (Vector2)hero.m_focus_object.transform.position;
                    // ���� ��Ÿ����� ����� ���
                    else
                        hero.m_vec_move_dir = (Vector2)hero.m_focus_object.transform.position - hero.m_target_point;
                }
                else
                    hero.m_move_state = eMoveState.STATE_MOVE_ROTATION;
            }
            // ���� �������µ� ���� ���õ� ���
            else
            {
                if (distance_to_target > 0.1f)
                {
                    hero.m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    hero.m_vec_move_dir = hero.m_target_point - (Vector2)hero.m_focus_object.transform.position;
                }
                else
                    hero.m_move_state = eMoveState.STATE_MOVE_NONE;
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
            var hero = m_heroes[i].GetComponent<MouseFollow>();
            if (hero.m_focus_enemy != null)
            {
                if (!m_group_by_target.TryGetValue(hero.m_focus_enemy.name, out List<MouseFollow> temp))
                    m_group_by_target[hero.m_focus_enemy.name] = new List<MouseFollow>();
                m_group_by_target[hero.m_focus_enemy.name].Add(hero);
            }
        }

        // Ÿ������ ���� �������� ���� ��ġ ���¸� ������
        foreach (var enemy_name in m_enemy_pos.Keys)
        {
            if (!m_group_by_target.TryGetValue(enemy_name, out List<MouseFollow> temp))
                continue;

            List<MouseFollow> right_pos_heros = new List<MouseFollow>();
            List<MouseFollow> left_pos_heros = new List<MouseFollow>();

            List<MouseFollow> heros = m_group_by_target[enemy_name];

            for (int i = 0; i < heros.Count; i++)
            {
                // �����̵� ����� ȸ���̳� ���� ������ ��� ������ ������ �����Ѵ�.
                if (heros[i].m_move_state == eMoveState.STATE_MOVE_NONE || heros[i].m_move_state == eMoveState.STATE_MOVE_ROTATION)
                {
                    if (heros[i].transform.position.x < m_enemy_pos[enemy_name].x)
                        left_pos_heros.Add(heros[i]);
                    else
                        right_pos_heros.Add(heros[i]);
                }
            }

            right_pos_heros = right_pos_heros.OrderBy(y => y.transform.position.y).ToList();
            left_pos_heros = left_pos_heros.OrderBy(y => y.transform.position.y).ToList();

            Vector2 dest_vec;

            for (int i = 0; i < right_pos_heros.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    m_angles[j] = Quaternion.FromToRotation(Vector2.right, (Vector2)right_pos_heros[j].transform.position - m_enemy_pos[enemy_name]).eulerAngles.z;
                    m_angles[j] = Mathf.Min(360 - m_angles[j], m_angles[j]);

                    if (Mathf.Abs(Mathf.Abs(m_angles[j]) - Mathf.Abs(m_angle_def[i, j])) >= 1.0f)
                    {
                        right_pos_heros[j].m_move_state = eMoveState.STATE_MOVE_ROTATION;
                        dest_vec = Quaternion.Euler(0, 0, m_angle_def[i, j]) * new Vector2(right_pos_heros[j].m_attack_range, 0);
                        right_pos_heros[j].m_vec_move_dir = m_enemy_pos[enemy_name] - (Vector2)right_pos_heros[j].transform.position + dest_vec;
                    }
                    else
                    {
                        right_pos_heros[j].m_move_state = eMoveState.STATE_MOVE_NONE;
                    }
                }
            }

            for (int i = 0; i < left_pos_heros.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    m_angles[j] = Quaternion.FromToRotation(Vector2.left, (Vector2)left_pos_heros[j].transform.position - m_enemy_pos[enemy_name]).eulerAngles.z;
                    m_angles[j] = Mathf.Min(360 - m_angles[j], m_angles[j]);

                    if (Mathf.Abs(Mathf.Abs(m_angles[j]) - Mathf.Abs(m_angle_def[i, j])) >= 1.0f)
                    {
                        left_pos_heros[j].m_move_state = eMoveState.STATE_MOVE_ROTATION;
                        dest_vec = Quaternion.Euler(0, 0, 180 - m_angle_def[i, j]) * new Vector2(left_pos_heros[j].m_attack_range, 0);
                        left_pos_heros[j].m_vec_move_dir = m_enemy_pos[enemy_name] - (Vector2)left_pos_heros[j].transform.position + dest_vec;
                    }
                    else
                        left_pos_heros[j].m_move_state = eMoveState.STATE_MOVE_NONE;
                }
            }
        }
        #endregion
    }
}
