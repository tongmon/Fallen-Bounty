using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputComponent : InputComponent
{
    public EnemyInputComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Enemy>();
    }

    protected override void OnMouseLeftDown()
    {
        base.OnMouseLeftDown();

        if (m_mouse_hit.collider && m_mouse_hit.collider.gameObject == ((Enemy)m_data).gameObject)
            ((Enemy)m_data).m_selected = true;
        else
            ((Enemy)m_data).m_selected = false;
    }

    // ±×·¡ÇÈ ÄÄÆ÷³ÍÆ®¶û Ä¿ÇÃ¸µ µÇ¾î ÀÖ´Âµ¥ ³öµÖµµ µÇÁö¸¸... ¸Õ°¡ ²¬²ô·¯¿ò
    protected override void OnMouseLeftDrag()
    {
        base.OnMouseLeftDrag();
    }

    protected override void OnMouseLeftUp()
    {
        base.OnMouseLeftUp();
    }

    protected override void OnMouseRightDown()
    {
        base.OnMouseRightDown();
    }

    protected override void OnMouseRightDrag()
    {
        base.OnMouseRightDrag();
    }

    protected override void OnMouseRightUp()
    {
        base.OnMouseRightUp();
    }
}
