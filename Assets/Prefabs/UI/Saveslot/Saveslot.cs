using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.IO;
using LitJson;

public class Saveslot : MonoBehaviour
{
    //두트윈할 타이틀
    [SerializeField] private GameObject m_titleName;

    //총 3개짜리 세이브 버튼
    [SerializeField] private GameObject[] SaveButton;

    [SerializeField] private GameObject save_name;

    private GameObject obj;//선택될 오브젝트

    private Vector2 obj_vec;

    //저장을 위한 직렬화 클래스 
    private SaveState saveState;
    private string savePath;

    private void Start()
    {
        m_titleName.transform.DOMoveY(3, 1.5f); //시작시 제목 이동 ,두트윈 이용
        savePath = Application.streamingAssetsPath;
    }
    public void ButtonClicked()
    {
        obj = EventSystem.current.currentSelectedGameObject; //클릭한 객체 저장
        StartCoroutine(ButtonDoTween(obj)); //코루틴호출
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //끄기버튼
        System.TimeSpan time = System.DateTime.Now - saveState.last_playtime;
        saveState.playtime = time.Minutes;
    }
    public void TitleExitButton()
    {
        StartCoroutine(ExitButton());
    }
    IEnumerator ButtonDoTween(GameObject obj)
    {
        for (int i = 0; i < SaveButton.Length; i++) SaveButton[i].GetComponent<Button>().interactable = false;

        saveState = new SaveState();
        saveState.last_playtime = System.DateTime.Now;

        if (File.Exists(savePath + string.Format("Save/SaveState{0}.json",obj.name)))//몇번째 저장소인지
        {
            string json = File.ReadAllText(savePath + string.Format("Save/SaveState{0}.json", obj.name));
            JsonData data = JsonMapper.ToObject(json);
            saveState.JsonSetting(data);
        }
        else
        {
            string json = JsonMapper.ToJson(saveState);
            File.WriteAllText(savePath + string.Format("Save/SaveState{0}.json", obj.name), json);
        }

        save_name.name = "SaveFile" + obj.name;
        DontDestroyOnLoad(save_name);

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

        for(int i =0; i<SaveButton.Length; i++)
        {
            SaveButton[i].GetComponent<Button>().interactable = true;
        }
    }
}