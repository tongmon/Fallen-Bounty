using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
public class DashAbilityEnhanceCard : Card
{
    public float dash_power;

    public DashAbilityEnhanceCard(string description, int quality)
    {
        card_name = GetType().Name;
        this.description = description;
        this.quality = quality;
    }

    public override void Acquisit(GameObject obj)
    {
        Player player = obj.GetComponent<Player>();

        HeroHolder hero_holder = player.m_hero_holder;
        
    }
}
