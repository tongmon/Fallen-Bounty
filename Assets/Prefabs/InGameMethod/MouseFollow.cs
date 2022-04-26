using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseFollow : MonoBehaviour
{
    public Vector2 vec;
    public bool path_select = false; //��뼱�� ����
    public GameObject m_focus_object;
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ŭ���� ���콺��ġ�� Ray����
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.transform.tag != "Enemy")
            {
                m_focus_object = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //�̵��ϸ� ���� �����
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0.2f, 1.5f);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Physics.Raycast(ray, out hit);
            path_select = true;
            if (hit.collider != null) //�� ���ý�
            {
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0.2f, 1.5f);
            }
        }
        if (path_select) //�װų� ���̻������ ��� ���� �ؾ���
        {
            m_focus_object.transform.LookAt(vec);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, vec, Time.deltaTime * 0.5f);
        }
    }
}
