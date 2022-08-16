using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum m_key_action { SKILL1, SKILL2, SKILL3, SKILL4, KEYCOUNT } //���������� ��������

//��ųʸ��� Ű�� Ű�ڵ带 �Ҵ�
public static class KeySet { public static Dictionary<m_key_action, KeyCode> m_keys = new Dictionary<m_key_action, KeyCode>(); }

public class KeySetting : MonoBehaviour
{
    KeyCode[] m_default_key = new KeyCode[] { KeyCode.Q , KeyCode.W , KeyCode.E , KeyCode.R }; //�⺻Ű�ڵ� ����
    private void Awake()
    {
        for(int i = 0; i<(int)m_key_action.KEYCOUNT; i++) //�⺻Ű�� �ֱ�
        {
            KeySet.m_keys.Add((m_key_action)i, m_default_key[i]);
        }
    }

    private void OnGUI()
    {
        Event key_event = Event.current; //����Ű�Է� ����
        if (key_event.isKey)
        {
            bool is_exist = false;
            for(int i =0; i <(int)m_key_action.KEYCOUNT; i++)//�ε������� 4�� 4������ ��
            {
                if (key_event.keyCode == KeySet.m_keys[(m_key_action)i]) is_exist = true;  //������ Ű���� ��۽�Ŵ
            }
            if(!is_exist) KeySet.m_keys[(m_key_action)key] = key_event.keyCode; //��۽�Ų Ű���� ã�Ƽ� ����.
            key = -1;
        }
    }
    int key = -1;
    public void ChangeKey(int num)
    {
        key = num;
    }
}
