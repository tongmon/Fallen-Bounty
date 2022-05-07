using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    List<MouseFollow> m_heros = new List<MouseFollow>();

    Dictionary<string, Vector2> m_enemy_pos;

    IDictionary<string, List<MouseFollow>> m_group_by_target;

    // Start is called before the first frame update
    void Start()
    {
        m_group_by_target = new Dictionary<string, List<MouseFollow>>();
        m_enemy_pos = new Dictionary<string, Vector2>();

        GameObject[] hero_objs = GameObject.FindGameObjectsWithTag("Character");

        for (int i = 0; i < hero_objs.Length; i++)
            m_heros.Add(hero_objs[i].GetComponent<MouseFollow>());

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
            m_enemy_pos[enemies[i].name] = enemies[i].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �� Ÿ������ ���� ������ ���� ����
        for (int i = 0; i < m_heros.Count; i++)
            m_group_by_target[m_heros[i].GetFocusEnemyName()].Add(m_heros[i]);

        // Ÿ������ ���� �������� ���� ��ġ ���¸� ������
        foreach(var heros in m_group_by_target.Values)
        {
            for (int i = 0; i < heros.Count; i++)
            {
                // ��ġ ���� ����
            }
        }
    }
}
