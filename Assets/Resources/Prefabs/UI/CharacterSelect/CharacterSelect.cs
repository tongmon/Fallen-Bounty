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
    [SerializeField] Image[] m_char_list;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    [SerializeField] Player player;

    public void Start()
    {
        //player.m_hero_manager 
        //계수,쿨타임 등 스킬정보 가져와야함.
    }
    public void OnEnable()
    {
        saveState = Resources.Load<SaveState>("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //시작 캐릭터 설정을 하자.
    {
        skill.transform.GetChild(int.Parse(EventSystem.current.currentSelectedGameObject.name)).gameObject.SetActive(true);//선택된 객체
        for(int i=0; i< skill.transform.childCount; i++)
        {
            if(i != int.Parse(EventSystem.current.currentSelectedGameObject.name))
            {
                skill.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
