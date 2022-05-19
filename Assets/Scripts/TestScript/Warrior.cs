using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    private new void Awake()
    {
        base.Awake();
        m_name = GetType().Name;
        m_ability_holder = new AbilityHolder(gameObject);
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }
}