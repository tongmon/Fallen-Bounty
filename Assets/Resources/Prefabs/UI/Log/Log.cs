using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    [SerializeField] GameObject ItemButtonList;
    [SerializeField] GameObject CharacterButtonList;
    [SerializeField] GameObject StageButtonList;
    [SerializeField] GameObject ChallengesButtonList;
    [SerializeField] GameObject ClearLogText;
    SaveState save_state;
    private void OnEnable()
    {
        //클래스 생성해서 각 인포 불러와야함
        save_state = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").transform.name);
        foreach(eItem item in save_state.unlock_item)
        {
            ItemButtonList.transform.GetChild((int)item).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        foreach (eCharacter character in save_state.unlock_character)
        {
            CharacterButtonList.transform.GetChild((int)character).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        foreach (eStage stage in save_state.unlock_character)
        {
            StageButtonList.transform.GetChild((int)stage).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        foreach (eChallenges challenges in save_state.unlock_character)
        {
            ChallengesButtonList.transform.GetChild((int)challenges).GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        /*string a= null;
        for(int i = 0; i<save_state.clear_log.Length; i++)
        {
            a.Insert(0, save_state.clear_log[i]);
        }
        ClearLogText.GetComponent<Text>().text = a; //적용되는지 모름
        */
    }

    public void ItemButton()
    {
        ItemButtonList.SetActive(true);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.transform.parent.parent.parent.gameObject.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void CharacterButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(true);
        StageButtonList.SetActive(false);
        ChallengesButtonList.transform.parent.parent.parent.gameObject.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void StageButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(true);
        ChallengesButtonList.transform.parent.parent.parent.gameObject.SetActive(false);
        ClearLogText.SetActive(false);
    }
    public void ChallengeButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.transform.parent.parent.parent.gameObject.SetActive(true);
        ClearLogText.SetActive(false);
    }
    public void ClearButton()
    {
        ItemButtonList.SetActive(false);
        CharacterButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallengesButtonList.transform.parent.parent.parent.gameObject.SetActive(false);
        ClearLogText.SetActive(true);
    }
}
