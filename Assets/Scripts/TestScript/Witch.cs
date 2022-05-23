using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Hero
{
    public SpriteRenderer circle_below_hero;

    private new void Awake()
    {
        base.Awake();
        m_name = GetType().Name;
        m_ability_holder = new AbilityHolder(gameObject);
        m_sprite_seleted_circle = transform.Find("FocusCircle").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnMouseLeftDown()
    {
        /*
        if (!Input.GetMouseButtonDown(0))
            return;

        var mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

        if (!mouse.collider)
            return;

        if(mouse.collider && mouse.collider.gameObject != gameObject)
        {
            circle_below_hero.color = new Color(255, 255, 255, 0);
            m_selected = false;
            return;
        }

        if(circle_below_hero.color.a == 0)
        {
            circle_below_hero.color = new Color(255, 255, 255, 255);
            m_selected = true;
        }
        */
    }

    protected override void OnMouseLeftDrag()
    {
        m_mouse_hold_time[0] += Time.deltaTime;
    }

    protected override void OnMouseLeftUp()
    {

    }

    protected override void OnMouseRightDown()
    {

    }

    protected override void OnMouseRightDrag()
    {

    }

    protected override void OnMouseRightUp()
    {

    }
}