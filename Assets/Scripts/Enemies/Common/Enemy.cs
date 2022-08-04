using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[JsonConverter(typeof(JsonSubtypes))]
class EnemyData : CreatureData
{
    #region Data from JSON file
    
    #endregion
}

public class Enemy : Creature
{
    protected override void OnAwake()
    {
        base.OnAwake();
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
            transform.GetChild(1).GetChild(1).GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, m_current_health);//현재 체력을 선형보간
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            m_hit_state.Enter();
            StartCoroutine(HitToolTip());
        }
    }
    IEnumerator HitToolTip()
    {
        transform.GetChild(1).GetChild(0).GetComponent<Image>().DOColor(Color.white, 0.1f);
        transform.GetChild(1).GetChild(1).GetComponent<Image>().DOColor(Color.white, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);

        transform.GetChild(1).GetChild(0).GetComponent<Image>().DOFade(0.2f, 1.0f);
        transform.GetChild(1).GetChild(1).GetComponent<Image>().DOFade(0.2f, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);

        transform.GetChild(1).GetChild(0).GetComponent<Image>().DOFade(0, 1.0f);
        transform.GetChild(1).GetChild(1).GetComponent<Image>().DOFade(0, 1.0f);
    }
}
