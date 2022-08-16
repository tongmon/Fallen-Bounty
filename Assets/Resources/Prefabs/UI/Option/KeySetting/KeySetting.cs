using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum m_key_action { SKILL1, SKILL2, SKILL3, SKILL4, KEYCOUNT } //열거형으로 갯수만듦

//딕셔너리로 키와 키코드를 할당
public static class KeySet { public static Dictionary<m_key_action, KeyCode> m_keys = new Dictionary<m_key_action, KeyCode>(); }

public class KeySetting : MonoBehaviour
{
    KeyCode[] m_default_key = new KeyCode[] { KeyCode.Q , KeyCode.W , KeyCode.E , KeyCode.R }; //기본키코드 설정
    private void Awake()
    {
        for(int i = 0; i<(int)m_key_action.KEYCOUNT; i++) //기본키값 넣기
        {
            KeySet.m_keys.Add((m_key_action)i, m_default_key[i]);
        }
    }

    private void OnGUI()
    {
        Event key_event = Event.current; //현재키입력 감지
        if (key_event.isKey)
        {
            bool is_exist = false;
            for(int i =0; i <(int)m_key_action.KEYCOUNT; i++)//인덱스값이 4라서 4번까지 돔
            {
                if (key_event.keyCode == KeySet.m_keys[(m_key_action)i]) is_exist = true;  //현재의 키값을 토글시킴
            }
            if(!is_exist) KeySet.m_keys[(m_key_action)key] = key_event.keyCode; //토글시킨 키값을 찾아서 저장.
            key = -1;
        }
    }
    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }
}
