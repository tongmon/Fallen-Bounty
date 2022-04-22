using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateHero : Hero
{
    #region 스킬 로직
    AbilityHolder m_ability_holder;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_ability_holder = new AbilityHolder(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }
}
