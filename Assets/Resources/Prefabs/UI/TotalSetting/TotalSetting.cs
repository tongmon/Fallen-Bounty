using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSetting : FadeInOut
{
    [SerializeField] GameObject[] m_box; //아이템박스
    [SerializeField] GameObject m_book_shelf;//스킬창 박스
    [SerializeField] GameObject[] m_char;//캐릭터 4개
    [SerializeField] GameObject m_hero_name;//캐릭터이름
    [SerializeField] GameObject m_hero_status;//캐릭터 변수들
    Vector3 []m_pos = new Vector3[10];


    [SerializeField] Image m_stat_panel;//캐릭터 스텟창
    [SerializeField] Image FadeInOut;
    [SerializeField] Button m_start_button;//선택종료 버튼
    Vector3[] m_target_vec = { new Vector3(2, 1.5f,100), new Vector3(0, 2.25f,100),
        new Vector3(-2, 1.5f,100), new Vector3(0, 0.75f,100) };//100을 넣어야 z값이 0으로 고정됨.
    //캐릭터 이동시 이용할 좌표들
    private void Awake()
    {
        for(int i = 0; i < m_pos.Length- 1; i++)
        {
            m_pos[i] = m_box[i].transform.localPosition;
        }
        m_pos[9] = m_book_shelf.transform.localPosition;
    }
    void OnEnable()
    {
        for (int i = 0; i < m_pos.Length - 1; i++)
        {
            m_box[i].transform.localPosition = m_pos[i];
        }
        m_book_shelf.transform.localPosition = m_pos[9];
        
        StartCoroutine(Box());//코루틴이용
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
        m_start_button.GetComponent<Image>().DOColor(Color.white, 1.0f);//점차 나타나는 두트윈
    }
    public void StartButtonClicked()//캐릭터 선택된후 시작.
    {
        StartCoroutine(StartScene());
    }
    IEnumerator StartScene()
    {
        FadeInOut.gameObject.SetActive(true);
        FadeInOut.DOColor(Color.black, 0.3f);
        yield return new WaitForSecondsRealtime(0.3f);

        SceneManager.LoadScene("Map_Scene");
    }
    IEnumerator Box() { //아이템창 랜덤으로 등장
        for(int i = 0; i<9; i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0, 0.3f));
            Vector3 target_vec = m_box[i].transform.position + new Vector3(5f, -1.5f, 0);
            m_box[i].transform.DORotate(new Vector3(0,0,180), 1);
            m_box[i].transform.DOMove(target_vec, 1);
        }
        StopCoroutine(Box());
    }
    IEnumerator BookShelf() //스킬창 나타나기
    {
        yield return 0;
        m_book_shelf.transform.DOMoveX(6f, 1);
        StopCoroutine(BookShelf());
    }
    IEnumerator StatPanel()//스텟 나타나기
    {
        yield return 0;
        m_stat_panel.DOColor(Color.white, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);//순차적으로 나타나기위해 시간을 둠
        m_hero_name.SetActive(true);
        m_hero_status.SetActive(true);
        StopCoroutine(StatPanel());
    }
    public void CharSwapRight()
    {
        GameObject temp = m_char[3];
        for (int i = 3; i >= 0; i--) //오른쪽으로 위치변경
        {
            m_char[i].transform.DOMove(m_target_vec[i], 0.8f);
            if (i == 0) m_char[i] = temp;
            else m_char[i] = m_char[i - 1];
            //위치변경으로 캐릭터 순서도 변경, 0이 자기자신
        }
    }
    public void CharSwapLeft() 
    {
        GameObject temp = m_char[0];
        for (int i = 0; i < 4; i++)//왼쪽으로 위치변경
        {
            m_char[i].transform.DOMove(m_target_vec[(i+2)%4], 0.8f);
            if (i == 3) m_char[i] = temp;
            else m_char[i] = m_char[i + 1];
            //위치변경으로 캐릭터 순서도 변경, 0이 자기자신
        }
    }
    public void StartButton()//시작하기버튼
    {
        StartCoroutine(StartGame());   
    }
    IEnumerator StartGame()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("Map_Scene");//다음씬으로 이동
    }
}
