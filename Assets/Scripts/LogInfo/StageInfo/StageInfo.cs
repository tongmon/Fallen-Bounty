using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu]
public class StageInfo : Info
{
    public string m_boss_name = string.Empty;
    public float m_clear_time = 0.0f;
    public float m_clear_count = 0.0f;
}
