using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // 생명체 선택 유무
    public bool m_selected;

    // 캐릭터 타겟팅
    public Creature m_target;
    // 타겟 위치
    public Vector2? m_point_target;

    //현재 체력
    public float m_current_health;
    //현재 방어력
    public float m_current_armor;
    //현재 마법저항력
    public float m_current_magic_armor;

    // 입력 처리 컴포넌트
    public InputComponent m_input_component;
    // 그래픽 처리 컴포넌트
    public GraphicsComponent m_graphics_component;
    // 물리 처리 컴포넌트
    public PhysicsComponent m_physics_component;

    // 이동 상태 처리 컴포넌트
    public StateComponent m_movement_state;
    // 공격 상태 처리 컴포넌트
    public StateComponent m_attack_state;
    // 상태 이상 처리 컴포넌트
    public StateComponent m_buff_debuff_state;
    // 피격 상태 처리 컴포넌트
    public StateComponent m_hit_state;

    protected void Awake()
    {
        OnAwake();
        m_hit_state = new StateComponent(null);
        m_abilities_limit = 1;
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