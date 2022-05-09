using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectScene : MonoBehaviour
{
    [SerializeField] Image m_skill_button1;
    [SerializeField] Image m_skill_button2;
    [SerializeField] Image m_skill_button3;
    [SerializeField] Image m_skill_button4;


    //방법1 캐릭터내 변수 가져와서 스킬에 출력
    //방법2 그냥 여기서 다 선언하고 누를때마다 다르게 출력
    public void CharacterClicked() 
    {

    }
    public void StartButtonClicked()//캐릭터 선택된후 시작.
    {
        SceneManager.LoadScene("Map_Scene");
    }
}
