using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.Security.Policy;

[Serializable]
public class StageInfo : Info
{
    public string m_boss_name = string.Empty;
    public float m_clear_time = 0.0f;
    public float m_clear_count = 0.0f;

    public override void InfoSetting(int index, JsonData data)
    {
        base.InfoSetting(index, data);

        m_boss_name = data[index]["m_boss_name"].ToString();
        m_clear_time = float.Parse(data[index]["m_clear_time"].ToString());
        m_clear_count = float.Parse(data[index]["m_clear_count"].ToString());
    }
}
