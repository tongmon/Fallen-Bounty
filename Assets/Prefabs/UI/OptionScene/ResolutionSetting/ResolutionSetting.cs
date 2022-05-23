using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    FullScreenMode m_screenMode; //풀 스크린 클래스
    public Dropdown m_dropdown; //해상도 드롭다운
    public Toggle m_fullscreen_button; //풀 스크린 토글버튼

    public Toggle m_damage_toggle; //피해량 표시 토글버튼
    public GameObject m_DamageText;// 피해량 텍스트

    public Slider m_gamma_slider; //감마 슬라이드
    public Light m_global_light; //감마 적용 광원

    public Slider m_HUD_slider; //스킬 등 HUD 적용 슬라이드
    public GameObject m_HUD; //HUD 프리팹
    
    List<Resolution> m_resolution = new List<Resolution>(); //해상도 리스트
    public int m_resolution_value;//해상도 위치값
    private void Awake()
    {
        InitializeUI();
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;//프리팹화 시킨 광원과 현재 광원의 일치화
        GameObject.Find("Skill").transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value, 0);//HUD도 마찬가지
        m_HUD_slider.value = m_HUD.transform.localScale.x;
        m_gamma_slider.value = m_global_light.intensity;
    }
    void InitializeUI()
    {
        for(int i=0; i<Screen.resolutions.Length; i++) 
        {
            if (Screen.resolutions[i].refreshRate == 75) //해상도 hz값이 75인것만 Add하기
                m_resolution.Add(Screen.resolutions[i]);
        }
        m_dropdown.options.Clear();//드롭다운 초기화

        int optionNum = 0;
        foreach(Resolution item in m_resolution)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz"; //해상도 구체화 시켜서 저장
            m_dropdown.options.Add(option);//입력

            if (item.width == Screen.width && item.height == Screen.height) //해당값으로 스크린에 적용
                m_dropdown.value = optionNum;
            optionNum++;
        }

        m_dropdown.RefreshShownValue();

        m_fullscreen_button.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false; //전체화면 유무 확인후 온오프 적용

    }
    public void DropdownOptionChange(int x)
    {
        m_resolution_value = x; //밸류값 가져오기
    }
    public void ApplyButton()
    { //전체화면 적용버튼
        Screen.SetResolution(m_resolution[m_resolution_value].width, m_resolution[m_resolution_value].height, m_screenMode); 
    }
    public void FullScreenButton(bool isFull)
    {//전체화면 버튼
        m_screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void VSyncChange()
    {//수직동기화 토글
        QualitySettings.vSyncCount ^= 1;
    }
    public void DamageToggle()
    {//데미지 효과 토글
        m_DamageText.SetActive(m_damage_toggle.isOn);
    }
    public void GammaEdit()
    {//감마효과적용
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;//강도 조절
        m_global_light.intensity = m_gamma_slider.value; 
    }
    public void HUDSizeEdit()
    {
        GameObject.Find("Skill").transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value, 0);//로컬스케일로 변경
        m_HUD.transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value,0);
    }
}
