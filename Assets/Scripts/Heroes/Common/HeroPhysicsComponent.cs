using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    public Vector2 m_move_velocity;

    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_mass = ((HeroData)((Hero)m_data).m_data).mass;

        m_move_velocity = ((HeroData)((Hero)m_data).m_data).velocity;
    }

    public override void Update()
    {
        base.Update();

        // ������ ó��
        Vector2 friction = Vector2.zero; // ������ ũ��(������ ���ԵǸ� �ȵ�), �̷��� �ϸ� �ȵǰ� �ܺο��� ���;� ��
        friction *= -((Hero)m_data).m_vec_direction.normalized; // �������� �ݴ� �������� ����
        Vector2 accel = friction / m_mass;
        m_velocity += accel * Time.deltaTime;

        // ���� �̵� �ӵ� �����ֱ�
        m_velocity += ((Hero)m_data).m_vec_direction.normalized * m_move_velocity;
    }
}
