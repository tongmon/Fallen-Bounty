using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputComponent : InputComponent
{
    public EnemyInputComponent(GameObject gameobject) : base(gameobject)
    {

    }

    protected override void OnMouseLeftDown()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
    }

    // �׷��� ������Ʈ�� Ŀ�ø� �Ǿ� �ִµ� ���ֵ� ������... �հ� ��������
    protected override void OnMouseLeftDrag()
    {
        if (!Input.GetMouseButton(0))
            return;

        m_mouse_hold_time[0] += Time.deltaTime;
    }

    protected override void OnMouseLeftUp()
    {
        if (!Input.GetMouseButtonUp(0))
            return;

        
    }

    protected override void OnMouseRightDown()
    {

    }

    protected override void OnMouseRightDrag()
    {

    }

    protected override void OnMouseRightUp()
    {

    }
}
