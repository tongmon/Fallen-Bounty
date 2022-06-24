using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent
{
    // ���� ��ü
    public Rigidbody2D m_rigidbody;

    // ����ü�� ���ϴ� ����
    public Vector2 m_vec_direction;

    public PhysicsComponent(GameObject gameobject)
    {
        m_rigidbody = gameobject.GetComponent<Rigidbody2D>();
        m_vec_direction = new Vector2();
    }

    public virtual void Update()
    {

    }
}
