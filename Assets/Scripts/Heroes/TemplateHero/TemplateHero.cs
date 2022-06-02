using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : Hero
{
    public TemplateHero()
    {
        type_name = GetType().Name;
        m_ability_holder = new AbilityHolder(gameObject);
    }

    private new void Awake()
    {
        base.Awake();
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
