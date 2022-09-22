using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    //Json 클래스
    SaveState saveState;

    [SerializeField] GameObject skill;
    //캐릭터 리스트
    [SerializeField] Button[] m_char_list;

    [SerializeField] Button select_button;

    [SerializeField] Button revoke_button;

    [SerializeField] Button start_button;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    [SerializeField] Player player;

    List <GameObject> holded_hero;

    GUIStyle style = new GUIStyle();//IMGUI에 스타일 넣기용

    int cnt = 0;

    void Start()
    {
        style.fontSize = 32;
        style.normal.textColor = Color.white;
    }

    public void OnEnable()//IMGUI필요할듯
    {
        saveState = Resources.Load<SaveState>("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        holded_hero = new List<GameObject>();
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].interactable = true;
        }
    }
    private void Update()
    {
        if (cnt > player.m_hero_holder.m_hero_limit)
        {
            start_button.interactable = false;
        }
        else
        {
            start_button.interactable = true;
        }
        if(cnt == 0)
        {
            revoke_button.GetComponent<Button>().interactable = false;
            select_button.GetComponent<Button>().interactable = false;
        }
        else
        {
            revoke_button.GetComponent<Button>().interactable = true;
            select_button.GetComponent<Button>().interactable = true;
        }
    }


    public void SelectHero()
    {
        if (cnt < player.m_hero_holder.m_hero_limit) {

            holded_hero.Add(EventSystem.current.currentSelectedGameObject);
            skill.transform.GetChild(int.Parse(EventSystem.current.currentSelectedGameObject.name)).gameObject.SetActive(true);//선택된 객체
            for (int i = 0; i < skill.transform.childCount; i++)
            {
                if (i != int.Parse(EventSystem.current.currentSelectedGameObject.name))
                {
                    skill.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            cnt++;
            select_button.interactable = false;
        }
        start_button.GetComponent<Button>().interactable = true;
    }

    public void RevokeSelect()//되돌리기
    {
        if(cnt > 0)
        {
            holded_hero[cnt-1].GetComponent<Button>().interactable = true;
            holded_hero.RemoveAt(cnt-1);
            cnt--;
        }
    }

    public void CancelSelect()//선택 취소
    {
        for (int i = 0; i < holded_hero.Count; i++)
        {
            holded_hero[i].GetComponent<Button>().interactable = true;
            player.m_hero_holder.RemoveHero(player.m_hero_manager.m_heroes[int.Parse(holded_hero[i].name)]);
            holded_hero.Remove(holded_hero[i]);
        }
        select_button.interactable = true;
    }

    public void CompleteSelect()//아이디어 다 선택한후 가져오기.
    {
        for(int i=0;i<holded_hero.Count;i++)
        {
            player.m_hero_holder.AddHero(player.m_hero_manager.m_heroes[int.Parse(holded_hero[i].name)]);
        }
        revoke_button.interactable = true;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(350, 960, 200, 100),"캐릭터 제한수 : " + player.m_hero_holder.m_hero_limit, style);
    }
}
