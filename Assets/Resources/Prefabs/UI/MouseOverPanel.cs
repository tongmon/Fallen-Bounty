using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MouseOverPanel : MonoBehaviour
{
    //등장할 패널
    [SerializeField] Image m_panel;

    //로그 캔버스
    [SerializeField] Canvas m_log_canvas;

    //캐릭터 선택 캔버스
    [SerializeField] Canvas m_charSelect_canvas;

    //Json으로 정보저장.
    [SerializeField] ItemInfo[] item_info;

    [SerializeField] StageInfo[] stage_info;

    [SerializeField] ChallengeInfo[] challenge_info;

    private Hero m_hero;

    //캔버스용 레이
    private GraphicRaycaster[] m_gr;

    //포인터 이벤트
    private PointerEventData m_ped;

    private void Start()
    {
        m_gr = new GraphicRaycaster[2];
        m_gr[0] = m_log_canvas.GetComponent<GraphicRaycaster>();
        m_gr[1] = m_charSelect_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);
    }
    public void MouseEnter()
    {
        //if (!EventSystem.current.currentSelectedGameObject.GetComponent<Hero>())
            //m_hero = EventSystem.current.currentSelectedGameObject.GetComponent<Hero>();
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
        if (m_log_canvas.gameObject.activeSelf) //로그에서의 동작
        {
            m_gr[0].Raycast(m_ped, results);
            if (results[0].gameObject.tag == "Item")
            {
                //스프라이트 추가 필 m_panel.transform.GetChild(0).GetComponent<Image>().sprite = item_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("피해량 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_damage);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("범위 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_range);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("시전시간 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_duration);
                m_panel.transform.GetChild(6).GetComponent<Text>().text = string.Format("쿨타임 : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_cooltime);
            }
            else if (results[0].gameObject.tag == "Character")
            {   //히어로에서 따와야함
                //m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform..name)].m_name);
            }
            else if (results[0].gameObject.tag == "Stage")
            {
                //스프라이트 추가 필 m_panel.transform.GetChild(0).GetComponent<Image>().sprite = stage_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("보스 이름 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_boss_name);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("클리어 시간 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_time);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("클리어 횟수 : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_count);
            }
            else if (results[0].gameObject.tag == "Challenge")
            {
                //스프라이트 추가 필m_panel.transform.GetChild(0).GetComponent<Image>().sprite = challenge_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("정보 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("해금 조건 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("획득량 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("목표량 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("달성률 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num / challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
            }
        }
        else
        {
            m_gr[1].Raycast(m_ped, results);//여기서 오류남
            if (results[0].gameObject.tag == "Skill")//캐릭터 선택창에서
            {
                m_panel.transform.GetChild(0).GetComponent<Image>().sprite = m_hero.m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", m_hero.abilities[int.Parse(results[0].gameObject.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("물리 계수 : {0}", m_hero.abilities[int.Parse(results[0].gameObject.name)].m_base_phhsical_coefficient);
                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("마법 계수 : {0}", m_hero.abilities[int.Parse(results[0].gameObject.name)].m_base_magic_coefficient);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("범위 : {0}", m_hero.abilities[int.Parse(results[0].gameObject.name)].m_base_range);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("쿨타임 : {0}", m_hero.abilities[int.Parse(results[0].gameObject.name)].m_base_cooldown_time);
                
            }
        }
        m_panel.gameObject.SetActive(true);
        m_panel.DOColor(Color.white, 0.1f);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_panel.transform.position = pos * Camera.main.transform.localScale;
        m_panel.transform.localPosition = new Vector3(m_panel.transform.localPosition.x + 350, m_panel.transform.localPosition.y - 175, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.DOKill();//두트윈 끄기
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
        m_panel.DOKill();
    }
}
