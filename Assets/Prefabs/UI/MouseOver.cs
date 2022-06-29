using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{
    [SerializeField] GameObject m_panel;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if(hit.collider != null &&hit.collider.tag != "Button") //콜리더 필요함
        {
            OnMouseOver();
        }
    }

    private void OnMouseEnter() //정보객체랑 연결해야함
    {
        m_panel.SetActive(true);
    }

    private void OnMouseOver()
    {
        m_panel.SetActive(true);
        m_panel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(150, -200, 0);
        //현재객체의 정보를 패널 텍스트로 바꿔야함.
    }
    private void OnMouseExit()
    {
        m_panel.SetActive(false);
    }
}
