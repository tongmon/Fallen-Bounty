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

    //캐릭터 리스트
    [SerializeField] Image[] m_char_list;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    //스킬 버튼
    [SerializeField] Button[] m_skill_button;
    public void OnEnable()//json으로 언락 캐릭터 가져오고, 패널에 출력
    {
        saveState = (SaveState)Resources.Load("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].DOColor(Color.white, 0.01f);
        }
    }
    
    public void CharacterClicked() //시작 캐릭터 설정을 하자.
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;//선택된 객체
        //HeroData hero_data = saveState.hdata_list[int.Parse(obj.name)];//나중에 Scriptable로 생성된거 연결해야함.


    }

}
