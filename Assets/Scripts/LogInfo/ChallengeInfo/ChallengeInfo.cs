using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

[Serializable]

public class ChallengeInfo : Info
{
    public float m_target_num;
    public float m_total_num;

    public override void InfoSetting(int index, JsonData data)
    {
        base.InfoSetting(index, data);

        m_target_num = float.Parse(data[index]["m_target_num"].ToString());
        m_total_num = float.Parse(data[index]["m_total_num"].ToString());
    }
}
