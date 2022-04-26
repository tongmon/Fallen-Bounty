using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseFollow : MonoBehaviour
{
    public Vector2 vec;
    public bool path_select = false; //상대선택 유무
    public GameObject m_focus_object;
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //클릭한 마우스위치의 Ray저장
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.transform.tag != "Enemy")
            {
                m_focus_object = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //이동하면 색이 사라짐
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0.2f, 1.5f);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Physics.Raycast(ray, out hit);
            path_select = true;
            if (hit.collider != null) //적 선택시
            {
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().DOFade(0.2f, 1.5f);
            }
        }
        if (path_select) //죽거나 적이사라지면 경로 삭제 해야함
        {
            m_focus_object.transform.LookAt(vec);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, vec, Time.deltaTime * 0.5f);
        }
    }
}
