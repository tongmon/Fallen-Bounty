using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    public Dropdown m_dropdown;
    List<Resolution> m_resolution = new List<Resolution>();

    private void Start()
    {
        InitializeUI();
    }
    void InitializeUI()
    {
        m_resolution.AddRange(Screen.resolutions);
        m_dropdown.options.Clear();

        foreach(Resolution item in m_resolution)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            m_dropdown.options.Add(option);
        }
        m_dropdown.RefreshShownValue();
    }
}
