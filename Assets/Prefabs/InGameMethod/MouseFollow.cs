using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{   //ĳ��Ƽ ��ĥ�� ���ÿ��� �ذ� �ʿ� -- ���̵�� ���콺������ ���� �ʻ�ȭ�� ���ð���
    public Vector2 vec;
    public bool path_select = false; //��뼱�� ����
    public GameObject m_focus_object;
    public GameObject m_focus_enemy;
    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;
    }
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư
        {
            if(m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ŭ���� ���콺��ġ�� Ray����
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.transform.tag != "Enemy")//���� �ƴѰŸ� ����
            {
                m_focus_object = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //�̵��ϸ� ���� ����� 
            }
        }
        else if (Input.GetMouseButtonDown(1)) //���콺 ������ ��ư
        {
            if(m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            path_select = true;
            if (hit.collider != null) //�� ���ý�
            {
                m_focus_enemy = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (path_select) //�װų� ���̻������ ��� ���� �ؾ���
        {
            if(hit.collider != null) vec = hit.transform.position;
            if (vec.x > 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, vec, Time.deltaTime * 0.5f);
        }
    }
}
