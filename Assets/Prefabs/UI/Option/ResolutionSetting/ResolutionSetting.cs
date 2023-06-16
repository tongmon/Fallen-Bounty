using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    //Ǯ ��ũ�� Ŭ����
    FullScreenMode m_screenMode;

    //�ػ� ��Ӵٿ�
    public Dropdown m_dropdown;

    //Ǯ ��ũ�� ��۹�ư
    public Toggle m_fullscreen_button;

    //���ط� ǥ�� ��۹�ư
    public Toggle m_damage_toggle;

    // ���ط� �ؽ�Ʈ
    public GameObject m_DamageText;

    //���� �����̵�
    public Slider m_gamma_slider;

    //���� ���� ����
    public Light m_global_light;

    //��ų �� HUD ���� �����̵�
    public Slider m_HUD_slider;

    //HUD ������
    public GameObject m_HUD;

    //�ػ� ����Ʈ
    List<Resolution> m_resolution = new List<Resolution>();

    //�ػ� ��ġ��
    public int m_resolution_value;
    private void Awake()
    {
        InitializeUI();
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;//������ȭ ��Ų ������ ���� ������ ��ġȭ
        GameObject.Find("Skill").transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value, 0);//HUD�� ��������
        m_HUD_slider.value = m_HUD.transform.localScale.x;
        m_gamma_slider.value = m_global_light.intensity;
    }
    void InitializeUI()
    {
        for(int i=0; i<Screen.resolutions.Length; i++) 
        {
            if (Screen.resolutions[i].refreshRate == 60) //�ػ� hz���� 75�ΰ͸� Add�ϱ�
                m_resolution.Add(Screen.resolutions[i]);
        }
        m_dropdown.options.Clear();//��Ӵٿ� �ʱ�ȭ

        int optionNum = 0;
        foreach(Resolution item in m_resolution)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz"; //�ػ� ��üȭ ���Ѽ� ����
            m_dropdown.options.Add(option);//�Է�

            if (item.width == Screen.width && item.height == Screen.height) //�ش簪���� ��ũ���� ����
                m_dropdown.value = optionNum;
            optionNum++;
        }

        m_dropdown.RefreshShownValue();

        m_fullscreen_button.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false; //��üȭ�� ���� Ȯ���� �¿��� ����

    }
    public void DropdownOptionChange(int x)
    {
        m_resolution_value = x; //����� ��������
    }
    public void ApplyButton() //��üȭ�� �����ư
    { 
        Screen.SetResolution(m_resolution[m_resolution_value].width, m_resolution[m_resolution_value].height, m_screenMode); 
    }
    public void FullScreenButton(bool isFull) //��üȭ�� ��ư
    {
        m_screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void VSyncChange() //��������ȭ ���
    {
        QualitySettings.vSyncCount ^= 1;
    }
    public void DamageToggle() //������ ȿ�� ���
    {
        m_DamageText.SetActive(m_damage_toggle.isOn);
    }
    public void GammaEdit() //����ȿ������
    {
        GameObject.Find("GlobalLight").GetComponent<Light>().intensity = m_global_light.intensity;//���� ����
        m_global_light.intensity = m_gamma_slider.value; 
    }
    public void HUDSizeEdit() //��ų ������ ����
    {
        GameObject.Find("Skill").transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value, 0);
        m_HUD.transform.localScale = new Vector3(m_HUD_slider.value, m_HUD_slider.value,0);
    }
}
