using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public AbilityHolder m_ability_holder;

    public string m_name;

    public float m_health;
    public float m_x_velocity;
    public float m_y_velocity;

    // 64bit, 최대 64개의 상태이상
    public long m_status_effect;

    // 스킬 제한 개수
    public int m_abilities_limit;

    public Vector2 m_vec_direction;

    public SpriteRenderer m_sprite_seleted_circle;

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
