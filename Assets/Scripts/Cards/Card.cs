using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : ScriptableObject
{
    /*
    //구조 - 맵 클리어시 현재 가진 스킬중 카드슬롯만큼 가져와서 랜덤으로 증가이벤트 만들기.
    //퀄1 - 100퍼 퀄2 - 50퍼 퀄3- 25퍼로 가정.
    public int m_card_slots = 3; //첨엔 3 영웅추가로 4명까지 가능

    public void SelectionApply() //카드선택적용
    {
        for (int i = 0; i < m_card_slots; i++)
        {
            if (Random.Range(1, 12) % 4 == 0) ; //m_abilities[Random.Range(1,m_card_slots)].속성부여(bool) = true; -퀄3
            else if (Random.Range(1, 2) % 2 == 0) ; //m_abilities[Random.Range(1,m_card_slots)].m_cur_cooldown_time -=0.5f; -퀄2
            else ; //m_abilities[Random.Range(1,m_card_slots)].데미지계수 +=0.5f; -퀄1
        }
    }
    */

    public string m_apply_target; // 카드 적용 목표
    public string m_name; // 카드 클래스 이름
    public string m_description; // 카드 설명, 구성: 카드 이름 + '/' + 카드 설명
    public int m_quality; // 카드 등급

    // 카드 획득
    public virtual void Acquisit(GameObject obj) { }
}
