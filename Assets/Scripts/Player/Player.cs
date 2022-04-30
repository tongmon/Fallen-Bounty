using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ����� ������ ���Ѽ�
    public int m_hero_limit;

    // ī�� ������ ���� ���Ѽ�
    public int m_card_option_limit;

    public CardManager m_card_manager;
    public CardHolder m_card_holder;

    public HeroManager m_hero_manager;
    public HeroHolder m_hero_holder;

    private void Awake()
    {
        m_hero_limit = 1;
        m_card_option_limit = 3;
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
