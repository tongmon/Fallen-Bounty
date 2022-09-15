using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill : MonoBehaviour
{
    Vector3 vec;

    public GameObject characterTarget;//스킬 시전한 캐릭터

    public float range = 0;

    public float duration_time = 0;

    public float active_time = 0;

    public bool posible = false;//스킬 사용 가능 여부

    bool buttonClick = false;//스킬 사용 이후 위치 변경 여부

    private void OnEnable()
    {
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
    }
    void Update()
    {
        if(!buttonClick) vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vec.x, vec.y ,0);
        if (posible)//쿨 돌아야함.
        {
            if (Input.GetMouseButtonDown(0))
            {
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
        else//쿨은 안돌게
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
    }
}
