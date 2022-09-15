using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public GameObject obj;//��ų ������ ĳ����

    GUIStyle style = new GUIStyle();//IMGUI�� ��Ÿ�� �ֱ��

    GameObject mouse;//�浹���� ���� ���콺 ������.

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
        transform.position = obj.transform.position;//������ ĳ���� ���󰡱�.
    }
    private void OnGUI()//IMGUI���
    {
        GUI.Label(new Rect(760, 1000, 200, 100), "���� ���̸� �ߵ����� �ʽ��ϴ�.", style);
    }
    private void OnTriggerStay2D(Collider2D other)//��ų ��� ����
    {
        if(other.gameObject == mouse)
        {
            GameObject.FindGameObjectWithTag("Skill").GetComponent<Skill>().posible = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)//��ų ��� �Ұ�
    {
        if (other.gameObject == mouse)
        {
            GameObject.FindGameObjectWithTag("Skill").GetComponent<Skill>().posible = false;
        }
    }
}
