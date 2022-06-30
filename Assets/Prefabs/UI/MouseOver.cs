using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    [SerializeField] GameObject m_panel;
    RaycastHit2D mouse_pos;

    private void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if(mouse_pos.collider != null)
        {
            Debug.Log("yes");
        }
    }


}
