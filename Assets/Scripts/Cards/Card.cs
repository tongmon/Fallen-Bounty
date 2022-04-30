using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : ScriptableObject
{
    // ī�尡 �ش��ϴ� ī�װ�, ex) State, Ability, Passive, EveryThing...
    public string m_apply_category;

    // ī�װ����� �� ������ ������ Ÿ��, ex) DashAbility(Ability), Ninja(State) 
    public string m_apply_target;

    // ī�尡 ����Ǵ� ����, ī�带 ���� �� �����Ǿ�� ��
    public string m_apply_hero;

    // ī�� Ŭ���� �̸�
    public string m_name;

    // ī�� ����, ����: ī�� �̸� + '/' + ī�� ����
    public string m_description;

    // ī�� ���
    public int m_quality;
    
    // ī�� ȹ��
    public virtual void Acquisit(GameObject obj) { }
}