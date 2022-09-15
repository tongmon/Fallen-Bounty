using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill : MonoBehaviour //타겟팅 이동, 사거리 발동 전용 스킬
{
    public string style;

    Vector3 vec;

    public HeroData hdata;

    public GameObject m_character;

    RaycastHit2D hit;

    public float active_time;

    public float damage;

    public bool posible = false;//스킬 사용 가능 여부

    bool buttonClick = false;//스킬 사용 이후 위치 변경 여부

    public float range = 0;

    public float duration_time = 0;

    private void OnEnable()
    {
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
    }
    void Update()
    {
        if (!buttonClick) vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vec.x, vec.y, 0);
        if (posible)//쿨 돌아야함.
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (style == "Target")
                {
                    hit = Physics2D.Raycast(vec, transform.forward, 110, ~(1<<9));
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().m_current_health -= damage;
                        StartCoroutine(hit.collider.gameObject.GetComponent<Enemy>().HitToolTip());
                        m_character.GetComponent<Hero>().m_current_health -= damage / 3;
                        StartCoroutine(m_character.GetComponent<Hero>().HitToolTip(hdata.health));
                        buttonClick = true;
                    }
                }
                if (style == "Range")
                {
                    gameObject.AddComponent<CircleCollider2D>();
                    GetComponent<CircleCollider2D>().isTrigger = true;
                    m_character.transform.DOMove(new Vector3(vec.x, vec.y, 0), active_time);//실제론 애니메이션 시간 빼줘야함.
                }
                Destroy(gameObject, duration_time);
                buttonClick = true;
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

