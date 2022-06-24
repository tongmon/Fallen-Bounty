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
        // 움직일 때 변경해야 하는 스프라이트 바꾸거나... 움직이기 시작할 때 변경해야하는 것들 모두 여기서 변경
    }
}
