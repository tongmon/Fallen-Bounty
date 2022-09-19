using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatureData : ScriptableObject
{
    // 생명체 이름
    public string type_name;
    // 생명력(체력)
    public float health;
    // 생명체 속도
    public Vector2 velocity;
    // 질량
    public float mass;
    // 마력 방어력
    public int magic_armor;
    // 물리 방어력
    public int physic_armor;
}
