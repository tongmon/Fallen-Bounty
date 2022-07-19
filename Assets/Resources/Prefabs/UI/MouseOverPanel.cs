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
    SaveState save_state; //Json ���� �ε�
    GraphicRaycaster m_gr;
    PointerEventData m_ped;
    private void Start()
    {
        m_gr = m_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);
    }
    public void JsonLoader()
    {
        save_state = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").transform.name);
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
        if (results[0].gameObject.transform.tag == "item")
        {
            //��������Ʈ �߰� ��
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", save_state.item_info[int.Parse(results[0].gameObject.transform.name)].m_name);
        }
        else if(results[0].gameObject.transform.tag == "Character")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", save_state.chanllenge_info[int.Parse(results[0].gameObject.transform.name)].m_name);
        }
        else if(results[0].gameObject.transform.tag == "Stage")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", save_state.stage_info[int.Parse(results[0].gameObject.transform.name)].m_name);
        }
        else if (results[0].gameObject.transform.tag == "Challenge")
        {
            m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", save_state.chanllenge_info[int.Parse(results[0].gameObject.transform.name)].m_name);
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
