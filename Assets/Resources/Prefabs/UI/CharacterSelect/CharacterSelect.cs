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

    //ĳ���� ����Ʈ
    [SerializeField] Image[] m_char_list;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    //��ų ��ư
    [SerializeField] Button[] m_skill_button;
    public void OnEnable()//json���� ��� ĳ���� ��������, �гο� ���
    {
        saveState = (SaveState)Resources.Load("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //���� ĳ���� ������ ����.
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;//���õ� ��ü
        //HeroData hero_data = saveState.hdata_list[int.Parse(obj.name)];//���߿� Scriptable�� �����Ȱ� �����ؾ���.


    }

}
