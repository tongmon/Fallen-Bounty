using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawLine : MonoBehaviour
{
    RectTransform rectTransform;
    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0); //�����ֱ�
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Me").GetComponent<MouseFollow>().path_select)//��Ŀ���� �ٲ����
        {
            gameObject.transform.position = GameObject.Find("Me").GetComponent<MouseFollow>().m_focus_object.transform.position; //���߿� ��Ŀ���� ������� �ٲ����
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
            Vector2 distance = GameObject.Find("Me").GetComponent<MouseFollow>().vec - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            rectTransform.sizeDelta = new Vector2(distance.magnitude, 2.0f);
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.position = gameObject.transform.position;
            GetComponent<Image>().DOFade(0.2f, 1.5f);
        }
    }
}
