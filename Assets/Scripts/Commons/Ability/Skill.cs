using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill : MonoBehaviour
{
    public float range = 0;

    public float duration_time = 0;

    public float active_time = 0;

    Vector3 vec;

    public GameObject characterTarget;

    bool buttonClick = false;
    private void Start()
    {
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }
    void Update()
    {
        if(!buttonClick) vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vec.x, vec.y ,0);
        if (Input.GetMouseButtonDown(0))
        {
            tag = "Skill";
            gameObject.AddComponent<CircleCollider2D>();
            GetComponent<CircleCollider2D>().isTrigger = true;
            buttonClick = true;
            characterTarget.transform.DOMove(new Vector3(vec.x, vec.y, 0), active_time);//실제론 애니메이션 시간 빼줘야함.
            Destroy(gameObject, duration_time);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
