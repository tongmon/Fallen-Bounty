using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : parent
{
    parent m_parent;

    // Start is called before the first frame update
    void Start()
    {
        m_parent.m_speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
