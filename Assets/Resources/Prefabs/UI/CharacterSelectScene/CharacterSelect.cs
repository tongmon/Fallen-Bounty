using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Image m_skill_button1;
    [SerializeField] Image m_skill_button2;
    [SerializeField] Image m_skill_button3;
    [SerializeField] Image m_skill_button4;

    [SerializeField] Image FadeInOut;

    //���1 ĳ���ͳ� ���� �����ͼ� ��ų�� ���
    //���2 �׳� ���⼭ �� �����ϰ� ���������� �ٸ��� ���
    public void CharacterClicked() 
    {

    }
    public void StartButtonClicked()//ĳ���� ���õ��� ����.
    {
        StartCoroutine(StartScene());
    }
    IEnumerator StartScene()
    {
        FadeInOut.gameObject.SetActive(true);
        FadeInOut.DOColor(Color.black, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Map_Scene");
    }
}
