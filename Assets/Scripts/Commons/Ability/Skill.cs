using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float range = 0;

    public float duration_time = 0;

    Vector3 vec;
    private void Start()
    {
        vec = Input.mousePosition;
        vec.z = Camera.main.farClipPlane;
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }
    void Update()
    {
        vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vec.x, vec.y ,0);
        if (Input.GetMouseButtonDown(0))
        {
            tag = "Skill";
            gameObject.AddComponent<CircleCollider2D>();
            GetComponent<CircleCollider2D>().isTrigger = true;//Æ®¸®°Å·Î Å½ÁöÇØ¾ß µÊ.
            GetComponent<CircleCollider2D>().radius *= range;
            Destroy(gameObject, duration_time);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
