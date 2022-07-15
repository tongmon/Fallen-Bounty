using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu]
public class ItemInfo : Info
{
    public float m_damage = 0.0f;
    public float m_range = 0.0f;
    public float m_duration = 0.0f;
    public float m_cooltime = 0.0f;
}