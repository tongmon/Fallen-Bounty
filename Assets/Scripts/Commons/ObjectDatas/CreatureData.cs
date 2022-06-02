using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(HeroData), "physic_power")]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(EnemyData), "magic_power")]
public class CreatureData
{
    // 생명체 이름
    public string name;
    // 생명력(체력)
    public float health;
    // 좌우 속도
    public float x_velocity;
    // 상하 속도
    public float y_velocity;
    // 64bit, 최대 64개의 상태이상
    public long status_effect;
    // 마력 방어력
    public float magic_armor;
    // 물리 방어력
    public float physic_armor;
}
