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

        m_dragline_fade_speed = 3.0f;

        m_sprite_mask = ((Hero)m_data).transform.Find("RangerSpriteMask").GetComponent<SpriteMask>();

        m_sprite_mask.transform.position = new Vector3(((HeroPhysicsComponent)((Hero)m_data).m_physics_component).m_position.x,
            ((Hero)m_data).m_physics_component.m_bottom.y - m_sprite_mask.bounds.size.y / 2, m_sprite_mask.transform.position.z);
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

    // ������ �ɾ�ٴϴ� ��� ó�� �Լ�
    public override void OnWalkInPool(Field pool)
    {
        Water water = (Water)pool;

        /*
        if (!water.m_physics_component.m_collider.bounds.Contains(((Hero)m_data).m_physics_component.m_bottom))
        {
            m_sprite_mask.transform.position = new Vector3(((HeroPhysicsComponent)((Hero)m_data).m_physics_component).m_position.x,
            ((Hero)m_data).m_physics_component.m_bottom.y - m_sprite_mask.bounds.size.y / 2, m_sprite_mask.transform.position.z);
            
            return;
        }
        */

        Vector2 water_size = water.m_physics_component.m_collider.bounds.size,
            origin = water.m_physics_component.m_collider.bounds.center,
            hero_bottom = ((Hero)m_data).m_physics_component.m_bottom,
            ray_dir = hero_bottom - origin, ray_hit_point = Vector2.zero;

        float half_size = Mathf.Max(water_size.x, water_size.y) / 2;

        // ����ĳ��Ʈ ���� ��ġ�� ������ ������ ������ ������ ������ ������ �ۿ��� ������ �߽����� ��
        Ray2D ray = new Ray2D(origin + ray_dir.normalized * half_size, -ray_dir.normalized);
        List<RaycastHit2D> hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity).ToList();
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider.gameObject == water.gameObject)
            {
                ray_hit_point = hits[i].point;
                break;
            }
        }

        // ������ �ܰ��� ĳ���� ���� �Ÿ�
        float dist_bet_creat_center = Vector2.Distance(hero_bottom, ray_hit_point);
        // ������ �߽ɰ� �ܰ� ��輱 ���� �Ÿ�
        float dist_bet_center_border = Vector2.Distance(origin, ray_hit_point);

        if (dist_bet_center_border <= dist_bet_creat_center)
            return;

        // ĳ���Ͱ� ���� ���� ����
        float sub_ratio = water.m_depth * dist_bet_creat_center / dist_bet_center_border;

        m_sprite_mask.transform.position = new Vector3(((HeroPhysicsComponent)((Hero)m_data).m_physics_component).m_position.x
            , ((Hero)m_data).m_physics_component.m_bottom.y - m_sprite_mask.bounds.size.y / 2 + sub_ratio
            , m_sprite_mask.transform.position.z);
    }


}

