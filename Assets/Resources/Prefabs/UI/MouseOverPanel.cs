using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MouseOverPanel : MonoBehaviour
{
    [SerializeField] Image m_panel;
    [SerializeField] Canvas m_canvas;

    [SerializeField] ItemInfo[] item_info;
    [SerializeField] StageInfo[] stage_info;
    [SerializeField] ChallengeInfo[] challenge_info;

    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    private void Start()
    {
        m_gr = m_canvas.GetComponent<GraphicRaycaster>();
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
        yield return new WaitForSecondsRealtime(0);
        m_ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_gr.Raycast(m_ped, results);
        if (results[0].gameObject.transform.parent.transform.tag == "Item")
        {
            //스프라이트 추가 필
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", item_info[int.Parse(results[0].gameObject.transform.parent.name)].m_name);
        }   
        else if(results[0].gameObject.transform.parent.transform.tag == "Character")
        {
            //m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.parent.name)].m_name);
        }
        else if(results[0].gameObject.transform.parent.transform.tag == "Stage")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", stage_info[int.Parse(results[0].gameObject.transform.parent.name)].m_name);
        }
        else if (results[0].gameObject.transform.parent.transform.tag == "Challenge")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.parent.name)].m_name);
        }
        m_panel.gameObject.SetActive(true);
        m_panel.DOColor(Color.white, 0.1f);

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_panel.transform.position = pos * Camera.main.transform.localScale;
        m_panel.transform.localPosition = new Vector3(m_panel.transform.localPosition.x + 350, m_panel.transform.localPosition.y - 175, 0);
    }
    IEnumerator MOut()
    {
        m_panel.DOFade(0, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.gameObject.SetActive(false);
        StopCoroutine(MOut());
    }
}
