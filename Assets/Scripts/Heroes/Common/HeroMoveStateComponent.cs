using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveStateComponent : StateComponent
{
    Hero m_data;
    public HeroMoveStateComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        Enter();
    }

    public override void Update()
    {
        // m_data.
    }

    public override void Enter()
    {
        // ������ �� �����ؾ� �ϴ� ��������Ʈ �ٲٰų�... �����̱� ������ �� �����ؾ��ϴ� �͵� ��� ���⼭ ����
    }
}
