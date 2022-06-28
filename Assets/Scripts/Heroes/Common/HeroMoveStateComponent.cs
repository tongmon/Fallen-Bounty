using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 움직임 방향 벡터만 지정해줌
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

        // 타겟팅된 적이 있는 경우
        if ((Enemy)data.m_target) 
        {
            // 원거리 캐릭터
            if (((HeroData)data.m_data).melee_range < 0)
            {
                // 적의 위치가 사거리와 맞지 않음
                if (distance_to_target >= ((HeroData)data.m_data).ranged_range)
                {
                    data.m_vec_direction = data.m_target.m_physics_component.GetPosition() - data.m_physics_component.GetPosition();
                }
                else
                {
                    // 적이 사거리 내에 들어와 있다면 idle 상태로 변경
                    data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
                }
            }
            // 근접 캐릭터
            else
            {
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - ((HeroData)data.m_data).melee_range) > 0.05f)
                {
                    // 적이 사거리 안에 없는 경우
                    if (distance_to_target >= ((HeroData)data.m_data).melee_range)
                        data.m_vec_direction = data.m_target.m_physics_component.GetPosition() - data.m_physics_component.GetPosition();
                    // 적이 사거리보다 가까운 경우
                    else
                        data.m_vec_direction = data.m_physics_component.GetPosition() - data.m_target.m_physics_component.GetPosition();
                }
                else
                {
                    // 다음 상태로 넘어가게 하거나 아니면 바로 로테이션 시키던가 해야됨
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
        // 움직이기 시작할 때 변경해야하는 것들 모두 여기서 변경
    }
}
