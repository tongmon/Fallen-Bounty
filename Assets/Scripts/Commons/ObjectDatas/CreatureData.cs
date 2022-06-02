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
    // ����ü �̸�
    public string name;
    // �����(ü��)
    public float health;
    // �¿� �ӵ�
    public float x_velocity;
    // ���� �ӵ�
    public float y_velocity;
    // 64bit, �ִ� 64���� �����̻�
    public long status_effect;
    // ���� ����
    public float magic_armor;
    // ���� ����
    public float physic_armor;
}
