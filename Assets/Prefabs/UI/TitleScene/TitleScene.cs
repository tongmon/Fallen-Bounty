using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Text m_title_name_text;
    public void Start()
    {
        m_title_name_text.DOText("Fallen Bounty", 2.0f);
    }
    public void StartJourneyClicked()
    {
        SceneManager.LoadScene("Character_Select_Scene");
    }
    public void ContinueClicked()
    {
        SceneManager.LoadScene("");
    }
    public void OptionClicked()
    {
        SceneManager.LoadScene("Option_Scene");
    }
    public void LogClicked()
    {
        SceneManager.LoadScene("Log_Scene");
    }
    public void ExitClicked()
    {
        SceneManager.LoadScene("Saveslot_Scene");
    }
}