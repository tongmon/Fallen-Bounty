using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbilityEnhanceCard : Card
{
    public DashAbilityEnhanceCard(string description, int quality)
    {
        m_name = GetType().Name;
        m_description = description;
        m_quality = quality;
    }

    public override void Acquisit(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();

        HeroHolder hero_holder = player.m_hero_holder;
        
    }
}
