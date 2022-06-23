using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

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
    // �¿� �ӵ�
    public float x_velocity;
    // ���� �ӵ�
    public float y_velocity;
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
    // ����ü ��ü
    public Rigidbody2D m_rigidbody;
    // ����ü ���� ����
    public bool m_selected;
    // 64bit, �ִ� 64���� �����̻�
    public long m_status_effect;

    // �Է� ó�� ������Ʈ
    public InputComponent m_input_component;
    // �׷��� ó�� ������Ʈ
    public GraphicsComponent m_graphics_component;

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

    protected virtual void OnAwake()
    {
        m_selected = false;
        m_rigidbody = GetComponent<Rigidbody2D>();
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
}