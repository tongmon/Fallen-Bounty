using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    GameObject[] m_enemies;

    GameObject[] m_heroes;

    Dictionary<string, Vector2> m_enemy_pos;

    Dictionary<string, List<MouseFollow>> m_group_by_target;

    // Start is called before the first frame update
    void Start()
    {
        m_group_by_target = new Dictionary<string, List<MouseFollow>>();
        m_enemy_pos = new Dictionary<string, Vector2>();

        m_heroes = GameObject.FindGameObjectsWithTag("Character");
        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
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
                if(!m_group_by_target.TryGetValue(hero.m_focus_enemy.name, out List<MouseFollow> temp))
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
                // 영웅의 속도가 없는 상태에서만 위치를 재조정한다.
                if (heros[i].m_vec_move_dir == null) // heros[i].m_vec_move_dir == null
                {
                    if (heros[i].transform.position.x < m_enemy_pos[enemy_name].x)
                        left_pos_heros.Add(heros[i]);
                    else
                        right_pos_heros.Add(heros[i]);
                }
            }

            right_pos_heros = right_pos_heros.OrderBy(y => y.transform.position.y).ToList();
            left_pos_heros = left_pos_heros.OrderBy(y => y.transform.position.y).ToList();

            switch (right_pos_heros.Count)
            {
                case 1:
                    {
                        float angle = Quaternion.FromToRotation(Vector2.right, (Vector2)right_pos_heros[0].transform.position - m_enemy_pos[enemy_name]).eulerAngles.z;
                        if (Mathf.Abs(angle) >= 1.0f)
                        {
                            right_pos_heros[0].m_move_state = MouseFollow.eMoveState.STATE_MOVE_ROTATION;
                            right_pos_heros[0].m_vec_move_dir = m_enemy_pos[enemy_name] - (Vector2)right_pos_heros[0].transform.position + new Vector2(right_pos_heros[0].m_attack_range, 0);
                        }
                        else
                        {
                            right_pos_heros[0].m_move_state = MouseFollow.eMoveState.STATE_MOVE_NONE;
                            right_pos_heros[0].m_vec_move_dir = null;
                        }
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

            switch (left_pos_heros.Count)
            {
                case 1:
                    {
                        float angle = Mathf.Atan2(right_pos_heros[0].transform.position.y, right_pos_heros[0].transform.position.x) * Mathf.Rad2Deg;
                        if (Mathf.Abs(180 - angle) >= 0.05f)
                        {
                            right_pos_heros[0].m_move_state = MouseFollow.eMoveState.STATE_MOVE_ROTATION;
                            right_pos_heros[0].m_vec_move_dir = m_enemy_pos[enemy_name] - (Vector2)right_pos_heros[0].transform.position + new Vector2(-right_pos_heros[0].m_attack_range, 0);
                        }
                        else
                            right_pos_heros[0].m_move_state = MouseFollow.eMoveState.STATE_MOVE_NONE;
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
    }
}
