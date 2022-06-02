using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Hero), "physic_power")]
public class Creature : MonoBehaviour
{
    #region Data from JSON file
    // ����ü �̸�
    public string type_name;
    // �����(ü��)
    public float health;
    // �¿� �ӵ�
    public float x_velocity;
    // ���� �ӵ�
    public float y_velocity;
    // 64bit, �ִ� 64���� �����̻�
    public long status_effect;
    // ���� ����
    public float magic_armor;
    // ���� ����
    public float physic_armor;
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
