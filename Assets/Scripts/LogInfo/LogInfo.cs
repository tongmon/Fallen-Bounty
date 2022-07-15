using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[CreateAssetMenu]
public class Info : ScriptableObject
{
    public Sprite m_sprite;
    public string m_name = string.Empty;
    public string m_info = string.Empty;
    public string m_unlock_condition = string.Empty;
}

