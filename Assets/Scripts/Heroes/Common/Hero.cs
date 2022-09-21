using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hero : Creature
{
    // 공격 속도
    public float m_cur_attack_cooltime;
    //가지고 있을 스킬, ㄹㅇ가지고만 있음
    public List<Ability> abilities;
    //이미지
    public Sprite m_sprite;
    //언락 여부
    public bool is_unlocked;
    protected override void OnAwake()
    {
        base.OnAwake();

        //abilities = new List<Ability>();

        m_point_target = null;

        m_cur_attack_cooltime = 0;

        m_sprite = null;

        is_unlocked = false;
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

    public void StartHitToolTip(float health)
    {
        StartCoroutine(HitToolTip(health));
    }
    public IEnumerator HitToolTip()
    {
        transform.GetChild(transform.childCount-1).GetChild(0).GetComponent<Image>().DOColor(Color.white, 0.1f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOColor(Color.white, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);

        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>().DOFade(0.2f, 1.0f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOFade(0.2f, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);

        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<Image>().DOFade(0, 1.0f);
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().DOFade(0, 1.0f);
    }
    public IEnumerator HitToolTip(float health)
    {
        transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<Image>().fillAmount = m_current_health / health;
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