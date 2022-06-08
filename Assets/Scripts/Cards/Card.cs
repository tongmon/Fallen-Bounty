using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(DashAbilityEnhanceCard), "dash_power")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(AllHeroStatUpCard), "stat_up_rate")]
public class Card : ScriptableObject
{
    // ī�尡 �ش��ϴ� ī�װ�, ex) State, Ability, Passive, EveryThing...
    public string apply_category;

    // ī�װ����� �� ������ ������ Ÿ��, ex) DashAbility(Ability), Ninja(State) 
    public string apply_target;

    // ī�尡 ����Ǵ� ����, ī�带 ���� �� �����Ǿ�� ��
    public string apply_hero;
    
    // ī�� Ŭ���� �̸�
    public string card_name;

    // ī�� ����, ����: ī�� �̸� + '/' + ī�� ����
    public string description;

    // ī�� ���
    public int quality;
    
    // ī�� ȹ��
    public virtual void Acquisit(GameObject obj) { }
}