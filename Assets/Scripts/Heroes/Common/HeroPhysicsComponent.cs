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
    }

    public override void FixedUpdate()
    {
        #region ���� �̵� �ӵ� ����(�̵� �ӵ���...)
        // ������ ���� �ϴ� �������� ���� �ӵ��� �ָ� ��� ���� �ӵ��� �̵� �ӵ��� �������� ���� ���� (���� �� �̵� �Ϸ��� ���������� ũ�⸦ �����Ѵٰ� ���� ��.)
        Vector2 applied_movement = m_velocity * ((Hero)m_data).m_vec_direction.normalized;
        float x = m_velocity.x, y = m_velocity.y;

        if (Mathf.Abs(applied_movement.x) < Mathf.Abs(((Hero)m_data).m_vec_direction.x * m_move_velocity.x))
            x = ((Hero)m_data).m_vec_direction.normalized.x * m_move_velocity.x;

        if(Mathf.Abs(applied_movement.y) < Mathf.Abs(((Hero)m_data).m_vec_direction.y * m_move_velocity.y))
            y = ((Hero)m_data).m_vec_direction.normalized.y * m_move_velocity.y;

        m_velocity = new Vector2(x, y);
        #endregion

        #region ������ ó��(��� �̵��ӵ��� ó���� �Ŀ� �������� ó�� �ؾ� ��)
        Vector2 friction = new Vector2(200, 200); // m_affected_friction; // ������ ũ��(������ ���ԵǸ� �ȵ�), �̷��� �ϸ� �ȵǰ� �ܺο��� ���;� ��
        friction *= -m_velocity.normalized; // �������� �������� �ӵ��� �ݴ� �������� ����
        Vector2 accel = friction / m_mass;
        Vector2 friction_velocity = m_velocity + accel * Time.deltaTime;

        if (m_velocity.x * friction_velocity.x <= 0 && m_velocity.y * friction_velocity.y <= 0)
            m_velocity = Vector2.zero;
        else
            m_velocity = friction_velocity;
        #endregion
    }
}
