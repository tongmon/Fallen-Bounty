using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveStateComponent : StateComponent
{
    public HeroMoveStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        Enter();
    }

    public override void Update()
    {
        if (((Hero)m_data).m_target_enemy)
        {
            
        }
        else
        {

        }
    }

    public override void Enter()
    {
        // ������ �� �����ؾ� �ϴ� ��������Ʈ �ٲٰų�... �����̱� ������ �� �����ؾ��ϴ� �͵� ��� ���⼭ ����
    }
}
