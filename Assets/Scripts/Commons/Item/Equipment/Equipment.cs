using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject
{
    public Sprite m_sprite; //�̹���

    public float m_coefficent; //���

    public virtual void Equip(HeroData herodata)
    {
    }
}
