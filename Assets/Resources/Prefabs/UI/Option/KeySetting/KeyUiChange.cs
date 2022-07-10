using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyUiChange : MonoBehaviour
{
    public Text[] txt;
    void Start()
    {
        for(int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySet.m_keys[(m_key_action)i].ToString(); 
        }
    }
    void Update()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySet.m_keys[(m_key_action)i].ToString();
        }
    }
}
