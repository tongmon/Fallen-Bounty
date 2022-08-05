using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterPhysicsComponent : FieldPhysicsComponent
{
    public WaterPhysicsComponent(GameObject gameobject) : base(gameobject)
    {
        m_data = gameobject.GetComponent<Water>();
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        base.OnTriggerEnter(collision);
    }

    public override void OnTriggerStay(Collider2D collision)
    {
        /*
        int sorting_order = ((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder;

        if (collision.tag != "Hero")
            return;

        Creature item = collision.gameObject.GetComponent<Creature>();

        if (((Field)m_data).m_physics_component.m_collider.bounds.Contains(item.m_physics_component.m_bottom))
        {
            if (!m_collisions.TryGetValue(item, out Creature tmp))
                m_collisions.Add(item);

            // 얘는 왜 두번 들어가는지... 모르겠지만 이미 값이 존재하는지 아닌지로 따져서 일단 에러 제거함
            if (!item.m_physics_component.m_affected_frictions.ContainsKey(sorting_order))
                item.m_physics_component.m_affected_frictions.Add(sorting_order, new Vector2(((Field)m_data).m_friction, ((Field)m_data).m_friction));
        }
        else
        {
            
        }

        List<Creature> del_list = new List<Creature>();
        foreach (Creature creature in m_collisions)
        {
            if (!((Field)m_data).m_physics_component.m_collider.bounds.Contains(creature.m_physics_component.m_bottom)
                || (int)creature.m_physics_component.m_affected_frictions.GetKey(0) > sorting_order)
            {
                del_list.Add(creature);
                continue;
            }

            creature.m_graphics_component.OnWalkInPool((Water)m_data);
        }

        for (int i = 0; i < del_list.Count; i++)
        {
            m_collisions.Remove(del_list[i]);
            del_list[i].m_physics_component.m_affected_frictions.Remove(sorting_order);
        }
        */
    }

    public override void OnTriggerExit(Collider2D collision)
    {
        base.OnTriggerExit(collision);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        int sorting_order = ((FieldGraphicsComponent)((Field)m_data).m_graphics_component).m_field_sprite.sortingOrder;

        List<Creature> keys = m_collisions.Keys.ToList();
        List<bool> vals = m_collisions.Values.ToList();
        for (int i = 0; i < m_collisions.Count; i++)
        {
            if (vals[i])
            {
                keys[i].m_graphics_component.OnWalkInLiquid((Water)m_data, ((Water)m_data).m_depth);
            }
        }

        /*
        foreach (KeyValuePair<Creature, bool> data in m_collisions)
        {
            if (data.Value)
            {
                data.Key.m_graphics_component.OnWalkInPool((Water)m_data);
            }
        }
        */
    }
}
