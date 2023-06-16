using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalSetting : FadeInOut
{
    //세이브파일
    SaveState saveState;

    //플레이어
    Player player;

    private int char_index;

    //아이템박스
    [SerializeField] GameObject[] m_box;

    //스킬창 박스
    [SerializeField] GameObject m_book_shelf;

    //캐릭터 4개
    [SerializeField] GameObject[] m_char;

    //캐릭터이름
    [SerializeField] GameObject m_hero_name;

    //캐릭터 변수들
    [SerializeField] GameObject m_hero_status;

    //기초 위치 저장용
    Vector3 []m_pos = new Vector3[10];

    //캐릭터 스텟창
    [SerializeField] Image m_stat_panel;

    //스왑 버튼
    [SerializeField] Button[] m_swap_button;

    //페이드 아웃 이미지
    [SerializeField] Image FadeInOut;

    //선택종료 버튼
    [SerializeField] Button m_start_button;

    Vector3[] m_target_vec_two = { new Vector3(0, 225f, 0), new Vector3(0, 75f, 0) };

    Vector3[] m_target_vec_three = { new Vector3(200, 225f, 0), new Vector3(-200, 225f, 0), new Vector3(0, 75f, 0) };

    Vector3[] m_target_vec_four = { new Vector3(200, 150f,0), new Vector3(0, 225f,0),
        new Vector3(-200, 150f,0), new Vector3(0, 75f,0) };

    private void Awake()//위치 저장 및 세이브파일 가져옴
    {
        char_index = 0;//스킬박스안에 인덱스로 찾기위함.
        saveState = (SaveState)Resources.Load("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName"));

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//플레이어 참조

        OnStart(player.m_hero_holder.m_hero_limit);

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
        
        StartCoroutine(Box());//코루틴 이용
        StartCoroutine(BookShelf());
        StartCoroutine(StatPanel());
        m_start_button.GetComponent<Image>().DOColor(Color.white, 1.0f);//점차 나타나는 두트윈
    }

    public void OnStart(int index)//스왑버튼 끄기 및 캐릭터 비활성화.
    {
        if(index == 1)
        {
            m_swap_button[0].interactable = false;
            m_swap_button[1].interactable = false;
        }
        for(int i = index; i <m_char.Length; i++)
        {
            m_char[i].SetActive(false);
        }
        if (index == 2)
        {
            m_char[0].transform.localPosition = new Vector3(0, 75f, 0);
            m_char[1].transform.localPosition = new Vector3(0, 225f, 0);
        }
        else if (index == 3) 
        { 
            m_char[0].transform.localPosition = new Vector3(0, 75f, 0);
            m_char[1].transform.localPosition = new Vector3(200, 225f, 0);
            m_char[2].transform.localPosition = new Vector3(-200, 225f, 0);
        }
    }
    public void StartButtonClicked()//캐릭터 선택된후 시작.
    {
        StartCoroutine(StartScene());
    }
    public void CharSwapRight()
    {
        int index = player.m_hero_holder.m_hero_limit;//인덱스(히어로 수)로 각 위치마다 다르게 해야함.
        GameObject temp = m_char[index-1];
        if(char_index == 0)
        {
            char_index = index;
        }
        else char_index--;

        for (int i = index-1; i >= 0; i--) //오른쪽으로 위치변경
        {
            if(index == 2) m_char[i].transform.DOLocalMove(m_target_vec_two[i], 0.4f);
            else if(index == 3) m_char[i].transform.DOLocalMove(m_target_vec_three[i], 0.4f);
            else m_char[i].transform.DOLocalMove(m_target_vec_four[i], 0.4f);

            if (i == 0)
            {
                m_char[i] = temp;
            }
            else m_char[i] = m_char[i - 1];

            //위치변경으로 캐릭터 순서도 변경, 0이 자기자신
        }
    }
    public void CharSwapLeft()
    {
        int index = player.m_hero_holder.m_hero_limit;
        GameObject temp = m_char[0];
        if (char_index == index)
        {
            char_index = 0;
        }
        else char_index++;

        for (int i = 0; i < index; i++)//왼쪽으로 위치변경
        {
            if(index == 2) m_char[i].transform.DOLocalMove(m_target_vec_two[(i) % index], 0.4f);//둘다 한번 멈췄다감
            else if(index == 3) m_char[i].transform.DOLocalMove(m_target_vec_three[(i + 1) % index], 0.4f);//얘도
            else m_char[i].transform.DOLocalMove(m_target_vec_four[(i + 2) % index], 0.4f);

            if (i == index-1) m_char[i] = temp;
            else m_char[i] = m_char[i + 1];
        }
    }   
    public void StartButton()//시작하기버튼
    {
        StartCoroutine(StartGame());
    }
    IEnumerator StartScene()//게임시작
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
    IEnumerator StartGame()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("Map_Scene");//다음씬으로 이동
    }
}
