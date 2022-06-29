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

        if(hit.collider != null &&hit.collider.tag != "Button") //�ݸ��� �ʿ���
        {
            OnMouseOver();
        }
    }

    private void OnMouseEnter() //������ü�� �����ؾ���
    {
        m_panel.SetActive(true);
    }

    private void OnMouseOver()
    {
        m_panel.SetActive(true);
        m_panel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(150, -200, 0);
        //���簴ü�� ������ �г� �ؽ�Ʈ�� �ٲ����.
    }
    private void OnMouseExit()
    {
        m_panel.SetActive(false);
    }
}
