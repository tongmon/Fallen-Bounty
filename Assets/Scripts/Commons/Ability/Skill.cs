using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public IEnumerator coroutine;

    Vector3 target_tr = new Vector3(0, 0, 0);

    private void OnEnable()
    {
        StartCoroutine(InputDelay(GameObject.FindGameObjectWithTag("SkillToolTip"),target_tr));
    }

    public IEnumerator InputDelay(GameObject image, Vector3 target_tr)
    {
        while (true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(image);
                target_tr = Input.mousePosition;
                break;
            }
            if (Input.GetMouseButtonDown(1))//������ ��ư�� ���.
            {
                Destroy(image);
                break;
            }
            image.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺 ����ٴϱ�
        }
        StartCoroutine(coroutine);
    }
}
