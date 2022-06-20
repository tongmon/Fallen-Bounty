using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using DG.Tweening;


public class SaveslotScene : MonoBehaviour
{
    [SerializeField] GameObject m_title_name; //���� ����
    public GameObject s_name;
    GameObject m_click_object; //Ŭ���� ��ü �����
    SaveState save_state;
    private void Start()
    {
        m_title_name.transform.DOMoveY(3, 1.5f); //���۽� ���� �̵� ,��Ʈ�� �̿�
        GetComponent<RectTransform>();
    }
    public void ButtonClicked()
    {
        m_click_object = EventSystem.current.currentSelectedGameObject; //Ŭ���� ��ü ����
        StartCoroutine("ButtonDoTween"); //�ڷ�ƾȣ��
    }
    public void ExitButtonClicked()
    {
        Application.Quit(); //�����ư
    }
    IEnumerator ButtonDoTween()
    {
        string file_path = "Assets/Resources/SaveFileJson/";
        if (!File.Exists(file_path + "SaveFile" + m_click_object.transform.name + "json"))
        {
            save_state = new SaveState();
            save_state.last_playtime = System.DateTime.Now;
            JsonParser.CreateJsonFile(file_path + "SaveFile" + m_click_object.transform.name + "json", JsonUtility.ToJson(save_state));
            s_name.transform.name = file_path + "SaveFile" + m_click_object.transform.name + "json";
            DontDestroyOnLoad(s_name);
        }
        else
        {
            save_state = JsonParser.LoadJsonFile<SaveState>(file_path + "SaveFile" + m_click_object.transform.name + "json");
            s_name.transform.name = file_path + "SaveFile" + m_click_object.transform.name + "json";
            DontDestroyOnLoad(s_name);
        }
        m_click_object.GetComponentInParent<Canvas>().sortingOrder = 1; //�� ���̾ ������ �Ǿ����� ������
        m_click_object.transform.DOMoveX(0, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        m_click_object.transform.DOScaleX(2.5f, 1.5f);
        m_click_object.transform.DOScaleY(2.5f, 1.5f);
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("Title_Scene"); //���������� �� �ҷ�����
    }
}
