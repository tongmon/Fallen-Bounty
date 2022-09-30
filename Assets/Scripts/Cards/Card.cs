using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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