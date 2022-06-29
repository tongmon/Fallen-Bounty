using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScene : MonoBehaviour
{
    [SerializeField] GameObject ItemButtonList;
    [SerializeField] GameObject CharacterButtonList;
    [SerializeField] GameObject StageButtonList;
    [SerializeField] GameObject ChallengesButtonList;
    [SerializeField] GameObject ClearLogText;
    SaveState save_state;
    private void Start()
    {
        save_state = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").transform.name);

        for(int i = 0; i<save_state.unlock_item.Count; i++)
        {
            ItemButtonList.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        for (int i = 0; i < save_state.unlock_character.Count; i++)
        {
            CharacterButtonList.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        for (int i = 0; i < save_state.unlock_stage.Count; i++)
        {
            StageButtonList.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        for (int i = 0; i < save_state.unlock_challenges.Count; i++)
        {
            ChallengesButtonList.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        string a= null;
        for(int i = 0; i<save_state.clear_log.Length; i++)
        {
            a.Insert(0, save_state.clear_log[i]);
        }
        ClearLogText.GetComponent<Text>().text = a; //적용되는지 모름
    }

    public void ItemButton()
    {
        ItemButtonList.SetActive(true);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void CharacterButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(true);
        StageButtonList.SetActive(false);
        ChallengesButtonList.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void StageButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(true);
        ChallengesButtonList.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void ChallengeButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.SetActive(true);
        ClearLogText.SetActive(false);
    }
    public void ClearButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.SetActive(false);
        ClearLogText.SetActive(true);
    }
    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Scene");
    }
}
