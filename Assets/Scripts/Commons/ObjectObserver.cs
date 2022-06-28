using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectObserver : MonoBehaviour
{
    private static ObjectObserver m_inst = null;

    // 전투 시 생성된 적
    List<GameObject> m_enemies;

    // 현재 전투에 참전한 영웅
    List<GameObject> m_heroes;

    // 적의 위치가 담김
    Dictionary<string, Vector2> m_enemy_pos;

    // m_group_by_target[적 이름] = 적 이름을 타겟팅한 영웅들이 담긴 배열...
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
