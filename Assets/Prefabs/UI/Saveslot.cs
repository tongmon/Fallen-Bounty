using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class Saveslot : MonoBehaviour
{   //양피지 형식으로 만들어서 클릭시 펴지면서 들어가게끔 만들기
    [SerializeField] GameObject title_name;
    GameObject click_object;
    private void Start()
    {
        title_name.transform.DOMoveY(4, 1.5f);
        GetComponent<RectTransform>();
    }

    public void OnButtonClicked()
    {
        click_object = EventSystem.current.currentSelectedGameObject;
        StartCoroutine("ButtonDoTween");
    }
    IEnumerator ButtonDoTween()
    {
        click_object.GetComponent<SpriteRenderer>().sortingOrder = 1;//재적용필요
        click_object.transform.position = Vector2.Lerp(click_object.transform.position, new Vector2(0, 0), 1);
        yield return new WaitForSecondsRealtime(1.0f);
        click_object.transform.DOScaleX(0.018f, 1.0f);
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene("Title_Scene");
    }
}
