using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 궁수 공격 상태 설정
public class BerserkerAutoAttackStateComponent : StateComponent
{
    public BerserkerAutoAttackStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Berserker>();

        Enter();
    }

    public override void Update()
    {
        var data = (Berserker)m_data;

        data.m_cur_attack_cooltime -= Time.deltaTime;

        if (!data.m_target)
            return;

        float distance = Vector2.Distance(data.m_target.m_physics_component.m_position, data.m_physics_component.m_position);

        // 공격 쿨타임이 되었고 사거리 내에 적이 있다면 투사체 발사
        if (data.m_cur_attack_cooltime < 0 && distance <= (data.gameObject.GetComponent<Berserker>().berserker_data.ranged_range))
        {
            data.m_cur_attack_cooltime = data.gameObject.GetComponent<Berserker>().berserker_data.attack_cooltime;
        }
    }

    public override void Enter()
    {

    }
}
