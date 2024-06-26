using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameScene : FadeInOut
{
    [SerializeField] GameObject m_map_obj;

    [SerializeField] GameObject m_pause_panel; 

    [SerializeField] GameObject m_confirm_panel;

    //아이템 리스트
    [SerializeField] List <GameObject> m_item; 

    //스테이지 클리어시 등장하는 보상 카드리스트
    [SerializeField] GameObject[] m_reward_card; 

    //패널티 카드 리스트
    [SerializeField] GameObject[] m_panalty_card;

    //스테이지 종료후 돌아가기버튼
    [SerializeField] GameObject m_back_button;

    //아이템 인포
    [SerializeField] ItemInfo[] m_itemInfos;

    //스킬칸
    [SerializeField] GameObject[] m_skills;

    [SerializeField] GameObject m_skill_range;
    //내가 선택한 보상
    GameObject m_selected_reward_card;

    //내가 선택한 패널티
    GameObject m_selected_panalty_card;

    //연결할 플레이어
    GameObject m_player;
    
    //맵 노드
    public List <MapNode> m_node;

    //마우스 오버 검사레이
    GraphicRaycaster m_gr;
    //마우스 포인터 데이터
    PointerEventData m_ped;

    //일시정지 토글용 부울변수
    bool m_toggle = false;

    //보상 선택유무 부울변수
    bool m_reward_selected = false;

    //패널티 선택유무 부울변수
    bool m_panalty_selected = false;

    //애니메이션용 각도변수
    float m_angle = 0;

    //게임속도 저장용 변수 
    float m_game_speed = 1.0f;

    private void Start()
    {
        m_player = GameObject.Find("Player");      
        m_gr = GameObject.Find("PrefabCanvas").GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);

        for(int i =0; i < m_player.GetComponent<Player>().m_hero_holder.m_hero_limit; i++)//버튼 초기화
        {
            for(int j= 0; j< m_player.GetComponent<Player>().m_hero_holder.m_heroes[i].abilities.Count; j++)
            {
                m_skills[i].transform.GetChild(j).GetComponent<Button>().interactable = true;
                m_skills[i].transform.GetChild(j).GetComponent<Image>().raycastTarget = true;
            }
        }

        SkillSet();

        StartCoroutine(OnStart());
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
        StartCoroutine(OnGiveUpYes());
    }
    public void ItemButton()//아이템 펼치기
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
    public void ItemUse()//아이템선택시 상호작용, 일시적인거고 스킬처럼 활용해야함.
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        
        m_player.GetComponent<Player>().ActivateItem(m_itemInfos[int.Parse(obj.name)]);
    }

    public void SkillSet()//포문을 쓰면 안됨. 이유 : 버튼누를때 다시 리스너 넣는줄에 들어와서 인덱스값이 바뀜.
    {
        foreach (Hero hero in m_player.GetComponent<Player>().m_hero_holder.m_heroes)
        {
            foreach (GameObject btn in m_skills)
            {
                btn.name = hero.name;
                btn.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => hero.abilities[0].Activate(hero.gameObject));
                btn.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => hero.abilities[1].Activate(hero.gameObject));
                btn.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => hero.abilities[2].Activate(hero.gameObject));
                btn.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => hero.abilities[3].Activate(hero.gameObject));
            }
        }
    }
    public void SkillRangeOn()//스킬 범위 표시
    {
        int index;
        m_ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_gr.Raycast(m_ped, results);
        m_skill_range.transform.position = GameObject.Find(results[0].gameObject.transform.parent.name).transform.position;//이름으로 캐릭터 위치 찾기
        if (m_skills[0].activeSelf) index = 0;
        else if (m_skills[1].activeSelf) index = 1;
        else if (m_skills[2].activeSelf) index = 2;
        else index = 3;
        float const1 = m_player.transform.GetChild(index).GetComponent<Hero>().abilities[int.Parse(results[0].gameObject.name)].m_base_range;
        m_skill_range.transform.localScale = new Vector3(const1, const1, const1);
        m_skill_range.gameObject.SetActive(true);
    }
    public void SkillRangeOff()
    {
        m_skill_range.gameObject.SetActive(false);
    }
    public void RewardSelect()//보상선택 코루틴호출
    {
        StartCoroutine(RewardCardMove());
    }
    public void PanaltySelect()//패널티선택 코루틴호출
    {
        StartCoroutine(PanaltyCardSpawn());
    }
    public void BackToMap()//보상선택후 돌아가기
    {
        StartCoroutine(OnBackToMap());
    }
    IEnumerator ItemOpen()
    {
        yield return 0;
        for (int i = 0; i < m_item.Count; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 110 * i, 0.8f);//지정위치로 나열하기 두트윈
        }
        for (int i = 0; i < m_item.Count-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = true;
        }
    }
    IEnumerator ItemRollBack()
    {
        yield return 0;
        for (int i = 0; i < m_item.Count; i++)
        {
            m_item[i].transform.DOLocalMoveX(-880 + 10 * i, 0.8f);//다시 원래위치로 돌아가기 두트윈
        }
        for (int i = 0; i < m_item.Count-1; i++)
        {
            m_item[i].GetComponent<Button>().interactable = false;
        }
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

    IEnumerator OnStart()
    {
        yield return null;
        FadeInM();
        //m_node = JsonParser.LoadJsonArrayToList<MapNode>("Assets/Resources/MapJson/MapJson");
    }
    
    IEnumerator OnBackToMap()
    {
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.0f);
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

    IEnumerator OnGiveUpYes()
    {
        Time.timeScale = 1.0f;//타임스케일 0이 정지여서 풀어줘야 작동함
        FadeOutForScene();
        yield return new WaitForSecondsRealtime(1.1f);

        m_node[40].m_num = 0;//이거로 초기화.
        //System.IO.File.Delete("Assets/Resources/MapJson/MapJson.json");
        SceneManager.LoadScene("Saveslot_Scene");
        Destroy(GameObject.FindGameObjectWithTag("MapType"));
        Destroy(GameObject.FindGameObjectWithTag("SaveFileName"));
    }
    
}

