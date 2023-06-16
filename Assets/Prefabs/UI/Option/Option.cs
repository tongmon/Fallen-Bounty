using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : FadeInOut
{
    //�� ����
    [SerializeField] GameObject AudioSetting;

    [SerializeField] GameObject ResolutionSetting;

    [SerializeField] GameObject KeySetting;

    [SerializeField] GameObject GameplaySetting;

    [SerializeField] GameObject Skill; //��ų ������
    bool m_skillMove = false;
    public void AudioButtonClicked() //�� ��ư Ŭ���� �¿��� ����
    {
        AudioSetting.SetActive(true);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
        m_skillMove = false;
    }
    public void ResolutionButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(true);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
        m_skillMove = true;
    }
    public void GraphicButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
        m_skillMove = false;
    }
    public void KeyboardButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(true);
        GameplaySetting.SetActive(false);
        m_skillMove = false;
    }
    public void GameplayButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(true);
        m_skillMove = false;
    }
    private void Update()
    {
        if (m_skillMove) Skill.transform.position = new Vector3(-0.8f, -1.5f , 0); //��ų �������� ������ �����Ŵ
        else Skill.transform.position = new Vector3(-10, -15 , 0);
    }
}