using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    RectTransform rectTransform;
    bool is_select = true;
    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0); //색없애기
        rectTransform.GetComponent<RectTransform>();
        is_select = GameObject.Find("Me").GetComponent<MouseFollow>().is_select;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_select)
        {
            Vector3 distance = GameObject.Find("Me").GetComponent<MouseFollow>().vec - gameObject.transform.position;
            rectTransform.sizeDelta = new Vector2(distance.magnitude, 1.0f);
            rectTransform.pivot = new Vector2(0, 0.5f);
            rectTransform.position = gameObject.transform.position;
        }
    }
}
