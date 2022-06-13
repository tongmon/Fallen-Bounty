using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü ������Ʈ Ǯ�� �ý���
// �����غ��� ����ü���� ȭ�� ������Ʈ Ǯ������ �ٲ�� �� ����... dequeue�� �� ������ ��
public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool m_instance;

    private Dictionary<string, GameObject> m_pooling_prefab;

    Dictionary<string, Queue<Projectile>> m_pools;

    void Awake()
    {
        m_instance = this;
        m_pools = new Dictionary<string, Queue<Projectile>>();
        m_pooling_prefab = new Dictionary<string, GameObject>();
    }

    void Update()
    {
        
    }

    Projectile CreateObj(string projectile_name)
    {
        // ���� ���� ���߿� ������Ʈ �����ϴ� �Լ� -> Instantiate
        var new_obj = Instantiate(m_pooling_prefab[projectile_name]).GetComponent<Projectile>();
        new_obj.gameObject.SetActive(false);
        new_obj.transform.SetParent(transform);
        return new_obj;
    }

    public static void InitPool(string projectile_name, int pool_num)
    {
        m_instance.m_pooling_prefab[projectile_name] = GameObject.Find(projectile_name);
        m_instance.m_pools[projectile_name] = new Queue<Projectile>();
        for (int i = 0; i < pool_num; i++)
        {
            m_instance.m_pools[projectile_name].Enqueue(m_instance.CreateObj(projectile_name));
        }
    }

    public static Projectile GetObj(string projectile_name)
    {
        if (m_instance.m_pools.Count > 0)
        {
            var obj = m_instance.m_pools[projectile_name].Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var new_obj = m_instance.CreateObj(projectile_name);
            new_obj.gameObject.SetActive(true);
            new_obj.transform.SetParent(null);
            return new_obj;
        }
    }

    public static void ReturnObject(string projectile_name, Projectile obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(m_instance.transform);
        m_instance.m_pools[projectile_name].Enqueue(obj);
    }
}
