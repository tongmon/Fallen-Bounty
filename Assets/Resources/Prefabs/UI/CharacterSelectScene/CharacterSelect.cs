using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Image m_skill_button1;
    [SerializeField] Image m_skill_button2;
    [SerializeField] Image m_skill_button3;
    [SerializeField] Image m_skill_button4;


    //���1 ĳ���ͳ� ���� �����ͼ� ��ų�� ���
    //���2 �׳� ���⼭ �� �����ϰ� ���������� �ٸ��� ���
    public void CharacterClicked() 
    {

    }
    public void StartButtonClicked()//ĳ���� ���õ��� ����.
    {
        SceneManager.LoadScene("Map_Scene");
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Title_Scene");
    }
}
