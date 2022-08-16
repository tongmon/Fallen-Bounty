using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    //Json 클래스
    SaveState saveState;

    //캐릭터 리스트
    [SerializeField] Image[] m_char_list;

    //영웅 목록
    Hero[] m_heroes;
    
    //스킬 버튼
    [SerializeField] Button[] m_skill_button;
    public void OnEnable()//json으로 언락 캐릭터 가져오고, 패널에 출력해야함
    {
        saveState = JsonParser.LoadJsonFile<SaveState>(GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)//언락된 캐릭터 보이게끔.
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //시작 캐릭터 설정을 하자.
    {

    }

}
