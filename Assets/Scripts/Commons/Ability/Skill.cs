using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill : MonoBehaviour //Ÿ���� �̵�, ��Ÿ� �ߵ� ���� ��ų
{
    public string style;

    Vector3 vec;

    public HeroData hdata;

    public GameObject m_character;

    RaycastHit2D hit;

    public float active_time;

    public float damage;

    public bool posible = false;//��ų ��� ���� ����

    bool buttonClick = false;//��ų ��� ���� ��ġ ���� ����

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
        if (posible)//�� ���ƾ���.
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
                    m_character.transform.DOMove(new Vector3(vec.x, vec.y, 0), active_time);//������ �ִϸ��̼� �ð� �������.
                }
                Destroy(gameObject, duration_time);
                buttonClick = true;
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

