using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability m_ability;
    float m_cooldowntime;
    float m_activetime;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState m_state = AbilityState.ready;

    // Start is called before the first frame update
    void Start()
    {
        m_ability = new DashAbility();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_ability.Activate(gameObject);
                    m_state = AbilityState.active;
                }
                break;
            case AbilityState.active:
                if(m_activetime > 0)
                {
                    m_activetime -= Time.deltaTime;
                }
                else
                {
                    m_state = AbilityState.ready;
                }
                break;
            case AbilityState.cooldown:
                break;

        }
    }
}
