using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ItemInfo[] m_items;

    [SerializeField] Hero[] temp_heroes;

    public float m_all_stat_coefficent = 1.0f;

    // ī�� ������ ���� ���Ѽ�
    public int m_card_option_limit;

    //������ ��� �ڷ�ƾ �����
    Coroutine c_item_coroutine;

    public CardManager m_card_manager;
    public CardHolder m_card_holder;

    public HeroManager m_hero_manager;
    public HeroHolder m_hero_holder;
   
    public ItemHolder m_item_holder;
    public ItemManager m_item_manager;

    public int hero_index;

    private void Awake()
    {
        m_hero_holder = new HeroHolder(gameObject);
        m_hero_manager = new HeroManager(gameObject,temp_heroes);

        m_item_holder = new ItemHolder(gameObject);
        m_item_manager = new ItemManager(gameObject, m_items);

        m_card_option_limit = 3;

        DontDestroyOnLoad(gameObject);//�׽�����

        temp_heroes = null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && c_item_coroutine != null)
        {
            StopCoroutine(c_item_coroutine);//�ڷ�ƾ ������ �ڷ�ƾ�� �����ؾ���.
        }   
    }

    public void ActivateItem(ItemInfo item)
    {
        if (item.m_type == "Attack")
        {
            GameObject obj = new GameObject();
            c_item_coroutine = StartCoroutine(item.Activation(obj, item));
        }
        else if (item.m_type == "Portion")
        {
            StartCoroutine(item.Activation(m_hero_holder.m_heroes,item));
            for(int i = 0; i< m_hero_holder.m_heroes.Count; i++)
            {
                StartCoroutine(m_hero_holder.m_heroes[i].GetComponent<Hero>().HitToolTip());
            }
        }
    }
}
