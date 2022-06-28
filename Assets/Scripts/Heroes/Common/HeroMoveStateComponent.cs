using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� ���͸� ��������
public class HeroMoveStateComponent : StateComponent
{
    public HeroMoveStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        Enter();
    }

    public override void Update()
    {
        var data = (Hero)m_data;
        float distance_to_target = Vector2.Distance(data.m_physics_component.GetPosition(), data.m_target ? data.m_target.m_physics_component.GetPosition() : data.m_point_target);

        // Ÿ���õ� ���� �ִ� ���
        if ((Enemy)data.m_target) 
        {
            // ���Ÿ� ĳ����
            if (((HeroData)data.m_data).melee_range < 0)
            {
                // ���� ��ġ�� ��Ÿ��� ���� ����
                if (distance_to_target >= ((HeroData)data.m_data).ranged_range)
                {
                    data.m_vec_direction = data.m_target.m_physics_component.GetPosition() - data.m_physics_component.GetPosition();
                }
                else
                {
                    // ���� ��Ÿ� ���� ���� �ִٸ� idle ���·� ����
                    data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
                }
            }
            // ���� ĳ����
            else
            {
                // ���� ��ġ�� ��Ÿ��� ���� ����
                if (Mathf.Abs(distance_to_target - ((HeroData)data.m_data).melee_range) > 0.05f)
                {
                    // ���� ��Ÿ� �ȿ� ���� ���
                    if (distance_to_target >= ((HeroData)data.m_data).melee_range)
                        data.m_vec_direction = data.m_target.m_physics_component.GetPosition() - data.m_physics_component.GetPosition();
                    // ���� ��Ÿ����� ����� ���
                    else
                        data.m_vec_direction = data.m_physics_component.GetPosition() - data.m_target.m_physics_component.GetPosition();
                }
                else
                {
                    // ���� ���·� �Ѿ�� �ϰų� �ƴϸ� �ٷ� �����̼� ��Ű���� �ؾߵ�
                }
            } 
        }
        else
        {
            if (distance_to_target > 0.05f)
            {
                data.m_vec_direction = data.m_point_target - data.m_physics_component.GetPosition();
            }
            else
            {
                data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
            }
        }
    }

    public override void Enter()
    {
        // �����̱� ������ �� �����ؾ��ϴ� �͵� ��� ���⼭ ����
    }
}
