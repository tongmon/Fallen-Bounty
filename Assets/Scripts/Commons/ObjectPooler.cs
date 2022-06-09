using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 싱글톤으로 구현, 일단은 투사체만...
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler m_instance;

    private GameObject m_pooling_obj_prefab;

    Queue<Projectile> m_pools;

    private void Awake()
    {
        m_instance = this;
        m_pools = new Queue<Projectile>();
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
        var new_obj = Instantiate(m_pooling_obj_prefab).GetComponent<Projectile>();
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
            var newObj = m_instance.CreateObj();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Projectile obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(m_instance.transform);
        m_instance.m_pools.Enqueue(obj);
    }
}
