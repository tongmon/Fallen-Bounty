using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Card : ScriptableObject
{
    /*
    //���� - �� Ŭ����� ���� ���� ��ų�� ī�彽�Ը�ŭ �����ͼ� �������� �����̺�Ʈ �����.
    //��1 - 100�� ��2 - 50�� ��3- 25�۷� ����.
    public int m_card_slots = 3; //÷�� 3 �����߰��� 4����� ����

    public void SelectionApply() //ī�弱������
    {
        for (int i = 0; i < m_card_slots; i++)
        {
            if (Random.Range(1, 12) % 4 == 0) ; //m_abilities[Random.Range(1,m_card_slots)].�Ӽ��ο�(bool) = true; -��3
            else if (Random.Range(1, 2) % 2 == 0) ; //m_abilities[Random.Range(1,m_card_slots)].m_cur_cooldown_time -=0.5f; -��2
            else ; //m_abilities[Random.Range(1,m_card_slots)].��������� +=0.5f; -��1
        }
    }
    */

    public string m_apply_target; // ī�� ���� ��ǥ
    public string m_name; // ī�� Ŭ���� �̸�
    public string m_description; // ī�� ����, ����: ī�� �̸� + '/' + ī�� ����
    public int m_quality; // ī�� ���

    // ī�� ȹ��
    public virtual void Acquisit(GameObject obj) { }
}
