using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class SaveslotScene : MonoBehaviour
{
    [SerializeField] GameObject m_title_name;
    GameObject m_click_object;
    private void Start()
    {
        m_title_name.transform.DOMoveY(3, 1.5f);
        GetComponent<RectTransform>();
    }
    public void ButtonClicked()
    {
        m_click_object = EventSystem.current.currentSelectedGameObject;
        StartCoroutine("ButtonDoTween");
    }
    public void ExitButtonClicked()
    {
        Application.Quit();
    }
    IEnumerator ButtonDoTween()
    {
        m_click_object.GetComponentInParent<Canvas>().sortingOrder = 1;
        m_click_object.transform.DOMoveX(0, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        m_click_object.transform.DOScaleX(2.5f, 1.5f);
        m_click_object.transform.DOScaleY(2.5f, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Title_Scene");
    }
}
