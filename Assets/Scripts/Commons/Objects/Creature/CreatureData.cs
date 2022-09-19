using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatureData : ScriptableObject
{
    // ����ü �̸�
    public string type_name;
    // �����(ü��)
    public float health;
    // ����ü �ӵ�
    public Vector2 velocity;
    // ����
    public float mass;
    // ���� ����
    public int magic_armor;
    // ���� ����
    public int physic_armor;
}
