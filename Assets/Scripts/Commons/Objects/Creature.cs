using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float m_health;
    public float m_x_velocity;
    public float m_y_velocity;

    // 64bit, 최대 64개의 상태이상
    public long m_status_effect;

    // 스킬 제한 개수
    public int m_abilities_limit;
}
