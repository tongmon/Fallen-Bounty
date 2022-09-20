using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangerData : HeroData
{
    // �������� ������ ������ �ֱ������� �߰� �ش� ������ ���� ���� ������ �ش� ������������ �������� ����.
    public int weakness_hit_cnt;
    public float weakness_popup_cooltime;
    public string projectile_type;
    public Vector2 arrow_velocity;
}
