using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
public class EnemyData : CreatureData
{
    // 물리 공격력
    public float physic_power;
    // 마법 공격력
    public float magic_power;
    // 평타 속도
    public float attack_speed;
    // 공격 범위
    public float attack_range;
}
