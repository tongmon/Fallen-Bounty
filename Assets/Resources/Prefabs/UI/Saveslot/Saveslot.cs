using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;

public class Saveslot : MonoBehaviour
{
    //��Ʈ���� Ÿ��Ʋ
    [SerializeField] GameObject m_title_name;

    //�� 3��¥�� ���̺� ��ư
    [SerializeField] GameObject[] save_button;

    [SerializeField] GameObject save_name; 
    //������ ���� ����ȭ Ŭ���� 
    SaveState save_state;

    List<Vector2> m_positon = new List<Vector2>();
    private void Awake()//�ٽ� �ε������� ������ ����.
    {
        m_positon.Add(m_title_name.transform.localPosition);
        m_positon.Add(save_button[0].transform.localPosition);
        m_positon.Add(save_button[1].transform.localPosition);
        m_positon.Add(save_button[2].transform.localPosition);
    }
    private void OnEnable()
    {
        m_title_name.transform.localPosition = m_positon[0];
        for (int i = 0; i < 3; i++)
        {
            save_button[i].transform.localScale = new Vector3(1, 1, 1);
            save_button[i].GetComponentInParent<Canvas>().sortingOrder = 0;
            save_button[i].transform.localPosition = m_positon[i + 1];
        }
        m_title_name.transform.DOMoveY(3, 1.5f); //���۽� ���� �̵� ,��Ʈ�� �̿�
        GetComponent<RectTransform>();
    }
    public void ButtonClicked()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject; //Ŭ���� ��ü ����
        StartCoroutine(ButtonDoTween(obj)); //�ڷ�ƾȣ��
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //�����ư
        System.TimeSpan time = System.DateTime.Now - save_state.last_playtime;
        save_state.playtime = time.Minutes;
    }
    IEnumerator ButtonDoTween(GameObject obj)
    {
        save_state = (SaveState)Resources.Load("SaveFile/SaveFile" + obj.name);

        UnityEditor.EditorUtility.SetDirty(save_state);//�̰��ϸ� �ײ��� �����.

        save_name.name = "SaveFile" + obj.name;
        DontDestroyOnLoad(save_name);

        save_state.last_playtime = System.DateTime.Now;

        obj.GetComponentInParent<Canvas>().sortingOrder = 1; //�� ���̾ ������ �Ǿ����� ������
        obj.transform.DOMoveX(0, 0.8f);
        yield return new WaitForSecondsRealtime(0.8f);

        obj.transform.DOScaleX(2.5f, 0.8f);
        obj.transform.DOScaleY(2.5f, 0.8f);
        yield return new WaitForSecondsRealtime(0.8f);

        GameObject.Find("EventSystem").GetComponent<CanvasManager>().TitleCanvasAdd();
    }
}