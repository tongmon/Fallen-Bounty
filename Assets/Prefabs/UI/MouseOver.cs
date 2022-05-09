using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    [SerializeField] Image m_panel;
    
    private void OnMouseOver()
    {
        m_panel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //현재객체의 정보를 패널 텍스트로 바꿔야함.
    }
}
