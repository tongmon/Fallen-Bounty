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
        float distance_to_target = Vector2.Distance(
            data.m_target ? data.m_physics_component.GetPosition() : ((HeroPhysicsComponent)data.m_physics_component).GetBottom(),
            data.m_target ? data.m_target.m_physics_component.GetPosition() : data.m_point_target.Value
            );
        float move_gap = 10 * Vector2.Distance(Vector2.zero, Time.deltaTime * ((HeroPhysicsComponent)data.m_physics_component).m_move_velocity * data.m_vec_direction.normalized);

        // 타겟팅된 적이 있는 경우
        if (data.m_target && data.m_target is Enemy) 
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
                    // 적은 타겟팅 되었는데 플레이어가 땅을 찍음 
                    if (data.m_point_target != null)
                    {
                        data.m_vec_direction = data.m_point_target.Value - ((HeroPhysicsComponent)data.m_physics_component).GetBottom();
                    }
                    else
                    {
                        data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
                    }
                }
            }
            // 근접 캐릭터
            else
            {
                // 적의 위치가 사거리와 맞지 않음
                if (Mathf.Abs(distance_to_target - ((HeroData)data.m_data).melee_range) > move_gap)
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
                    List<Creature> creatures = ObjectObserver.Instance.m_group_by_target[data.m_target];
                    List<Hero> heroes = new List<Hero>();
                    Vector2 hero_pos = data.m_physics_component.GetPosition(), target_pos = data.m_target.m_physics_component.GetPosition();
                    bool is_right = true;
                    int cnt = -1, order = -1, st, end;
                    float angle = 0, angle_def;

                    // 현재 영웅이 타게팅한 적의 좌측에 있음
                    if (hero_pos.x < target_pos.x)
                    {
                        st = ObjectObserver.Instance.m_right_side_creature_index[data.m_target];
                        end = creatures.Count;
                        is_right = false;
                    }
                    else
                    {
                        st = 0;
                        end = ObjectObserver.Instance.m_right_side_creature_index[data.m_target];
                    }

                    for (int i = st; i < end; i++)
                    {
                        if (creatures[i] is Hero)
                        {
                            cnt++;
                            if (creatures[i].gameObject == data.gameObject)
                                order = cnt;
                        }
                    }

                    angle = Quaternion.FromToRotation(is_right ? Vector2.right : Vector2.left, hero_pos - target_pos).eulerAngles.z;
                    angle = Mathf.Min(360 - angle, angle);
                    angle_def = ObjectObserver.Instance.m_angle_def[cnt, order];

                    if (Mathf.Abs(Mathf.Abs(angle) - Mathf.Abs(angle_def)) >= 1.0f)
                    {
                        Vector2 dest_vec = Quaternion.Euler(0, 0, is_right ? angle_def : 180 - angle_def) * new Vector2(((HeroData)data.m_data).melee_range, 0);
                        data.m_vec_direction = target_pos - hero_pos + dest_vec;
                    }
                    else
                    {
                        // 적은 타겟팅 되었는데 플레이어가 땅을 찍음 
                        if (data.m_point_target != null)
                        {
                            data.m_vec_direction = data.m_point_target.Value - ((HeroPhysicsComponent)data.m_physics_component).GetBottom();
                        }
                        else
                        {
                            data.m_movement_state = new HeroIdleStateComponent(data.gameObject);
                        }
                    }
                }
            } 
        }
        else
        {
            if (distance_to_target > move_gap)
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
        // 움직이기 시작할 때 변경해야하는 것들 모두 여기서 변경
    }
}
