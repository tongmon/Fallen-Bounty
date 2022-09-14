using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill : MonoBehaviour
{
    Vector3 vec;

    public GameObject characterTarget;

    public float range = 0;

    public float duration_time = 0;

    public float active_time = 0;

    public bool posible = false;

    bool buttonClick = false;
    private void Start()
    {
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
    }
    void Update()
    {
        if(!buttonClick) vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vec.x, vec.y ,0);
        if (posible)//�� ���ƾ���.
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.AddComponent<CircleCollider2D>();
                GetComponent<CircleCollider2D>().isTrigger = true;
                buttonClick = true;
                characterTarget.transform.DOMove(new Vector3(vec.x, vec.y, 0), active_time);//������ �ִϸ��̼� �ð� �������.
                Destroy(gameObject, duration_time);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
        else//���� �ȵ���
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
    }
}
