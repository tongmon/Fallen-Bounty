using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Log : MonoBehaviour
{
    //�� ����Ʈ
    [SerializeField] GameObject ItemButtonList;

    [SerializeField] GameObject CharacterButtonList;

    [SerializeField] GameObject StageButtonList;

    [SerializeField] GameObject ChallengesButtonList;

    [SerializeField] GameObject ClearLogText;

    SaveState save_state;
    private void OnEnable()
    {
        save_state = (SaveState)Resources.Load("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);//���� �̸����� ã��.

        foreach (eItem item in save_state.unlock_item)//Ȱ��ȭ�� �ֵ� �÷�����.
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
        ClearLogText.GetComponent<Text>().text = a; //����Ǵ��� ��
        */
    }

    public void ItemButton()//�� ��ư ��ȣ�ۿ�
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
