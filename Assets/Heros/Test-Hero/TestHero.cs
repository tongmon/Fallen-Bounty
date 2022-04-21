using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero : MonoBehaviour
{
    Rigidbody2D m_rb;
    public float m_normal_acceleration;
    [HideInInspector] public float m_acceleration;
    [HideInInspector] public Vector2 m_movement_input;

    public Transform m_arrow;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_acceleration = m_normal_acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_arrow.up = (mouse_pos - (Vector2)transform.position).normalized;
    }

    private void FixedUpdate()
    {
        m_movement_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        m_rb.velocity = m_movement_input * m_acceleration * Time.deltaTime;
    }
}
