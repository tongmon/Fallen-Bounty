using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CharacterSelect : MonoBehaviour
{
    //Json Ŭ����
    SaveState saveState;

    [SerializeField] GameObject skill;
    //ĳ���� ����Ʈ
    [SerializeField] Button[] m_char_list;

    [SerializeField] Button select_button;

    [SerializeField] Button revoke_button;

    [SerializeField] Button start_button;

    [SerializeField] Text hero_name;

    [SerializeField] Text hero_info;

    [SerializeField] Player player;

    List <GameObject> holded_hero;

    GUIStyle style = new GUIStyle();//IMGUI�� ��Ÿ�� �ֱ��

    int cnt = 0;

    void Start()
    {
        style.fontSize = 32;
        style.normal.textColor = Color.white;
    }

    public void OnEnable()//IMGUI�ʿ��ҵ�
    {
        saveState = Resources.Load<SaveState>("SaveFile/" + GameObject.FindGameObjectWithTag("SaveFileName").name);
        holded_hero = new List<GameObject>();
        foreach (eCharacter Char in saveState.unlock_character)
        {
            m_char_list[(int)Char].interactable = true;
        }
    }
    private void Update()
    {
        if (cnt > player.m_hero_holder.m_hero_limit)
        {
            start_button.interactable = false;
        }
        else
        {
            start_button.interactable = true;
        }
        if(cnt == 0)
        {
            revoke_button.GetComponent<Button>().interactable = false;
            select_button.GetComponent<Button>().interactable = false;
        }
        else
        {
            revoke_button.GetComponent<Button>().interactable = true;
            select_button.GetComponent<Button>().interactable = true;
        }
    }


    public void SelectHero()
    {
        if (cnt < player.m_hero_holder.m_hero_limit) {

            holded_hero.Add(EventSystem.current.currentSelectedGameObject);
            skill.transform.GetChild(int.Parse(EventSystem.current.currentSelectedGameObject.name)).gameObject.SetActive(true);//���õ� ��ü
            for (int i = 0; i < skill.transform.childCount; i++)
            {
                if (i != int.Parse(EventSystem.current.currentSelectedGameObject.name))
                {
                    skill.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            cnt++;
            select_button.interactable = false;
        }
        start_button.GetComponent<Button>().interactable = true;
    }

    public void RevokeSelect()//�ǵ�����
    {
        if(cnt > 0)
        {
            holded_hero[cnt-1].GetComponent<Button>().interactable = true;
            holded_hero.RemoveAt(cnt-1);
            cnt--;
        }
    }

    public void CancelSelect()//���� ���
    {
        for (int i = 0; i < holded_hero.Count; i++)
        {
            holded_hero[i].GetComponent<Button>().interactable = true;
            player.m_hero_holder.RemoveHero(player.m_hero_manager.m_heroes[int.Parse(holded_hero[i].name)]);
            holded_hero.Remove(holded_hero[i]);
        }
        select_button.interactable = true;
    }

    public void CompleteSelect()//���̵�� �� �������� ��������.
    {
        for(int i=0;i<holded_hero.Count;i++)
        {
            player.m_hero_holder.AddHero(player.m_hero_manager.m_heroes[int.Parse(holded_hero[i].name)]);
        }
        revoke_button.interactable = true;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(350, 960, 200, 100),"ĳ���� ���Ѽ� : " + player.m_hero_holder.m_hero_limit, style);
    }
}
