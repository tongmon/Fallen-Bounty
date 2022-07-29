using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsComponent
{
    public object m_data;
    public SpriteRenderer m_main_sprite;

    public GraphicsComponent(GameObject gameobject)
    {
        m_main_sprite = gameobject.GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        
    }

    public virtual void OnWalkInPool(Field pool)
    {

    }
}
