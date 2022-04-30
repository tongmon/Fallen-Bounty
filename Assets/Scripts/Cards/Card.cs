using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : ScriptableObject
{
    // 카드가 해당하는 카테고리, ex) State, Ability, Passive, EveryThing...
    public string m_apply_category;

    // 카테고리보다 더 세밀한 범주인 타겟, ex) DashAbility(Ability), Ninja(State) 
    public string m_apply_target;

    // 카드가 적용되는 영웅, 카드를 뽑을 때 결정되어야 함
    public string m_apply_hero;

    // 카드 클래스 이름
    public string m_name;

    // 카드 설명, 구성: 카드 이름 + '/' + 카드 설명
    public string m_description;

    // 카드 등급
    public int m_quality;
    
    // 카드 획득
    public virtual void Acquisit(GameObject obj) { }
}