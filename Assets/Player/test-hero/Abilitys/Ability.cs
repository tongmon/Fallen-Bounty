using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string m_name;
    public float m_cooldowntime;
    public float m_activetime;

    public virtual void Activate(GameObject hero) { }
}
