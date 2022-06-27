using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogScene : MonoBehaviour
{
    [SerializeField] GameObject ItemButtonList;
    [SerializeField] GameObject CharacterButtonList;
    [SerializeField] GameObject StageButtonList;
    [SerializeField] GameObject ChallangesButtonList;
    [SerializeField] GameObject ClearButtonList;

    public void ItemButton()
    {
        ItemButtonList.SetActive(true);
        ChallangesButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        ClearButtonList.SetActive(false);
    }
    public void CharacterButton()
    {
        ItemButtonList.SetActive(false);
        ChallangesButtonList.SetActive(true);
        StageButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        ClearButtonList.SetActive(false);
    }
    public void StageButton()
    {
        ItemButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        StageButtonList.SetActive(true);
        ChallangesButtonList.SetActive(false);
        ClearButtonList.SetActive(false);
    }
    public void ChallengeButton()
    {
        ItemButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallangesButtonList.SetActive(true);
        ClearButtonList.SetActive(false);
    }
    public void ClearButton()
    {
        ItemButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        StageButtonList.SetActive(false);
        ChallangesButtonList.SetActive(false);
        ClearButtonList.SetActive(true);
    }
    public void BackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title_Scene");
    }
}
