using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : ScriptableObject
{
    public Sprite m_sprite; //이미지

    public float m_coefficent; //계수

    public virtual void Equip(HeroData herodata)
    {
    }
}
