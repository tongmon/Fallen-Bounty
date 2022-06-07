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
    // 카드가 해당하는 카테고리, ex) State, Ability, Passive, EveryThing...
    public string apply_category;

    // 카테고리보다 더 세밀한 범주인 타겟, ex) DashAbility(Ability), Ninja(State) 
    public string apply_target;

    // 카드가 적용되는 영웅, 카드를 뽑을 때 결정되어야 함
    public string apply_hero;
    
    // 카드 클래스 이름
    public string card_name;

    // 카드 설명, 구성: 카드 이름 + '/' + 카드 설명
    public string description;

    // 카드 등급
    public int quality;
    
    // 카드 획득
    public virtual void Acquisit(GameObject obj) { }
}