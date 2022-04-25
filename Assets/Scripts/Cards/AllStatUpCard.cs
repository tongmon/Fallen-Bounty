using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStatUpCard : Card
{
    public AllStatUpCard(string description, int quality)
    {
        m_name = GetType().Name;
        m_description = description;
        m_quality = quality;
    }

    public override void Acquisit(GameObject obj) 
    {
        Player player = obj.GetComponent<Player>();

        
    }
}
