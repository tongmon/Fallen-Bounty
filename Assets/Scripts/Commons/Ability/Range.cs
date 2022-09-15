using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public GameObject obj;//스킬 시전한 캐릭터

    GUIStyle style = new GUIStyle();//IMGUI에 스타일 넣기용

    GameObject mouse;//충돌값을 가진 마우스 포인터.

    Vector3 vec;
    private void OnEnable()
    {
        style.fontSize = 32;
        style.normal.textColor = Color.white;

        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;

        mouse = new GameObject();
        mouse.AddComponent<CircleCollider2D>();
        mouse.name = "MousePointer";
        mouse.transform.localScale = new Vector3(0.1f, 0.1f, 1);
    }

    void Update()
    {
        vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.transform.position = new Vector3(vec.x, vec.y, 0);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Destroy(mouse);
            Destroy(gameObject);
        }
        transform.position = obj.transform.position;//시전한 캐릭터 따라가기.
    }
    private void OnGUI()//IMGUI사용
    {
        GUI.Label(new Rect(760, 1000, 200, 100), "범위 밖이면 발동되지 않습니다.", style);
    }
    private void OnTriggerStay2D(Collider2D other)//스킬 사용 가능
    {
        if(other.gameObject == mouse)
        {
            GameObject.FindGameObjectWithTag("Skill").GetComponent<Skill>().posible = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)//스킬 사용 불가
    {
        if (other.gameObject == mouse)
        {
            GameObject.FindGameObjectWithTag("Skill").GetComponent<Skill>().posible = false;
        }
    }
}
