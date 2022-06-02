using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
public class EnemyData : CreatureData
{
    // ���� ���ݷ�
    public float physic_power;
    // ���� ���ݷ�
    public float magic_power;
    // ��Ÿ �ӵ�
    public float attack_speed;
    // ���� ����
    public float attack_range;
}
