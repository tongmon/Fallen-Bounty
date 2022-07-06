using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    SaveState save_state;
    [SerializeField] Text m_title_name_text; //제목 글씨
    public void OnEnable()
    {
        m_title_name_text.DOText("Half Blood", 2.0f); //글씨가 써지는 두트윈 적용
        save_state = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").transform.name);
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
        System.TimeSpan time = System.DateTime.Now - save_state.last_playtime;
        save_state.playtime = time.Minutes;
    }
}
