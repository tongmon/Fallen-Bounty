using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;


public class Saveslot : MonoBehaviour
{
    //두트윈할 타이틀
    [SerializeField] GameObject m_title_name; 

    //총 3개짜리 세이브 버튼
    [SerializeField] GameObject[] save_button;

    //Don't destroy할 세이브파일 이름.
    public GameObject s_name;

    //저장을 위한 Json 클래스 
    SaveState save_state;

    List <Vector2> m_positon = new List<Vector2>();
    private void Awake()//다시 로드했을때 오류를 방지.
    {
        m_positon.Add(m_title_name.transform.localPosition);
        m_positon.Add(save_button[0].transform.localPosition);
        m_positon.Add(save_button[1].transform.localPosition);
        m_positon.Add(save_button[2].transform.localPosition);
    }
    private void OnEnable()
    {
        m_title_name.transform.localPosition = m_positon[0];
        for (int i = 0; i < 3; i++)
        {
            save_button[i].transform.localScale = new Vector3(1, 1, 1);
            save_button[i].GetComponentInParent<Canvas>().sortingOrder = 0;
            save_button[i].transform.localPosition = m_positon[i + 1];
        }
        m_title_name.transform.DOMoveY(3, 1.5f); //시작시 제목 이동 ,두트윈 이용
        GetComponent<RectTransform>();
    }
    public void ButtonClicked()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject; //클릭한 객체 저장
        StartCoroutine(ButtonDoTween(obj)); //코루틴호출
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //끄기버튼
        System.TimeSpan time = System.DateTime.Now - save_state.last_playtime;
        save_state.playtime = time.Minutes;
    }
    IEnumerator ButtonDoTween(GameObject obj)
    {
        string file_path = "Assets/Resources/SaveFileJson/";
        if (!File.Exists(file_path + "SaveFile" + obj.transform.name + "json"))//json이 없을때 새로 생성
        {
            save_state = new SaveState();
            save_state.last_playtime = System.DateTime.Now;
            JsonParser.CreateJsonFile(file_path + "SaveFile" + obj.transform.name + "json", JsonUtility.ToJson(save_state, true));
            s_name.transform.name = file_path + "SaveFile" + obj.transform.name + "json";
        }
        else//잇을때는 로드
        {
            save_state = JsonParser.LoadJsonFile<SaveState>(file_path + "SaveFile" + obj.transform.name + "json");
            s_name.transform.name = file_path + "SaveFile" + obj.transform.name + "json";

        }
        DontDestroyOnLoad(s_name);
        obj.GetComponentInParent<Canvas>().sortingOrder = 1; //각 레이어를 구분해 맨앞으로 보내기
        obj.transform.DOMoveX(0, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);

        obj.transform.DOScaleX(2.5f, 1.5f);
        obj.transform.DOScaleY(2.5f, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);

        GameObject.Find("EventSystem").GetComponent<CanvasManager>().TitleCanvasAdd();
    }
}
