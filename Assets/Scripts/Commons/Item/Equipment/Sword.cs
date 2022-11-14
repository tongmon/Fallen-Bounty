using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment
{
    public Sword()
    {
        m_coefficent = 1.2f;
    }
    public override void Equip(HeroData herodata)
    {
        herodata.physic_coefficient = m_coefficent;
    }
}
