using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionScene : MonoBehaviour
{
    [SerializeField] GameObject AudioSetting;
    [SerializeField] GameObject ResolutionSetting;
    [SerializeField] GameObject GraphicSetting;
    [SerializeField] GameObject KeySetting;
    [SerializeField] GameObject GameplaySetting;
    void AudioButtonClicked()
    {

    }
    void ResolutionButtonClicked()
    {

    }
    void GraphicButtonClicked()
    {

    }
    void KeyboardButtonClicked()
    {

    }
    void GameplayButtonClicked()
    {

    }
    void BackButtonClicked()
    {
        SceneManager.LoadScene("Title_Scene");
    }
}
