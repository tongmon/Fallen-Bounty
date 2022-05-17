using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField] GameObject m_panel;
    
    void Update()
    {
        
    }
    public void PauseButton()
    {
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = Time.timeScale;
        m_panel.SetActive(true);
    }
    /*public void DoubleTime()
    {
        Time.timeScale = 2.0f;
        Time.fixedDeltaTime = Time.timeScale;
    }*/
}
