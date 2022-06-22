using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using UnityEngine;

/*
[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(Hero), "physic_power")]
public class Creature : MonoBehaviour
{
    #region Data from JSON file
    // 생명체 이름
    public string type_name;
    // 생명력(체력)
    public float health;
    // 좌우 속도
    public float x_velocity;
    // 상하 속도
    public float y_velocity;
    // 64bit, 최대 64개의 상태이상
    public long status_effect;
    // 마력 방어력
    public float magic_armor;
    // 물리 방어력
    public float physic_armor;
    #endregion
    
    // 스킬 제한 개수
    public int m_abilities_limit;
    // 생명체가 향하는 방향
    public Vector2 m_vec_direction;
    // 스킬 홀더
    public AbilityHolder m_ability_holder;
    // 생명체 선택 포커스 스프라이트
    public SpriteRenderer m_sprite_seleted_circle;
    // 생명체 선택 유무
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
*/

[JsonConverter(typeof(JsonSubtypes))]
[JsonSubtypes.KnownSubTypeWithProperty(typeof(HeroData), "physic_power")]
public class CreatureData
{
    #region Data from JSON file
    // 생명체 이름
    // [JsonProperty(PropertyName = "Type Name")] 나중에 보기 쉽게 이렇게 바꿀 수 있음
    public string type_name;
    // 생명력(체력)
    public float health;
    // 좌우 속도
    public float x_velocity;
    // 상하 속도
    public float y_velocity;
    // 마력 방어력
    public int magic_armor;
    // 물리 방어력
    public int physic_armor;
    #endregion
}

public class Creature : MonoBehaviour
{
    // JSON 파싱한 데이터가 담김, CreatureData, HeroData 등...
    public object m_data;

    // 스킬 제한 개수
    public int m_abilities_limit;
    // 생명체가 향하는 방향
    public Vector2 m_vec_direction;
    // 스킬 홀더
    public AbilityHolder m_ability_holder;
    // 생명체 선택 포커스 스프라이트
    public SpriteRenderer m_sprite_seleted_circle;
    // 생명체 강체
    public Rigidbody2D m_rigidbody;
    // 생명체 선택 유무
    public bool m_selected;
    // 64bit, 최대 64개의 상태이상
    public long m_status_effect;

    // 입력 처리 컴포넌트
    public InputComponent m_input_component;
    // 그래픽 처리 컴포넌트
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