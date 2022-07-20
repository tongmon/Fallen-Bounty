using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MouseOverPanel : MonoBehaviour
{
    [SerializeField] Image m_panel;

    [SerializeField] Canvas m_log_canvas;
    [SerializeField] Canvas m_charSelect_canvas;

    [SerializeField] ItemInfo[] item_info;
    [SerializeField] StageInfo[] stage_info;
    [SerializeField] ChallengeInfo[] challenge_info;

    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    private void Start()
    {
        m_gr = m_log_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);
    }
    public void MouseEnter()
    {
        StopCoroutine("MOut");
        StartCoroutine("MIn");
    }
    
    public void MouseOut()
    {
        StopCoroutine("MIn");
        StartCoroutine("MOut");
    }

    IEnumerator MIn()
    {
        for (int i = 0; i < m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(true);
        }
        yield return null;
        m_ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_gr.Raycast(m_ped, results);
        if (m_log_canvas.gameObject.activeSelf) //로그에서의 동작
        {
            if (results[1].gameObject.transform.tag == "Item")
            {
                //스프라이트 추가 필 m_panel.transform.GetChild(0).GetComponent<Image>().sprite = item_info[int.Parse(results[1].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("피해량 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_damage);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("범위 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_range);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("시전시간 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_duration);
                m_panel.transform.GetChild(6).GetComponent<Text>().text = string.Format("쿨타임 : {0}", item_info[int.Parse(results[1].gameObject.transform.name)].m_cooltime);
            }
            else if (results[1].gameObject.transform.tag == "Character")
            {   //히어로에서 따와야함
                //m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform..name)].m_name);
            }
            else if (results[1].gameObject.transform.tag == "Stage")
            {
                //스프라이트 추가 필 m_panel.transform.GetChild(0).GetComponent<Image>().sprite = stage_info[int.Parse(results[1].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("보스 이름 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_boss_name);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("클리어 시간 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_time);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("클리어 횟수 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_count);
            }
            else if (results[1].gameObject.transform.tag == "Challenge")
            {
                //스프라이트 추가 필m_panel.transform.GetChild(0).GetComponent<Image>().sprite = challenge_info[int.Parse(results[1].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("획득량 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("목표량 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("달성률 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num / challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
            }
        }
        else//캐릭터 옵션 가져와야함
        {

        }
        m_panel.gameObject.SetActive(true);
        m_panel.DOColor(Color.white, 0.1f);

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_panel.transform.position = pos * Camera.main.transform.localScale;
        m_panel.transform.localPosition = new Vector3(m_panel.transform.localPosition.x + 350, m_panel.transform.localPosition.y - 175, 0);
    }
    IEnumerator MOut()
    {
        for(int i= 0; i< m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(false);
        }
        m_panel.DOFade(0, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.gameObject.SetActive(false);
        for (int i = 0; i < m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
