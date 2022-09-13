using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroGraphicsComponent : GraphicsComponent
{
    public float m_dragline_alpha;
    public float m_dragline_fade_speed;
    public LineRenderer m_line_renderer;

    public float m_seleted_sprite_alpha;
    public SpriteRenderer m_seleted_sprite;

    public SpriteMask m_sprite_mask;

    public HeroGraphicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Hero>();

        m_seleted_sprite_alpha = m_dragline_alpha = 0.0f;

        m_line_renderer = ((Hero)m_data).GetComponent<LineRenderer>();

        m_seleted_sprite = ((Hero)m_data).transform.Find("FocusCircle").GetComponent<SpriteRenderer>();

        m_seleted_sprite.transform.position = new Vector3(((Hero)m_data).m_physics_component.m_position.x, 
            ((Hero)m_data).m_physics_component.m_bottom.y, m_seleted_sprite.transform.position.z);

        m_dragline_fade_speed = 3.0f;

        m_sprite_mask = ((Hero)m_data).transform.Find("BerserkerSpriteMask").GetComponent<SpriteMask>();

        m_sprite_mask.transform.position = new Vector3(((Hero)m_data).m_physics_component.m_position.x,
            ((Hero)m_data).m_physics_component.m_bottom.y - m_sprite_mask.bounds.size.y / 2, m_sprite_mask.transform.position.z);

        m_main_sprite = ((Hero)m_data).transform.Find("BerserkerSprite").GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();

        OnDrawDragLine();
        OnDrawSelectedSprite();
    }

    protected virtual void OnDrawDragLine()
    {

    }

    protected virtual void OnDrawSelectedSprite()
    {

    }

    // 웅덩이 걸어다니는 경우 처리 함수
    public override void OnWalkInLiquid(Field pool, float depth)
    {
        Vector2 water_size = pool.m_physics_component.m_collider.bounds.size,
            origin = pool.m_physics_component.m_collider.bounds.center,
            hero_bottom = ((Hero)m_data).m_physics_component.m_bottom,
            ray_dir = hero_bottom - origin, ray_hit_point = Vector2.zero;

        float half_size = Mathf.Max(water_size.x, water_size.y) / 2;

        // 레이캐스트 시작 위치를 웅덩이 밖으로 빼내고 레이져 방향은 웅덩이 밖에서 웅덩이 중심으로 쏨
        Ray2D ray = new Ray2D(origin + ray_dir.normalized * half_size, -ray_dir.normalized);
        List<RaycastHit2D> hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity).ToList();
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider.gameObject == pool.gameObject)
            {
                ray_hit_point = hits[i].point;
                break;
            }
        }

        // 웅덩이 외곽과 캐릭터 사이 거리
        float dist_bet_creat_center = Vector2.Distance(hero_bottom, ray_hit_point);
        // 웅덩이 중심과 외곽 경계선 사이 거리
        float dist_bet_center_border = Vector2.Distance(origin, ray_hit_point);

        // 캐릭터가 웅덩이 밖으로 나가버린 경우엔 잠기는 표현 방지
        if (dist_bet_center_border <= Vector2.Distance(hero_bottom, origin))
            return;

        // 캐릭터가 물에 잠기는 비율
        float sub_ratio = depth * dist_bet_creat_center / dist_bet_center_border;

        m_main_sprite.transform.position = new Vector3(((Hero)m_data).m_physics_component.m_position.x
            , ((Hero)m_data).m_physics_component.m_position.y - sub_ratio
            , m_sprite_mask.transform.position.z);
    }
}

