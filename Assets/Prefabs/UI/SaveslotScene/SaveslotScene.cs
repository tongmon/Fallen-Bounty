using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;
using System;
using Newtonsoft.Json.Linq;

namespace Save {
    public class SaveslotScene : MonoBehaviour
    {
        [SerializeField] GameObject m_title_name; //���� ����
        public GameObject m_click_object; //Ŭ���� ��ü �����
        SaveStat save_stat = new SaveStat();
        private static string m_save_path => "Assets/Resources/SaveFileJson/";
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
            save_stat = JsonParser.LoadJsonFile<SaveStat>(m_save_path, m_click_object + "json");//�ҷ����� ����
            m_click_object.GetComponentInParent<Canvas>().sortingOrder = 1; //�� ���̾ ������ �Ǿ����� ������
            m_click_object.transform.DOMoveX(0, 1.5f);
            yield return new WaitForSecondsRealtime(1.5f);
            m_click_object.transform.DOScaleX(2.5f, 1.5f);
            m_click_object.transform.DOScaleY(2.5f, 1.5f);
            yield return new WaitForSecondsRealtime(1.5f);
            DontDestroyOnLoad(m_click_object);
            SceneManager.LoadScene("Title_Scene"); //���������� �� �ҷ�����
        }
    }
}
