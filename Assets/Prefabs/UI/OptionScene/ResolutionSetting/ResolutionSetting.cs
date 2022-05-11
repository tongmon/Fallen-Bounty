using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    FullScreenMode m_screenMode;
    public Dropdown m_dropdown;
    public Toggle m_fullscreen_button;
    public Toggle m_damage_toggle;
    public GameObject m_DamageText;
    public Slider m_gamma_slider;
    public Light m_global_light;
    
    List<Resolution> m_resolution = new List<Resolution>();
    public int m_resolution_value;
    private void Start()
    {
        InitializeUI();
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;
        m_gamma_slider.value = m_global_light.intensity;
    }
    void InitializeUI()
    {
        for(int i=0; i<Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 75)
                m_resolution.Add(Screen.resolutions[i]);
        }
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
    public void VSyncChange()
    {
        QualitySettings.vSyncCount ^= 1;
    }
    public void DamageToggle()
    {
        m_DamageText.SetActive(m_damage_toggle.isOn);
    }
    public void GammaEdit()
    {
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;
        m_global_light.intensity = m_gamma_slider.value; 
    }
}
