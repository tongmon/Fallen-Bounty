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


    //���1 ĳ���ͳ� ���� �����ͼ� ��ų�� ���
    //���2 �׳� ���⼭ �� �����ϰ� ���������� �ٸ��� ���
    public void CharacterClicked() 
    {

    }
    public void StartButtonClicked()//ĳ���� ���õ��� ����.
    {
        SceneManager.LoadScene("Map_Scene");
    }
}
