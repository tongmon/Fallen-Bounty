using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectObserver : MonoBehaviour
{
    private static ObjectObserver m_inst = null;

    // ���� �� ������ ��
    List<GameObject> m_enemies;

    // ���� ������ ������ ����
    List<GameObject> m_heroes;

    // ���� ��ġ�� ���
    Dictionary<string, Vector2> m_enemy_pos;

    // m_group_by_target[�� �̸�] = �� �̸��� Ÿ������ �������� ��� �迭...
    Dictionary<string, List<Hero>> m_group_by_target;

    float[] m_angles;
    float[,] m_angle_def;

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
        m_enemies = new List<GameObject>();
        m_heroes = new List<GameObject>();
    }

    void Update()
    {
        
    }
}
