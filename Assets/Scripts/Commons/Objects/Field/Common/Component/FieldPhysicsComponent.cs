using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPhysicsComponent : PhysicsComponent
{
    // �浹ü�� ����
    public HashSet<Creature> m_collisions;

    public FieldPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Field>();
        m_collisions = new HashSet<Creature>();
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        /*
        if(collision.gameObject.tag == "HeroFoot")
        {
            Creature item = collision.GetComponentInParent<Creature>();

            //if (!((Field)m_data).m_physics_component.m_collider.bounds.Contains(item.m_physics_component.m_bottom))
               //return;

            if (!m_collisions.TryGetValue(item, out Creature tmp))
                m_collisions.Add(item);

            // ��� �� �ι� ������... �𸣰����� �̹� ���� �����ϴ��� �ƴ����� ������ �ϴ� ���� ������
            if (!item.m_physics_component.m_affected_frictions.ContainsKey(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder))
                item.m_physics_component.m_affected_frictions.Add(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder, new Vector2(((Field)m_data).m_friction, ((Field)m_data).m_friction));
        }
        */
    }

    public override void OnTriggerStay(Collider2D collision)
    {
        int sorting_order = ((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder;

        if (collision.tag == "Hero")
        {
            Creature item = collision.gameObject.GetComponent<Creature>();

            if (((Field)m_data).m_physics_component.m_collider.bounds.Contains(item.m_physics_component.m_bottom))
            {
                if (!m_collisions.TryGetValue(item, out Creature tmp))
                    m_collisions.Add(item);

                // ��� �� �ι� ������... �𸣰����� �̹� ���� �����ϴ��� �ƴ����� ������ �ϴ� ���� ������
                if (!item.m_physics_component.m_affected_frictions.ContainsKey(sorting_order))
                    item.m_physics_component.m_affected_frictions.Add(sorting_order, new Vector2(((Field)m_data).m_friction, ((Field)m_data).m_friction));
            }
        }

        List<Creature> del_list = new List<Creature>();
        foreach (Creature creature in m_collisions)
        {
            if (!((Field)m_data).m_physics_component.m_collider.bounds.Contains(creature.m_physics_component.m_bottom)
                || (int)creature.m_physics_component.m_affected_frictions.GetKey(0) > sorting_order)
                del_list.Add(creature);
        }

        for (int i = 0; i < del_list.Count; i++)
        {
            m_collisions.Remove(del_list[i]);
            del_list[i].m_physics_component.m_affected_frictions.Remove(sorting_order);
        }
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "HeroFoot")
        {
            Creature item = collision.GetComponentInParent<Creature>();
            m_collisions.Remove(item);
            item.m_physics_component.m_affected_frictions.Remove(((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder);

            //((HeroGraphicsComponent)(item.m_graphics_component)).spr
        }
        */
    }
}
