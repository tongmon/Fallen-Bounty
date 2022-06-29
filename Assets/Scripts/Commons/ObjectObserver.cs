using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectObserver : MonoBehaviour
{
    private static ObjectObserver m_inst = null;

    // �ش� ���� ��鿡 �����ϴ� ��� ����ü�� ����ִ� �迭
    // ���� m_creatures["Hero"]�ϸ� ��鿡 �����ϴ� ��� ������ ���� �迭�� ����
    public Dictionary<string, List<Creature>> m_creatures;

    // m_group_by_target[Ư�� ����ü] = Ư�� ����ü�� Ÿ������ ����ü�� ��� �迭
    public Dictionary<Creature, List<Creature>> m_group_by_target;

    // m_group_by_target�� ���� ����Ʈ���� Ư�� ��Ģ�� ������ ���
    // ����Ʈ �ʹ��� Ư�� ����ü�� �����ʿ� ��ġ�ϴ� �༮���� y�� ������ ���ĵǾ� ����
    // ����Ʈ �Ĺ��� Ư�� ����ü�� ���ʿ� ��ġ�ϴ� �༮���� y�� ������ ���ĵǾ� ����.
    // �ش簪�� �����ʿ� ��ġ�� �ִ� ����ü�� ������ ����.
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
        m_inst.m_angle_def[0, 0] = 0; // �� or ������ ������ �Ѹ�
        m_inst.m_angle_def[1, 0] = -20; m_inst.m_angle_def[1, 1] = 20; // �� or ������ ������ �θ�
        m_inst.m_angle_def[2, 0] = -25; m_inst.m_angle_def[2, 1] = 0; m_inst.m_angle_def[2, 2] = 25; // �� or ������ ������ ����
        m_inst.m_angle_def[3, 0] = -30; m_inst.m_angle_def[3, 1] = -15; m_inst.m_angle_def[3, 2] = 15; m_inst.m_angle_def[3, 3] = 30; // �� or ������ ������ �׸�
    }

    void Update()
    {
        m_inst.m_group_by_target.Clear();

        // ����ü Ÿ������ ���� ����ü�� ���� ����
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
