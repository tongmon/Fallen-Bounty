using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(RangerData), "weakness_popup_cooltime")]
public class HeroData : CreatureData
{
    #region Data from JSON file
    // 물리 공격력
    public int physic_power;
    // 마법 공격력
    public int magic_power;
    // 평타 속도, 초 단위
    public float attack_cooltime;
    // 공격 범위
    public float melee_range;
    // 공격 범위
    public float ranged_range;

    public string m_info;
    #endregion
}

public class Hero : Creature
{
    // 공격 속도
    public float m_cur_attack_cooltime;
    //가지고 있을 스킬, ㄹㅇ가지고만 있음
    public List<Ability> abilities;
    //이미지
    public Sprite m_sprite;

    public string m_name; //히어로 이름.

    public bool is_unlocked;
    protected override void OnAwake()
    {
        base.OnAwake();

        abilities = new List<Ability>();

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
    public IEnumerator HitToolTip()
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
    public IEnumerator HitToolTip(float health)
    {
        transform.GetChild(1).GetChild(1).GetComponent<Image>().fillAmount = m_current_health / health;
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