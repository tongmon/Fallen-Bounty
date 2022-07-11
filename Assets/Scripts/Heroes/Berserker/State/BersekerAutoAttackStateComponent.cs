using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BersekerAutoAttackStateComponent : StateComponent
{
    public BersekerAutoAttackStateComponent(GameObject gameObject) : base(gameObject)
    {
        m_data = gameObject.GetComponent<Berserker>();
        Enter();
    }

    // Update is called once per frame
   public override void Update()
    {
        var data = (Berserker)m_data;

        data.m_cur_attack_cooltime -= Time.deltaTime;

        if (!data.m_target)
            return;

        float distance = Vector2.Distance(data.m_target.m_physics_component.m_position, data.m_physics_component.m_position);

        if (data.m_cur_attack_cooltime < 0 && distance <= ((BerserkerData)data.m_data).ranged_range)
        {
            data.m_cur_attack_cooltime = ((BerserkerData)data.m_data).attack_cooltime;

            
        }
    }
    public override void Enter()
    {

    }
}
