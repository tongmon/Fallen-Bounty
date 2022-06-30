using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGraphicsComponent : EnemyGraphicsComponent
{
    public GoblinGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Goblin>();
    }

    protected override void OnDrawSelectedSprite()
    {
        var data = (Goblin)m_data;

        m_seleted_sprite.color = new Color(255, 255, 255, m_seleted_sprite_alpha);
    }
}
