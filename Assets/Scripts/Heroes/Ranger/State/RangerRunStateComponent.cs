using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerRunStateComponent : StateComponent
{
    public RangerRunStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();

        Enter();
    }

    public override void Update()
    {
        var data = (Ranger)m_data;

        float distance_to_target = Vector2.Distance(
            data.m_target ? data.m_physics_component.GetPosition() : ((HeroPhysicsComponent)data.m_physics_component).GetBottom(),
            data.m_target ? data.m_target.m_physics_component.GetPosition() : data.m_point_target.Value
            );

        float move_gap = Time.deltaTime * ((HeroPhysicsComponent)data.m_physics_component).m_move_velocity.magnitude;

        // Ÿ���õ� ���� �ִ� ���
        if (data.m_target && data.m_target is Enemy)
        {
            // ���� ��ġ�� ��Ÿ��� ���� ����
            if (distance_to_target >= ((HeroData)data.m_data).ranged_range)
            {
                data.m_vec_direction = data.m_target.m_physics_component.GetPosition() - data.m_physics_component.GetPosition();
            }
            else
            {
                // Ÿ���õ� ���� �ִµ� �÷��̾ �̵��� ��Ŵ
                if (Mathf.Round(distance_to_target) > Mathf.Round(move_gap))
                    data.m_vec_direction = data.m_point_target.Value - ((HeroPhysicsComponent)data.m_physics_component).GetBottom();
                else
                    data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
            }
        }
        else
        {
            if (Mathf.Round(distance_to_target) > Mathf.Round(move_gap))
            {
                data.m_vec_direction = data.m_point_target.Value - ((HeroPhysicsComponent)data.m_physics_component).GetBottom();
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
