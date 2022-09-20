using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Saveslot : MonoBehaviour
{
    //��Ʈ���� Ÿ��Ʋ
    [SerializeField] GameObject m_title_name;

    //�� 3��¥�� ���̺� ��ư
    [SerializeField] GameObject[] save_button;

    [SerializeField] GameObject save_name;

    GameObject obj;//���õ� ������Ʈ

    Vector2 obj_vec;

    //������ ���� ����ȭ Ŭ���� 
    SaveState save_state;

    private void Start()
    {
        m_title_name.transform.DOMoveY(3, 1.5f); //���۽� ���� �̵� ,��Ʈ�� �̿�
    }
    public void ButtonClicked()
    {
        obj = EventSystem.current.currentSelectedGameObject; //Ŭ���� ��ü ����
        StartCoroutine(ButtonDoTween(obj)); //�ڷ�ƾȣ��
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //�����ư
        System.TimeSpan time = System.DateTime.Now - save_state.last_playtime;
        save_state.playtime = time.Minutes;
    }
    public void TitleExitButton()
    {
        StartCoroutine(ExitButton());
    }
    IEnumerator ButtonDoTween(GameObject obj)
    {
        for (int i = 0; i < save_button.Length; i++) save_button[i].GetComponent<Button>().interactable = false;
        save_state = (SaveState)Resources.Load("SaveFile/SaveFile" + obj.name);
#if UNITY_EDITOR
        EditorUtility.SetDirty(save_state);//�̰��ϸ� �ײ��� �����.
#endif
        save_name.name = "SaveFile" + obj.name;
        DontDestroyOnLoad(save_name);

        save_state.last_playtime = System.DateTime.Now;

        obj.GetComponentInParent<Canvas>().sortingOrder = 1; //�� ���̾ ������ �Ǿ����� ������
        obj_vec = obj.transform.localPosition;
        obj.transform.DOMoveX(0, 0.8f);
        yield return new WaitForSecondsRealtime(0.8f);

        obj.transform.DOScaleX(2.7f, 0.8f);
        obj.transform.DOScaleY(3.2f, 0.8f);
        obj.transform.GetChild(0).GetComponent<Text>().DOColor(Color.white, 0.8f);
        obj.transform.GetChild(1).GetComponent<Image>().DOColor(Color.white, 0.8f);
        obj.transform.GetChild(2).GetComponent<Image>().DOColor(Color.white, 0.8f);
        obj.transform.GetChild(3).GetComponent<Image>().DOColor(Color.white, 0.8f);
        obj.transform.GetChild(4).GetComponent<Image>().DOColor(Color.white, 0.8f);
        
        yield return new WaitForSecondsRealtime(0.8f);
        obj.transform.GetChild(1).GetComponent<Button>().interactable = true;
        obj.transform.GetChild(2).GetComponent<Button>().interactable = true;
        obj.transform.GetChild(3).GetComponent<Button>().interactable = true;
        obj.transform.GetChild(4).GetComponent<Button>().interactable = true;
    }
    IEnumerator ExitButton()
    {
        obj.GetComponentInParent<Canvas>().sortingOrder = 0; //�� ���̾ ������ �Ǿ����� ������
        obj.transform.DOLocalMoveX(obj_vec.x, 0.8f);
        yield return new WaitForSecondsRealtime(0.8f);

        obj.transform.DOScaleX(1.0f, 0.8f);
        obj.transform.DOScaleY(1.0f, 0.8f);
        obj.transform.GetChild(0).GetComponent<Text>().DOFade(0, 0.8f);
        obj.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0.8f);
        obj.transform.GetChild(2).GetComponent<Image>().DOFade(0, 0.8f);
        obj.transform.GetChild(3).GetComponent<Image>().DOFade(0, 0.8f);
        obj.transform.GetChild(4).GetComponent<Image>().DOFade(0, 0.8f);

        yield return new WaitForSecondsRealtime(0.8f);
        obj.transform.GetChild(1).GetComponent<Button>().interactable = false;
        obj.transform.GetChild(2).GetComponent<Button>().interactable = false;
        obj.transform.GetChild(3).GetComponent<Button>().interactable = false;
        obj.transform.GetChild(4).GetComponent<Button>().interactable = false;

        for(int i =0; i<save_button.Length; i++)
        {
            save_button[i].GetComponent<Button>().interactable = true;
        }
    }
}