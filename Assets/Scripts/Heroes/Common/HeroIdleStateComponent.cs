using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleStateComponent : StateComponent
{
    public HeroIdleStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        Enter();
    }

    public override void Update()
    {
        var data = (Hero)m_data;
        data.m_vec_direction = Vector2.zero;
    }

    public override void Enter()
    {
        // idle ���� ������ �� �����ؾ��ϴ� �͵� ��� ���⼭ ����
    }
}
