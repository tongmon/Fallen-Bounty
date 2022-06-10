using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 투사체 오브젝트 풀링 시스템
// 생각해보니 투사체말고 화살 오브젝트 풀링으로 바꿔야 할 수도... dequeue할 때 문제가 됨
public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool m_instance;

    [SerializeField]
    private GameObject m_pooling_prefab;

    Queue<Projectile> m_pools;

    void Awake()
    {
        m_instance = this;
        m_pools = new Queue<Projectile>();
    }

    void Update()
    {
        
    }

    void InitPools(int pool_num)
    {
        for (int i = 0; i < pool_num; i++)
        {
            m_pools.Enqueue(CreateObj());
        }
    }

    Projectile CreateObj()
    {
        // 게임 실행 도중에 오브젝트 생성하는 함수 -> Instantiate
        var new_obj = Instantiate(m_pooling_prefab).GetComponent<Projectile>();
        new_obj.gameObject.SetActive(false);
        new_obj.transform.SetParent(transform);
        return new_obj;
    }

    public static Projectile GetObj()
    {
        if (m_instance.m_pools.Count > 0)
        {
            var obj = m_instance.m_pools.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var new_obj = m_instance.CreateObj();
            new_obj.gameObject.SetActive(true);
            new_obj.transform.SetParent(null);
            return new_obj;
        }
    }

    public static void ReturnObject(Projectile obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(m_instance.transform);
        m_instance.m_pools.Enqueue(obj);
    }
}
