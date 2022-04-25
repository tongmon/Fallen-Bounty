using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public Vector3 vec;
    public bool is_select = false;
    RaycastHit hit;
    private void Start()
    {
        vec = gameObject.transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            is_select = true;
        }
        if(hit.collider != null) //선택이 됐을때
        {
            vec = hit.transform.position;
        }
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, vec, Time.deltaTime);
    }
}
