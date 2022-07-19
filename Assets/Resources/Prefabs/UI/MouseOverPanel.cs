using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MouseOverPanel : MonoBehaviour
{
    [SerializeField] Image m_panel;
    SaveState save_state;
    RaycastHit2D m_hit;

    public void JsonLoader()
    {
        save_state = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").transform.name);
    }
    public void MouseEnter()//객체 들어가는지 검사.
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
        yield return null;
        m_hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 300.0f);
        if (m_hit.transform.tag == "item")
        {
            //스프라이트 추가 필
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", save_state.item_info[int.Parse(m_hit.transform.name)].m_name);
        }
        else if(m_hit.transform.tag == "Character")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", save_state.chanllenge_info[int.Parse(m_hit.transform.name)].m_name);
        }
        else if(m_hit.transform.tag == "Stage")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", save_state.stage_info[int.Parse(m_hit.transform.name)].m_name);
        }
        else if (m_hit.transform.tag == "Challenge")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("이름 : {0}", save_state.chanllenge_info[int.Parse(m_hit.transform.name)].m_name);
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
