using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ü� ���� ���� ����
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

        float distance = Vector2.Distance(data.m_target.m_physics_component.GetPosition(), data.m_physics_component.GetPosition());

        // ���� ��Ÿ���� �Ǿ��� ��Ÿ� ���� ���� �ִٸ� ����ü �߻�
        if (data.m_cur_attack_cooltime < 0 && distance <= ((RangerData)data.m_data).ranged_range)
        {
            data.m_cur_attack_cooltime = ((RangerData)data.m_data).attack_cooltime;
            Arrow arrow = (Arrow)ProjectilePool.GetObj(((RangerData)data.m_data).projectile_type);

            // �Ѿ� ������ ������ ����, ���� ����
            arrow.SetPosition(data.m_target.m_physics_component.GetPosition());

            arrow.m_target = data.m_target;
            arrow.m_shooter = data;

            arrow.Shoot(((RangerData)data.m_data).arrow_velocity);
        }
    }

    public override void Enter()
    {

    }
}
