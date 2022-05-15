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
        #region 영웅 직선 이동 조정
        for (int i = 0; i < m_heroes.Length; i++)
        {
            var hero = m_heroes[i].GetComponent<MouseFollow>();

            if (hero.m_move_state != eMoveState.STATE_MOVE_STRAIGHT)
                continue;

            float distance_to_target = Vector2.Distance(hero.m_focus_object.transform.position, hero.m_target_point);

            // 각도에 따른 좌우 구분을 하기 위해 각도 검사 재검토 필요
            if (hero.m_target_point.x < 0)
                hero.m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                hero.m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);

            // 길이 정해졌는데 적이 선택된 경우
            if (hero.m_hit_right_mouse.collider != null
                && hero.m_hit_right_mouse.collider.gameObject.transform.tag == "Enemy")
            {
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - hero.m_attack_range) > 0.05f)
                {
                    hero.m_move_state = eMoveState.STATE_MOVE_STRAIGHT;
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= hero.m_attack_range)
                        hero.m_vec_move_dir = hero.m_target_point - (Vector2)hero.m_focus_object.transform.position;
                    // 적이 사거리보다 가까운 경우
                    else
                        hero.m_vec_move_dir = (Vector2)hero.m_focus_object.transform.position - hero.m_target_point;
                }
                else
                    hero.m_move_state = eMoveState.STATE_MOVE_ROTATION;
            }
            // 길이 정해졌는데 땅이 선택된 경우
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

        #region 영웅 각도 조정
        m_group_by_target.Clear();
        m_enemy_pos.Clear();

        // 갱신된 적의 위치 획득
        for (int i = 0; i < m_enemies.Length; i++)
            m_enemy_pos[m_enemies[i].name] = m_enemies[i].transform.position;

        // 적 타겟팅이 같은 영웅들 끼리 묶음
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

        // 타겟팅이 같은 영웅들의 현재 위치 상태를 조사함
        foreach (var enemy_name in m_enemy_pos.Keys)
        {
            if (!m_group_by_target.TryGetValue(enemy_name, out List<MouseFollow> temp))
                continue;

            List<MouseFollow> right_pos_heros = new List<MouseFollow>();
            List<MouseFollow> left_pos_heros = new List<MouseFollow>();

            List<MouseFollow> heros = m_group_by_target[enemy_name];

            for (int i = 0; i < heros.Count; i++)
            {
                // 영웅이동 방법이 회전이나 정지 상태인 경우 영웅의 각도를 조절한다.
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
