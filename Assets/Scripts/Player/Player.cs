using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    [SerializeField] List <GameObject> m_items;
    // ����� ������ ���Ѽ�
    public int m_hero_limit;

    // ī�� ������ ���� ���Ѽ�
    public int m_card_option_limit;

    public int m_item_count;

    public CardManager m_card_manager;
    public CardHolder m_card_holder;

    public HeroManager m_hero_manager;
    public HeroHolder m_hero_holder;

    private void Awake()
    {
        m_hero_limit = 1;
        m_card_option_limit = 3;
        m_item_count = 0;
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
            StopCoroutine(CancelItem());
        }   
    }
    public void ItemUse(ItemInfo item)
    {

    }
    public void AddItem(ItemInfo item)//q
    {
        if(m_item_count < 4)
        {
            //m_items.
        }
    }
    public void ThrowItem(ItemInfo item)
    {
        GameObject obj = Instantiate(new GameObject(), Input.mousePosition, Quaternion.Euler(0,0,0));
        StopCoroutine(DropItem(obj, item));
    }
    IEnumerator DropItem(GameObject obj,ItemInfo item)
    {
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonDown(0)) break;
            obj.AddComponent<Rigidbody2D>();
            obj.GetComponent<Rigidbody2D>().isKinematic = true;
            obj.AddComponent<CircleCollider2D>();
            obj.GetComponent<CircleCollider2D>().isTrigger = true;
            obj.name = item.m_damage.ToString();//�̸��� �������� ����
            obj.transform.localScale = new Vector2(item.m_range, item.m_range);//���� ����
            obj.transform.tag = "Item";//�浹�ϴ� �ֵ��� ���� �˻�
            Debug.Log("������ ��� : " + item.m_info);
            Destroy(obj, item.m_duration);//���ӽð� ���� ����
        }
    }
    IEnumerator CancelItem()
    {
        yield return null;
        StopCoroutine("DropItem");
    }
}
