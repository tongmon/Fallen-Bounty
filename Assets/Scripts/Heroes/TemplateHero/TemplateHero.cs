using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : Hero
{
    public TemplateHero()
    {
        m_name = GetType().Name;
        m_ability_holder = new AbilityHolder(gameObject);
    }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
    }
}
