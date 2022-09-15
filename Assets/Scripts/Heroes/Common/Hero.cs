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
    // ���� ���ݷ�
    public int physic_power;
    // ���� ���ݷ�
    public int magic_power;
    // ��Ÿ �ӵ�, �� ����
    public float attack_cooltime;
    // ���� ����
    public float melee_range;
    // ���� ����
    public float ranged_range;

    public string m_info;
    #endregion
}

public class Hero : Creature
{
    // ���� �ӵ�
    public float m_cur_attack_cooltime;
    //������ ���� ��ų, ���������� ����
    public List<Ability> abilities;
    //�̹���
    public Sprite m_sprite;

    public string m_name; //����� �̸�.

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