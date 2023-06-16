using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using LitJson;
using System.Windows.Forms.DataVisualization.Charting;

public class MouseOverPanel : MonoBehaviour
{
    //������ �г�
    [SerializeField] private Image m_panel;

    //�α� ĵ����
    [SerializeField] private Canvas m_log_canvas;

    //ĳ���� ���� ĵ����
    [SerializeField] private Canvas m_charSelect_canvas;

    //Json���� ��������.
    private List <ItemInfo> item_info;

    private List <StageInfo> stage_info;

    private List<ChallengeInfo> challenge_info;

    [SerializeField] private Player player;

    //ĵ������ ����
    private GraphicRaycaster[] m_gr;

    //������ �̺�Ʈ
    private PointerEventData m_ped;

    private string stagePath;
    private string challengePath;
    private string itemPath;

    private void Start()
    {
        m_gr = new GraphicRaycaster[2];
        m_gr[0] = m_log_canvas.GetComponent<GraphicRaycaster>();
        m_gr[1] = m_charSelect_canvas.GetComponent<GraphicRaycaster>();
        m_ped = new PointerEventData(null);

        stagePath = Application.streamingAssetsPath + "Log/Stage.json";
        challengePath = Application.streamingAssetsPath + "Log/Challenge.json";
        itemPath = Application.streamingAssetsPath + "Log/Item.json";

        item_info = new List<ItemInfo>();
        stage_info = new List<StageInfo>();
        challenge_info = new List<ChallengeInfo>();

        if (File.Exists(itemPath))
        {
            string json = File.ReadAllText(itemPath);
            JsonData data = JsonMapper.ToObject(json);

            for (int i = 0; i < data.Count; i++)
            {
                ItemInfo item = new ItemInfo();
                item.InfoSetting(i,data);
                item_info.Add(item);
            }
        }
        else
        {
            for(int i = 0; i< 23; i++)
            {
                ItemInfo item = new ItemInfo();
                switch (i)
                {
                    case 0 :
                        //item.spritePath ���� ���� �ʿ�.
                        item.m_name = i.ToString();
                        item.m_info = "Molotov";
                        //item.m_unlock_condition = ���� ��.
                        item.m_type = "Attack";
                        item.m_damage = 1;
                        item.m_range = 0;
                        item.m_duration = 3;
                        item.m_cooltime = 1;
                        break; 

                }
            }
        }

        if (File.Exists(stagePath))
        {
            string json = File.ReadAllText(stagePath);
            JsonData data = JsonMapper.ToObject(json);

            for(int i =0; i < data.Count; i++)
            {
                StageInfo stage = new StageInfo();
                stage.InfoSetting(i,data);
                stage_info.Add(stage);
            }
        }
        else
        {

        }
        if (File.Exists(challengePath))
        {
            string json = File.ReadAllText(challengePath);
            JsonData data = JsonMapper.ToObject(json);

            for (int i = 0; i < data.Count; i++)
            {
                ChallengeInfo challenge = new ChallengeInfo();
                challenge.InfoSetting(i,data);
                challenge_info.Add(challenge);
            }
        }
        else
        {

        }


    }
    public void MouseEnter()
    {
        //if (!EventSystem.current.currentSelectedGameObject.GetComponent<Hero>())
            //m_hero = EventSystem.current.currentSelectedGameObject.GetComponent<Hero>();
        StopCoroutine("MOut");
        StartCoroutine("MIn");
    }
    
    public void MouseOut()
    {
        StopCoroutine("MIn");
        StartCoroutine("MOut");
    }
 
    IEnumerator MIn()
    {
        for (int i = 0; i < m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(true);
        }
        yield return null;
        m_ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        if (m_log_canvas.gameObject.activeSelf) //�α׿����� ����
        {
            m_gr[0].Raycast(m_ped, results);
            if (results[0].gameObject.tag == "Item")
            {
                //��������Ʈ �߰� �� m_panel.transform.GetChild(0).GetComponent<Image>().sprite = item_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("���� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("�ر� ���� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("���ط� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_damage);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("���� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_range);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("�����ð� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_duration);
                m_panel.transform.GetChild(6).GetComponent<Text>().text = string.Format("��Ÿ�� : {0}", item_info[int.Parse(results[0].gameObject.transform.name)].m_cooltime);
            }
            else if (results[0].gameObject.tag == "Character")
            {   //����ο��� ���;���
                //m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", challenge_info[int.Parse(results[0].gameObject.transform..name)].m_name);
            }
            else if (results[0].gameObject.tag == "Stage")
            {
                //��������Ʈ �߰� �� m_panel.transform.GetChild(0).GetComponent<Image>().sprite = stage_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("���� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("�ر� ���� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("���� �̸� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_boss_name);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("Ŭ���� �ð� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_time);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("Ŭ���� Ƚ�� : {0}", stage_info[int.Parse(results[0].gameObject.transform.name)].m_clear_count);
            }
            else if (results[0].gameObject.tag == "Challenge")
            {
                //��������Ʈ �߰� ��m_panel.transform.GetChild(0).GetComponent<Image>().sprite = challenge_info[int.Parse(results[0].gameObject.transform..name)].m_sprite;
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("���� : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_info);
                m_panel.transform.GetChild(7).GetComponent<Text>().text = string.Format("�ر� ���� : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_unlock_condition);

                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("ȹ�淮 : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("��ǥ�� : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("�޼��� : {0}", challenge_info[int.Parse(results[0].gameObject.transform.name)].m_target_num / challenge_info[int.Parse(results[0].gameObject.transform.name)].m_total_num);
            }
        }
        else
        {
            m_gr[1].Raycast(m_ped, results);
            if (results[0].gameObject.tag == "Skill" && player.m_hero_manager.m_heroes[player.hero_index] != null)//ĳ���� ����â����
            {
                //0���� ���
                m_panel.transform.GetChild(1).GetComponent<Text>().text = string.Format("�̸� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_name);
                m_panel.transform.GetChild(2).GetComponent<Text>().text = string.Format("���� ��� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_base_physical_coefficient);
                m_panel.transform.GetChild(3).GetComponent<Text>().text = string.Format("���� ��� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_base_magic_coefficient);
                m_panel.transform.GetChild(4).GetComponent<Text>().text = string.Format("���� ���� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_base_range);
                m_panel.transform.GetChild(5).GetComponent<Text>().text = string.Format("���� ���� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_base_active_range);
                m_panel.transform.GetChild(6).GetComponent<Text>().text = string.Format("��Ÿ�� : {0}", player.m_hero_manager.m_heroes[player.hero_index].abilities[int.Parse(results[0].gameObject.name)].m_base_cooldown_time);
            }
        }
        m_panel.gameObject.SetActive(true);
        m_panel.DOColor(Color.white, 0.1f);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_panel.transform.position = pos * Camera.main.transform.localScale;
        m_panel.transform.localPosition = new Vector3(m_panel.transform.localPosition.x + 350, m_panel.transform.localPosition.y - 175, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.DOKill();//��Ʈ�� ����
    }
    IEnumerator MOut()
    {
        for(int i= 0; i< m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(false);
        }
        m_panel.DOFade(0, 0.1f);
        yield return new WaitForSecondsRealtime(0.1f);
        m_panel.gameObject.SetActive(false);
        for (int i = 0; i < m_panel.transform.childCount; i++)
        {
            m_panel.transform.GetChild(i).gameObject.SetActive(true);
        }
        m_panel.DOKill();
    }
}
