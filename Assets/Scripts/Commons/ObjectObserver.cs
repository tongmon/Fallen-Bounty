using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectObserver : MonoBehaviour
{
    private static ObjectObserver m_inst = null;

    // 해당 전투 장면에 존재하는 모든 생명체가 들어있는 배열
    // 예를 m_creatures["Hero"]하면 장면에 존재하는 모든 영웅에 대한 배열이 나옴
    public Dictionary<string, List<Creature>> m_creatures;

    // m_group_by_target[특정 생명체] = 특정 생명체를 타겟팅한 생명체가 담긴 배열
    public Dictionary<Creature, List<Creature>> m_group_by_target;

    // m_group_by_target에 담기는 리스트들은 특정 규칙을 가지고 담김
    // 리스트 초반은 특정 생명체의 오른쪽에 위치하는 녀석들이 y축 순으로 정렬되어 담기고
    // 리스트 후반은 특정 생명체의 왼쪽에 위치하는 녀석들이 y축 순으로 정렬되어 담긴다.
    // 해당값은 오른쪽에 위치해 있는 생명체의 개수가 들어간다.
    public Dictionary<Creature, int> m_right_side_creature_index;

    public float[,] m_angle_def;

    public static ObjectObserver Instance
    {
        get
        {
            if (null == m_inst)
            {
                return null;
            }
            return m_inst;
        }
    }

    private void Awake()
    {
        if (!m_inst)
        {
            m_inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_inst.m_group_by_target = new Dictionary<Creature, List<Creature>>();
        m_inst.m_creatures = new Dictionary<string, List<Creature>>();
        Creature[] all_creatures = Resources.FindObjectsOfTypeAll<Creature>();
        for (int i = 0; i < all_creatures.Length; i++)
        {
            string type_name = "Creature";
            if(all_creatures[i] is Hero)
            {
                type_name = "Hero";
            }
            else if (all_creatures[i] is Enemy)
            {
                type_name = "Enemy";
            }

            if (!m_inst.m_creatures.TryGetValue(type_name, out List<Creature> tmp))
                m_inst.m_creatures[type_name] = new List<Creature>();
            m_inst.m_creatures[type_name].Add(all_creatures[i]);
        }

        m_inst.m_angle_def = new float[4, 4];
        m_inst.m_angle_def[0, 0] = 0; // 좌 or 우측에 영웅이 한명
        m_inst.m_angle_def[1, 0] = -20; m_inst.m_angle_def[1, 1] = 20; // 좌 or 우측에 영웅이 두명
        m_inst.m_angle_def[2, 0] = -25; m_inst.m_angle_def[2, 1] = 0; m_inst.m_angle_def[2, 2] = 25; // 좌 or 우측에 영웅이 세명
        m_inst.m_angle_def[3, 0] = -30; m_inst.m_angle_def[3, 1] = -15; m_inst.m_angle_def[3, 2] = 15; m_inst.m_angle_def[3, 3] = 30; // 좌 or 우측에 영웅이 네명
    }

    void Update()
    {
        m_inst.m_group_by_target.Clear();

        // 생명체 타겟팅이 같은 생명체들 끼리 묶음
        foreach (var dict_data in m_inst.m_creatures)
        {
            List<Creature> creature_list = dict_data.Value;
            for (int i = 0; i < creature_list.Count; i++)
            {
                if (creature_list[i].m_target)
                {
                    if (!m_inst.m_group_by_target.TryGetValue(creature_list[i].m_target, out List<Creature> tmp))
                        m_inst.m_group_by_target[creature_list[i].m_target] = new List<Creature>();
                    m_inst.m_group_by_target[creature_list[i].m_target].Add(creature_list[i]);
                }
            }
        }

        foreach (var dict_data in m_inst.m_group_by_target)
        {
            List<Creature> creature_list = dict_data.Value;
            List<Creature> right_pos = new List<Creature>(), left_pos = new List<Creature>();
            for (int i = 0; i < creature_list.Count; i++)
            {
                if (creature_list[i].m_physics_component.GetPosition().x < dict_data.Key.m_physics_component.GetPosition().x)
                    left_pos.Add(creature_list[i]);
                else
                    right_pos.Add(creature_list[i]);
            }

            right_pos = right_pos.OrderBy(y => y.m_physics_component.GetPosition().y).ToList();
            left_pos = left_pos.OrderBy(y => y.m_physics_component.GetPosition().y).ToList();

            m_inst.m_right_side_creature_index[dict_data.Key] = right_pos.Count;

            creature_list.Clear();
            creature_list.AddRange(right_pos);
            creature_list.AddRange(left_pos);
        }
    }
}
