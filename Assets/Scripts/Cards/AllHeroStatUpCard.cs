using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
public class AllHeroStatUpCard : Card
{
    public float stat_up_rate;

    public AllHeroStatUpCard(string description, string target, int quality)
    {
        card_name = GetType().Name;
        this.description = description;
        this.quality = quality;
        apply_target = target;
    }

    public override void Acquisit(GameObject obj) 
    {
        Player player = obj.GetComponent<Player>();

        
    }
}
