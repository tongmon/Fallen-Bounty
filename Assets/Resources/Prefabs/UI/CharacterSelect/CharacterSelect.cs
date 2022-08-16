using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    Button m_skill_button1;
    Button m_skill_button2;
    Button m_skill_button3;
    Button m_skill_button4;
    SaveState saveState;

    public void OnEnable()//json으로 언락 캐릭터 가져오고, 패널에 출력해야함
    {
        
       
    }
    //방법1 캐릭터내 변수 가져와서 스킬에 출력
    //방법2 그냥 여기서 다 선언하고 누를때마다 다르게 출력
    public void CharacterClicked() 
    {

    }

}
