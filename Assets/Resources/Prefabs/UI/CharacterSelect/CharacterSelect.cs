using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    //Json Ŭ����
    SaveState saveState;

    //ĳ���� ����Ʈ
    [SerializeField] Image[] m_char_list;

    //���� ���
    Hero[] m_heroes;
    
    //��ų ��ư
    [SerializeField] Button[] m_skill_button;
    public void OnEnable()//json���� ��� ĳ���� ��������, �гο� ����ؾ���
    {
        saveState = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)//����� ĳ���� ���̰Բ�.
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //���� ĳ���� ������ ����.
    {

    }

}
