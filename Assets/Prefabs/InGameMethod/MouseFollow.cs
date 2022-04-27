using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseFollow : MonoBehaviour
{   //ĳ��Ƽ ��ĥ�� ���ÿ��� �ذ� �ʿ� -- ���̵�� ���콺������ ���� �ʻ�ȭ�� ���ð���
    public bool m_path_select;
    public Vector2 m_vec;
    public GameObject m_focus_object;
    public GameObject m_focus_enemy;
    public RaycastHit m_hit;

    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��ư
        {
            if(m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Ŭ���� ���콺��ġ�� Ray����
            Physics.Raycast(m_ray, out m_hit);
            if (m_hit.collider != null && m_hit.transform.tag != "Enemy")//���� �ƴѰŸ� ����
            {
                m_focus_object = m_hit.collider.gameObject;
                m_hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //�̵��ϸ� ���� ����� 
            }
        }
        else if (Input.GetMouseButtonDown(1)) //���콺 ������ ��ư
        {
            if(m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(m_ray, out m_hit);
            m_path_select = true;
            if (m_hit.collider != null && m_hit.collider.tag == "Enemy") //�� ���ý�
            {
                m_focus_enemy = m_hit.collider.gameObject;
                m_hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else m_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (m_path_select) //�װų� ���̻������ ��� ���� �ؾ���
        {
            if(m_hit.collider != null) m_vec = m_hit.transform.position;
            if (m_vec.x > 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, m_vec, Time.deltaTime * 0.5f);
        }
    }
}
