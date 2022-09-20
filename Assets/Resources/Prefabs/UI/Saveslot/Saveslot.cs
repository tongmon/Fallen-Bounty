using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Saveslot : MonoBehaviour
{
    //두트윈할 타이틀
    [SerializeField] GameObject m_title_name;

    //총 3개짜리 세이브 버튼
    [SerializeField] GameObject[] save_button;

    [SerializeField] GameObject save_name;

    GameObject obj;//선택될 오브젝트

    Vector2 obj_vec;

    //저장을 위한 직렬화 클래스 
    SaveState save_state;

    private void Start()
    {
        m_title_name.transform.DOMoveY(3, 1.5f); //시작시 제목 이동 ,두트윈 이용
    }
    public void ButtonClicked()
    {
        obj = EventSystem.current.currentSelectedGameObject; //클릭한 객체 저장
        StartCoroutine(ButtonDoTween(obj)); //코루틴호출
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //끄기버튼
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
        EditorUtility.SetDirty(save_state);//이거하면 겜꺼도 저장됨.
#endif
        save_name.name = "SaveFile" + obj.name;
        DontDestroyOnLoad(save_name);

        save_state.last_playtime = System.DateTime.Now;

        obj.GetComponentInParent<Canvas>().sortingOrder = 1; //각 레이어를 구분해 맨앞으로 보내기
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
        obj.GetComponentInParent<Canvas>().sortingOrder = 0; //각 레이어를 구분해 맨앞으로 보내기
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