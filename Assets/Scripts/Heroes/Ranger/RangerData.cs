using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RangerData : HeroData
{
    // 레인저는 몬스터의 약점이 주기적으로 뜨고 해당 약점을 가진 적을 때리면 해당 전투한정으로 데미지가 세짐.
    public int weakness_hit_cnt;
    public float weakness_popup_cooltime;
    public string projectile_type;
    public Vector2 arrow_velocity;
}
