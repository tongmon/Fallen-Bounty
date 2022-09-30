using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHeroStatUpCard : Card
{
    public float stat_up_rate;

    public AllHeroStatUpCard(string description, string target, int quality, float stat_up)
    {
        card_name = GetType().Name;
        this.description = description;
        this.quality = quality;
        apply_target = target;
        stat_up_rate = stat_up;
    }

    public override void Acquisit(GameObject obj) //√ÎµÊ
    {
        Player player = obj.GetComponent<Player>();
        player.m_all_stat_coefficent += stat_up_rate;
    }
}
