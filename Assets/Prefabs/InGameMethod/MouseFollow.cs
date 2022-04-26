using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{   //캐릭티 겹칠시 선택오류 해결 필요 -- 아이디어 마우스오버시 둘의 초상화로 선택가능
    public Vector2 vec;
    public bool path_select = false; //상대선택 유무
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
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
        {
            if(m_focus_object != null) m_focus_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //클릭한 마우스위치의 Ray저장
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.transform.tag != "Enemy")//적이 아닌거만 선택
            {
                m_focus_object = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); //이동하면 색이 사라짐 
            }
        }
        else if (Input.GetMouseButtonDown(1)) //마우스 오른쪽 버튼
        {
            if(m_focus_enemy != null) m_focus_enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            path_select = true;
            if (hit.collider != null) //적 선택시
            {
                m_focus_enemy = hit.collider.gameObject;
                hit.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
            else vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (path_select) //죽거나 적이사라지면 경로 삭제 해야함
        {
            if(hit.collider != null) vec = hit.transform.position;
            if (vec.x > 0) m_focus_object.transform.rotation = Quaternion.Euler(0, 0, 0);
            else m_focus_object.transform.rotation = Quaternion.Euler(0, 180, 0);
            m_focus_object.transform.position = Vector2.Lerp(m_focus_object.transform.position, vec, Time.deltaTime * 0.5f);
        }
    }
}
