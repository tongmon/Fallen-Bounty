using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    // ����� ������ ���Ѽ�
    public int m_hero_limit;

    // ī�� ������ ���� ���Ѽ�
    public int m_card_option_limit;

    // ������ ���� ���� ����
    public List<GameObject> m_item;
    public int m_item_limit;

    public CardManager m_card_manager;
    public CardHolder m_card_holder;

    public HeroManager m_hero_manager;
    public HeroHolder m_hero_holder;

    private void Awake()
    {
        m_hero_limit = 1;
        m_card_option_limit = 3;
        m_item_limit = 4;
        m_item = new List<GameObject>();
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
    public void ThrowItem(ItemInfo item)
    {
        GameObject obj = Instantiate(new GameObject(), Input.mousePosition, Quaternion.Euler(0,0,0));
        obj.AddComponent<Rigidbody2D>();
        obj.GetComponent<Rigidbody2D>().isKinematic = true; 
        obj.AddComponent<CircleCollider2D>();
        obj.GetComponent<CircleCollider2D>().isTrigger = true;
        obj.transform.localScale = new Vector2(item.m_range, item.m_range);//���� ����
        obj.transform.tag = "Item";//�浹�ϴ� �ֵ��� ���� �˻�
        Destroy(obj, item.m_duration);//���ӽð� ���� ����
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
