using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] Text m_title_name_text; //제목 글씨

    public void OnEnable()
    {
        m_title_name_text.text = string.Empty;
        m_title_name_text.DOText("Half Blood", 2.0f); //글씨가 써지는 두트윈 적용
    }
}
