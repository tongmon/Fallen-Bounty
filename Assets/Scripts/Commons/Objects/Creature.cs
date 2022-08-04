using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(HeroData), "physic_power")]
public class CreatureData
{
    #region Data from JSON file
    // ����ü �̸�
    // [JsonProperty(PropertyName = "Type Name")] ���߿� ���� ���� �̷��� �ٲ� �� ����
    public string type_name;
    // �����(ü��)
    public float health;
    // ����ü �ӵ�
    public JsonVector2 velocity;
    // ����
    public float mass;
    // ���� ����
    public int magic_armor;
    // ���� ����
    public int physic_armor;
    #endregion
}

public class Creature : MonoBehaviour
{
    // JSON �Ľ��� �����Ͱ� ���, CreatureData, HeroData ��...
    public object m_data;

    // ��ų ���� ����
    public int m_abilities_limit;
    // ����ü�� ���ϴ� ����
    public Vector2 m_vec_direction;
    // ��ų Ȧ��
    public AbilityHolder m_ability_holder;
    // ����ü ���� ����
    public bool m_selected;

    // ĳ���� Ÿ����
    public Creature m_target;
    // Ÿ�� ��ġ
    public Vector2? m_point_target;

    //���� ü��
    public float m_current_health;
    //���� ����
    public float m_current_armor;
    //���� �������׷�
    public float m_current_magic_armor;

    // �Է� ó�� ������Ʈ
    public InputComponent m_input_component;
    // �׷��� ó�� ������Ʈ
    public GraphicsComponent m_graphics_component;
    // ��ġ ó�� ������Ʈ
    public PhysicsComponent m_physics_component;

    // �̵� ���� ó�� ������Ʈ
    public StateComponent m_movement_state;
    // ���� ���� ó�� ������Ʈ
    public StateComponent m_attack_state;
    // ���� �̻� ó�� ������Ʈ
    public StateComponent m_buff_debuff_state;
    // �ǰ� ���� ó�� ������Ʈ
    public StateComponent m_hit_state;

    protected void Awake()
    {
        OnAwake();
    }

    protected void Start()
    {
        OnStart();
    }

    protected void Update()
    {
        OnUpdate();
    }

    protected void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnCreatureCollisionEnter(collision);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        OnCreatureCollisionStay(collision);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        OnCreatureCollisionExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pool")
        {

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Pool")
        {
           
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Pool")
        {

        }
    }
    protected virtual void OnAwake()
    {
        m_selected = false;
        m_target = null;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnFixedUpdate()
    {

    }

    protected virtual void OnCreatureCollisionEnter(Collision2D collision)
    {
        m_physics_component.OnCollisionEnter(collision);
    }

    protected virtual void OnCreatureCollisionStay(Collision2D collision)
    {
        m_physics_component.OnCollisionStay(collision);
    }

    protected virtual void OnCreatureCollisionExit(Collision2D collision)
    {
        m_physics_component.OnCollisionExit(collision);
    }
    
}