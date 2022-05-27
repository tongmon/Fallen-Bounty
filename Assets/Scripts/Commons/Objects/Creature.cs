using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    #region Data from JSON file
    // ����ü �̸�
    public string m_name;
    // �����(ü��)
    public float m_health;
    // �¿� �ӵ�
    public float m_x_velocity;
    // ���� �ӵ�
    public float m_y_velocity;
    // 64bit, �ִ� 64���� �����̻�
    public long m_status_effect;
    // ���� ����
    public float m_magic_armor;
    // ���� ����
    public float m_physic_armor;
    #endregion

    // ��ų ���� ����
    public int m_abilities_limit;
    // ����ü�� ���ϴ� ����
    public Vector2 m_vec_direction;
    // ��ų Ȧ��
    public AbilityHolder m_ability_holder;
    // ����ü ���� ��Ŀ�� ��������Ʈ
    public SpriteRenderer m_sprite_seleted_circle;
    // ����ü ���� ����
    public bool m_selected;

    protected void Awake()
    {
        m_selected = false;
    }

    protected void Start()
    {
        
    }

    protected void Update()
    {
        
    }

    protected void FixedUpdate()
    {
        
    }
}
