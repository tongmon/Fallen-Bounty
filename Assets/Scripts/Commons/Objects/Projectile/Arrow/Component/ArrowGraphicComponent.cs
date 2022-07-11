using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGraphicComponent : GraphicsComponent
{
    public float m_arrow_alpha;
    public SpriteRenderer m_arrow_sprite;

    public ArrowGraphicComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Arrow>();
        m_arrow_sprite = gameobject.GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        OnDrawArrow();
    }

    // 일단은 쏴질 때만 보이게...
    void OnDrawArrow()
    {
        float alpha;
        if (((Arrow)m_data).m_shooted)
        {
            alpha = 255.0f;
        }
        else
        {
            alpha = 0.0f;
        }

        m_arrow_sprite.color = new Color(m_arrow_sprite.color.r, m_arrow_sprite.color.g, m_arrow_sprite.color.b, alpha);
    }
}
