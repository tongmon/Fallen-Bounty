using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Enemy : Creature
{
    EnemyData Edata;
    protected override void OnAwake()
    {
        base.OnAwake();
        Edata = (EnemyData)m_data;
    }

    protected override void OnStart()
    {
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnFixedUpdate()
    {

    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            m_hit_state.Update();
            m_current_health -= float.Parse(other.name);
            transform.GetChild(1).GetChild(1).GetComponent<Image>().fillAmount =  m_current_health/Edata.health;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item" || other.tag == "Skill")
        {
            if (other.tag == "Skill")
            {
                m_current_health -= float.Parse(other.name);
                other.GetComponent<Ability>().m_hit_count++;
            }
            StopCoroutine("HitToolTip");
            m_hit_state.Enter();
            StartCoroutine("HitToolTip");
        }
    }

    public void StartHitToolTip()
    {
        StartCoroutine(HitToolTip());
    }
    public IEnumerator HitToolTip()
    {
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().fillAmount = m_current_health / Edata.health;
        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>().DOColor(Color.white, 0.1f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOColor(Color.white, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);

        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>().DOFade(0.2f, 1.0f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOFade(0.2f, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);

        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>().DOFade(0, 1.0f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOFade(0, 1.0f);
    }
}
