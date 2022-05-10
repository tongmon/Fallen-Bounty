using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    FullScreenMode m_screenMode;
    public Dropdown m_dropdown;
    public Toggle m_fullscreen_button;
    List<Resolution> m_resolution = new List<Resolution>();
    public int m_resolution_value;
    private void Start()
    {
        InitializeUI();
    }
    void InitializeUI()
    {
        m_resolution.AddRange(Screen.resolutions);
        m_dropdown.options.Clear();

        int optionNum = 0;
        foreach(Resolution item in m_resolution)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            m_dropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                m_dropdown.value = optionNum;
            optionNum++;
        }

        m_dropdown.RefreshShownValue();

        m_fullscreen_button.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void DropdownOptionChange(int x)
    {
        m_resolution_value = x;
    }
    public void ApplyButton()
    {
        Screen.SetResolution(m_resolution[m_resolution_value].width, m_resolution[m_resolution_value].height, m_screenMode);
    }
    public void FullScreenButton(bool isFull)
    {
        m_screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
}
