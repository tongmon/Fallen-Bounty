using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawLine : MouseFollow
{
    RectTransform m_rectTransform;
    Vector2 m_distance;

    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();   
    }
    void Update()
    {
        if (m_path_select)
        {
            gameObject.transform.position = m_focus_object.transform.GetChild(0).transform.position; //나중에 포커스한 대상으로 바꿔야함
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
            if (m_hit.collider != null) m_distance = gameObject.transform.position - m_focus_enemy.transform.position;
            else m_distance = new Vector2(gameObject.transform.position.x - m_vec.x ,gameObject.transform.position.y - m_vec.y);
            m_rectTransform.sizeDelta = new Vector2(m_distance.magnitude, 2.0f);
            m_rectTransform.pivot = new Vector2(0, 0.5f);
            m_rectTransform.position = gameObject.transform.position;
        }
    }
}
