using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{
    // 히어로 소지량 제한수
    public int m_hero_limit;

    // 카드 선택지 개수 제한수
    public int m_card_option_limit;

    // 아이템 소지 갯수 제한
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
        // 히어로 초기화
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
        obj.transform.localScale = new Vector2(item.m_range, item.m_range);//범위 조정
        obj.transform.tag = "Item";//충돌하는 애들을 위한 검사
        Destroy(obj, item.m_duration);//지속시간 이후 삭제
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
