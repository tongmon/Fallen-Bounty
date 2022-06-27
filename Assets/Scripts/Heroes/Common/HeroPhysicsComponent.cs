using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPhysicsComponent : PhysicsComponent
{
    public HeroPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();
    }

    public override void Update()
    {
        base.Update();

        // 영웅 이동 속도 더해주기
        m_rigidbody.velocity += ((Hero)m_data).m_vec_direction.normalized * 
            new Vector2(((HeroData)((Hero)m_data).m_data).x_velocity, ((HeroData)((Hero)m_data).m_data).y_velocity);
    }
}
