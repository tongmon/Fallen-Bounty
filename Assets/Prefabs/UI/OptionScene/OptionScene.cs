using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionScene : MonoBehaviour
{
    [SerializeField] GameObject AudioSetting;
    [SerializeField] GameObject ResolutionSetting;
    [SerializeField] GameObject KeySetting;
    [SerializeField] GameObject GameplaySetting;
    public void AudioButtonClicked()
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
}
