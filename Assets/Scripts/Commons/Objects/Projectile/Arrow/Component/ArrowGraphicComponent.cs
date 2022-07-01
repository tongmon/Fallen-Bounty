using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGraphicComponent : GraphicsComponent
{
    public ArrowGraphicComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Arrow>();
    }

    public override void Update()
    {
        base.Update();

        OnDrawArrow();
    }

    void OnDrawArrow()
    {

    }
}
