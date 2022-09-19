using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    //Json Ŭ����
    SaveState saveState;

    [SerializeField] GameObject skill;
    //ĳ���� ����Ʈ
    [SerializeField] Image[] m_char_list;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    [SerializeField] Player player;

    public void Start()
    {
        //player.m_hero_manager 
        //���,��Ÿ�� �� ��ų���� �����;���.
    }
    public void OnEnable()
    {
        saveState = Resources.Load<SaveState>("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //���� ĳ���� ������ ����.
    {
        skill.transform.GetChild(int.Parse(EventSystem.current.currentSelectedGameObject.name)).gameObject.SetActive(true);//���õ� ��ü
        for(int i=0; i< skill.transform.childCount; i++)
        {
            if(i != int.Parse(EventSystem.current.currentSelectedGameObject.name))
            {
                skill.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
