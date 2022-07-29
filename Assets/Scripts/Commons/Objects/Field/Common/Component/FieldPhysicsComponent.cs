using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPhysicsComponent : PhysicsComponent
{
    // 충돌체들 모음
    public HashSet<Creature> m_collisions;

    public FieldPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Field>();
        m_collisions = new HashSet<Creature>();
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if(collision.tag == "Hero_Bottom")
        {
            Creature item = collision.GetComponentInParent<Creature>();
            
            if (!m_collisions.TryGetValue(item, out Creature tmp))
                m_collisions.Add(item);

            // 얘는 왜 두번 들어가는지... 모르겠지만 이미 값이 존재하는지 아닌지로 따져서 일단 에러 제거함
            if (!item.m_physics_component.m_affected_frictions.ContainsKey(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder))
                item.m_physics_component.m_affected_frictions.Add(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder, new Vector2(((Field)m_data).m_friction, ((Field)m_data).m_friction));
        }
    }

    public override void OnTriggerStay(Collider2D collision)
    {

    }

    public override void OnTriggerExit(Collider2D collision)
    {
        if (collision.tag == "Hero_Bottom")
        {
            Creature item = collision.GetComponentInParent<Creature>();
            m_collisions.Remove(item);
            item.m_physics_component.m_affected_frictions.Remove(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder);

            //((HeroGraphicsComponent)(item.m_graphics_component)).spr
        }
    }
}
