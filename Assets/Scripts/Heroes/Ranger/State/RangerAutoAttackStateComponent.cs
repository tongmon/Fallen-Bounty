using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 궁수 공격 상태 설정
public class RangerAutoAttackStateComponent : StateComponent
{
    public RangerAutoAttackStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Ranger>();

        Enter();
    }

    public override void Update()
    {
        var data = (Ranger)m_data;

        data.m_cur_attack_cooltime -= Time.deltaTime;

        if (!data.m_target)
            return;

        float distance = Vector2.Distance(data.m_target.m_physics_component.GetPosition(), data.m_physics_component.GetPosition());

        // 공격 쿨타임이 되었고 사거리 내에 적이 있다면 투사체 발사
        if (data.m_cur_attack_cooltime < 0 && distance <= ((RangerData)data.m_data).ranged_range)
        {
            data.m_cur_attack_cooltime = ((RangerData)data.m_data).attack_cooltime;
            Arrow arrow = (Arrow)ProjectilePool.GetObj(((RangerData)data.m_data).projectile_type);

            // 총알 나가는 시작점 결정, 추후 수정
            arrow.SetPosition(data.m_physics_component.GetPosition());

            arrow.m_target = data.m_target;
            arrow.m_shooter = data;

            arrow.Shoot(((RangerData)data.m_data).arrow_velocity);

            // 밑이 더 효율적이나 방향 문제있음, 추후수정
            // arrow.Shoot(data, data.m_target, ((RangerData)data.m_data).arrow_velocity);
        }
    }

    public override void Enter()
    {

    }
}
