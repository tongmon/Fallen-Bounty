using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject AudioSetting;
    [SerializeField] GameObject ResolutionSetting;
    [SerializeField] GameObject KeySetting;
    [SerializeField] GameObject GameplaySetting;
    [SerializeField] GameObject Skill; //스킬 프리팹
    bool m_skillMove = false;
    public void AudioButtonClicked() //각 버튼 클릭시 온오프 적용
    {
        AudioSetting.SetActive(true);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
    }
    public void ResolutionButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(true);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
        m_skillMove ^= true;
    }
    public void GraphicButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(false);
    }
    public void KeyboardButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(true);
        GameplaySetting.SetActive(false);
    }
    public void GameplayButtonClicked()
    {
        AudioSetting.SetActive(false);
        ResolutionSetting.SetActive(false);
        KeySetting.SetActive(false);
        GameplaySetting.SetActive(true);
    }
    public void BackButtonClicked()
    {
        SceneManager.LoadScene("Title_Scene");
    }
    private void Update()
    {
        if (m_skillMove) Skill.transform.position = new Vector3(-0.8f, -1.5f , 0); //스킬 프리팹을 씬으로 등장시킴
        else Skill.transform.position = new Vector3(-10, -15 , 0);
    }
}
