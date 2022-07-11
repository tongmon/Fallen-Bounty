using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü ������Ʈ Ǯ�� �ý���
public class ProjectilePool
{
    public static ProjectilePool m_inst;

    private Dictionary<string, GameObject> m_pooling_prefab;

    Dictionary<string, Queue<Projectile>> m_pools;

    ProjectilePool()
    {
        m_pooling_prefab = new Dictionary<string, GameObject>();
        m_pools = new Dictionary<string, Queue<Projectile>>();
    }

    public static ProjectilePool Instance
    {
        get
        {
            if (null == m_inst)
            {
                //���� �ν��Ͻ��� ���ٸ� �ϳ� �����ؼ� �־��ش�.
                m_inst = new ProjectilePool();
            }
            return m_inst;
        }
    }

    Projectile CreateObj(string projectile_name)
    {
        // ���� ���� ���߿� ������Ʈ �����ϴ� �Լ� -> Instantiate
        var new_obj = Object.Instantiate(Instance.m_pooling_prefab[projectile_name]).GetComponent<Projectile>();
        new_obj.gameObject.SetActive(false);
        return new_obj;
    }

    public static void InitPool(string projectile_name, int pool_num)
    {
        if (!Instance.m_pooling_prefab.TryGetValue(projectile_name, out GameObject tmp))
        {
            Instance.m_pooling_prefab[projectile_name] = Resources.Load<GameObject>("Prefabs/Object/Projectile/" + projectile_name); // GameObject.Find(projectile_name);
            Instance.m_pools[projectile_name] = new Queue<Projectile>();
        }

        for (int i = 0; i < pool_num; i++)
        {
            Instance.m_pools[projectile_name].Enqueue(Instance.CreateObj(projectile_name));
        }
    }

    public static Projectile GetObj(string projectile_name)
    {
        Projectile obj;

        if (Instance.m_pools[projectile_name].Count > 0)
            obj = Instance.m_pools[projectile_name].Dequeue();
        else
            obj = Instance.CreateObj(projectile_name);
        
        obj.gameObject.SetActive(true);
        obj.OnAwake();

        return obj;
    }

    public static void ReturnObject(string projectile_name, Projectile obj)
    {        
        obj.gameObject.SetActive(false);
        Instance.m_pools[projectile_name].Enqueue(obj);
    }
}
