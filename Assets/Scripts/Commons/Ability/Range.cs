using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public GameObject obj;

    GUIStyle style = new GUIStyle();

    Vector3 vec;
    private void Awake()
    {
        style.fontSize = 32;
        style.normal.textColor = Color.white;
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject mouse = new GameObject();
            mouse.AddComponent<CircleCollider2D>();
            mouse.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            mouse.name = "Mouse";
            mouse.AddComponent<Rigidbody2D>();
            mouse.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.transform.position = new Vector3(vec.x, vec.y, 0);
            Destroy(mouse);
            Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
        transform.position = obj.transform.position;
    }
    private void OnGUI()//IMGUI사용
    {
        GUI.Label(new Rect(760, 1000, 200, 100), "범위 밖이면 발동되지 않습니다.", style);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Mouse")
        {
            obj.GetComponent<Skill>().posible = true;
        }
    }
}
