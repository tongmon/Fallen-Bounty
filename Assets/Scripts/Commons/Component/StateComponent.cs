using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateComponent
{
    public object m_data; 
 
    public StateComponent(GameObject gameobject)
    {
        m_data = gameobject;
    }

    public virtual void Update()
    {
        
    }

    public virtual void Enter()
    {

    }
}
