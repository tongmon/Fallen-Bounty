using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Queue <ItemInfo> m_items;

    public List <Hero> m_heroes; 
    // ����� ������ ���Ѽ�
    public int m_hero_limit;

    // ī�� ������ ���� ���Ѽ�
    public int m_card_option_limit;

    // ������ ���� ����
    public int m_item_count;

    Coroutine c_coroutine;

    public CardManager m_card_manager;
    public CardHolder m_card_holder;

    public HeroManager m_hero_manager;
    public HeroHolder m_hero_holder;
   

    private void Awake()
    {
        m_hero_limit = 1;
        m_card_option_limit = 3;
        m_item_count = 0;
        DontDestroyOnLoad(gameObject);//�׽�����
        for(int i = 0; i < m_hero_limit; i++) m_heroes.Add(transform.GetChild(i).GetComponent<Hero>());
        /*
        // ����� �ʱ�ȭ
        m_hero_manager = new HeroManager(gameObject);
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Hero");
        for (int i = 0; i < heros.Length; i++)
        {
            heros[i].GetComponent<Hero>().m_data = m_hero_manager.GetHero(heros[i].name);        
        }
        */
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StopCoroutine(c_coroutine);//�ڷ�ƾ ������ �ڷ�ƾ�� �����ؾ���.
        }   
    }
    public void AddItem(ItemInfo item)
    {
        if(m_item_count > 4)
        {
            m_items.Dequeue();
        }
        m_items.Enqueue(item);
    }
    public void ActivateItem(ItemInfo item)
    {
        if (item.m_type == "Attack")
        {
            GameObject obj = new GameObject();
            c_coroutine = StartCoroutine(item.Activation(obj, item));
        }
        else if (item.m_type == "Portion")
        {
            StartCoroutine(item.Activation(m_heroes,item));
            for(int i = 0; i<m_heroes.Count; i++)
            {
                StartCoroutine(m_heroes[i].GetComponent<Hero>().HitToolTip());
            }
        }
    }
}
