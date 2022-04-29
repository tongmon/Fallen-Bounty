using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{   
    private bool m_path_select;
    private Vector2 m_vec;
    private GameObject m_focus_object; //포커스만 공용화
    private GameObject m_focus_enemy;
    private RaycastHit m_hit1,m_hit2;

    LineRenderer m_lr;
    private void Start()
    {
        m_focus_object = null;
        m_focus_enemy = null;
        m_lr = GetComponent<LineRenderer>();
        m_lr.startWidth = 0.05f;
        m_lr.endWidth = 0.05f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 좌 클릭
        {
            if (m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0); //색 없애기
            Ray m_ray1 = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Physics.Raycast(m_ray1, out m_hit1);
            if (m_hit1.collider != null && m_hit1.collider.gameObject == gameObject && m_hit1.collider.transform.tag != "Enemy") //자기자신 선택하면
            {
                m_path_select = false;
                m_focus_object = gameObject;
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }
        else if (Input.GetMouseButtonDown(1) && m_hit1.collider.gameObject == gameObject) //마우스 우 클릭
        {
            if (m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray m_ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(m_ray2, out m_hit2);
            if(m_focus_object == gameObject) m_path_select = true;  //포커스된 애가 내가 맞으면
            if (m_hit2.collider != null)
            {
                m_focus_enemy = m_hit2.collider.gameObject;
                m_hit2.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else //땅 클릭
            {
                m_vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_focus_enemy = null;
            }
        }
        if (m_path_select)
        {
            if (m_hit2.collider != null) m_vec = m_hit2.transform.position;
            if (m_vec.x > 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, m_vec, Time.deltaTime * 0.5f);
            m_lr.SetPosition(0, m_focus_object.transform.position);
            if (m_hit2.collider != null && m_focus_enemy != null)
            {
                m_lr.SetPosition(1, m_focus_enemy.transform.GetChild(0).transform.position);
            }
            else
            {
                m_lr.SetPosition(1, m_vec);
            }
        }
    }
}
