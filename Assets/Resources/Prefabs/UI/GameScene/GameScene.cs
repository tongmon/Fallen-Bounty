using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameScene : MonoBehaviour
{
    [SerializeField] GameObject m_map_obj;
    [SerializeField] GameObject m_pause_panel; //이 패널이 먼저클릭되야하므로 새로운 컨버스의 패널이용
    [SerializeField] GameObject m_confirm_panel;
    [SerializeField] GameObject[] m_item; //아이템 리스트
    [SerializeField] GameObject[] m_reward_card; //스테이지 클리어시 등장하는 보상 카드리스트
    [SerializeField] GameObject[] m_panalty_card;//패널티 카드 리스트
    [SerializeField] GameObject m_back_button;//스테이지 종료후 돌아가기버튼
    GameObject m_selected_reward_card;//내가 선택한 보상
    GameObject m_selected_panalty_card;//내가 선택한 패널티

    List <MapNode> m_node;

    bool m_toggle = false;//일시정지 토글용 부울변수
    bool m_reward_selected = false;//보상 선택유무 부울변수
    bool m_panalty_selected = false;//패널티 선택유무 부울변수

    float m_angle = 0;//애니메이션용 각도변수
    float m_game_speed = 1.0f;//게임속도 저장용 변수 

    private void Start()
    {
        m_node = JsonParser.LoadJsonArrayToList<MapNode>("Assets/Resources/MapJson/MapJson");
    }
    void Update()
    {
        if (m_reward_selected) //보상선택시 선택안된애들 날아가는 회전 애니메이션
        {
            m_angle += 20f;
            m_angle %= 360;
            for (int i = 0; i < m_reward_card.Length; i++)
            {
                if (m_reward_card[i] != m_selected_reward_card)
                {
                    m_reward_card[i].transform.rotation = Quaternion.Euler(0, 0, m_angle);
                    m_reward_card[i].transform.DOScale(0, 1.2f);//1.2초안에 사라짐
                }
            }
        }
        if (m_panalty_selected)//패널티 선택시 위와같음
        {
            m_angle += 20f;
            m_angle %= 360;
            for (int i = 0; i < m_panalty_card.Length; i++)
            {
                if (m_panalty_card[i] != m_selected_panalty_card)
                {
                    m_panalty_card[i].transform.rotation = Quaternion.Euler(0, 0, m_angle);
                }
            }
        }
    }
    public void PauseButton() //일시정지버튼
    {
        m_pause_panel.SetActive(true);//정지시 상호작용 패널
        Time.timeScale = 0.0f;//타임스케일 0이 정지
    }
    public void DoubleTime()
    {
        if (Time.timeScale != 2.0f) m_game_speed = 2.0f;
        //이번엔 2배로조정하고 저장. 이유는 일시정지 풀었을시 2배로 실행해야해서
        else m_game_speed = 1.0f;
        Time.timeScale = m_game_speed;
    }
    public void GiveUp()
    {
        m_confirm_panel.SetActive(true);
    }
    public void BackToGameButton()//다시 게임으로 돌아가기, 즉 일시정지 해제
    {
        Time.timeScale = m_game_speed;//내 게임속도 반영
        m_pause_panel.SetActive(false);
    }
    public void BackToPause()//다시 일시정지로 돌아가기
    {
        m_confirm_panel.SetActive(false);
    }
    public void GiveUpYes()//json 삭제하고 나가기.
    {
        Time.timeScale = 1.0f;//타임스케일 0이 정지여서 풀어줘야 작동함
        System.IO.File.Delete("Assets/Resources/MapJson/MapJson.json");
        SceneManager.LoadScene("Saveslot_Scene");
        Destroy(GameObject.FindGameObjectWithTag("MapType"));
        Destroy(GameObject.FindGameObjectWithTag("SaveFileName"));
    }
    public void ItemButton()//아이템 누르기
    {
        if (!m_toggle) {
            StartCoroutine(ItemOpen());
            m_toggle = true;
        }
        else {
            StartCoroutine(ItemRollBack());
            m_toggle = false;
        }
    }
    IEnumerator ItemOpen()
    {
        yield return 0;
        for (int i = 0; i < m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i, 0.8f);//지정위치로 나열하기 두트윈
        }
        for (int i = 0; i < m_item.Length-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = true;
        }
    }
    IEnumerator ItemRollBack()
    {
        yield return 0;
        for (int i = 0; i < m_item.Length; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 10 * i, 0.8f);//다시 원래위치로 돌아가기 두트윈
        }
        for (int i = 0; i < m_item.Length-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = false;
        }
    }
    public void ItemUse()//아이템선택시 상호작용 , 미결
    {

    }
    public void RewardSelect()//보상선택 코루틴호출
    {
        StartCoroutine(RewardCardMove());
    }
    public void PanaltySelect()//패널티선택 코루틴호출
    {
        StartCoroutine(PanaltyCardSpawn());
    }
    IEnumerator RewardCardMove()
    {
        m_selected_reward_card = EventSystem.current.currentSelectedGameObject;//선택한것 보상 저장
        m_reward_selected = true;//회전시작
        m_selected_reward_card.transform.DOMove(new Vector2(0, 0), 0.4f);//가운데로 이동
        yield return new WaitForSecondsRealtime(0.4f);
        m_selected_reward_card.transform.DOScale(1.5f, 0.4f);//커졌다가
        yield return new WaitForSecondsRealtime(0.3f);
        m_selected_reward_card.transform.DOScale(0, 0.4f);//다시줄어들기
        yield return new WaitForSecondsRealtime(0.4f);
        m_reward_selected = false;
        for (int i = 0; i < m_panalty_card.Length; i++) //패널티 나타나기
        {
            m_panalty_card[i].transform.DOScale(1, 0.6f);
        }
        StopCoroutine(RewardCardMove());
    }
    IEnumerator PanaltyCardSpawn()
    {
        m_angle = 0;//각도 0으로 초기화시켜야함 안그러면 일정값으로 시작
        m_selected_panalty_card = EventSystem.current.currentSelectedGameObject;//다시 선택한것 패널티저장
        m_panalty_selected = true;
        for (int i = 0; i < m_panalty_card.Length; i++)
        {
            if(m_panalty_card[i] != m_selected_panalty_card) m_panalty_card[i].transform.DOScale(0, 0.6f);//보상과 마찬가지~
        }
        m_selected_panalty_card.transform.DOMove(new Vector2(0, 0), 0.4f);
        yield return new WaitForSecondsRealtime(0.4f);
        m_selected_panalty_card.transform.DOScale(1.5f, 0.4f);
        yield return new WaitForSecondsRealtime(0.3f);
        m_selected_panalty_card.transform.DOScale(0, 0.4f);
        m_panalty_selected = false;
        yield return new WaitForSecondsRealtime(0.4f);
        m_back_button.transform.DOScale(1, 0.5f);
        StopCoroutine(PanaltyCardSpawn());
    }

    public void BackToMap()//보상선택후 돌아가기
    {
        for (int i = 0; i < m_map_obj.transform.childCount; i++)
        {
            m_map_obj.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        if (m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num.Count > 1)
        {
            m_map_obj.transform.GetChild(m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num[1]).GetComponent<Button>().interactable = true;
        }
        m_map_obj.transform.GetChild(m_node[int.Parse(GameObject.FindGameObjectWithTag("MapType").transform.GetChild(0).name)].m_child_num[0]).GetComponent<Button>().interactable = true;
        Destroy(GameObject.FindGameObjectWithTag("MapType"));
        SceneManager.LoadScene("Map_Scene");
    }
}

