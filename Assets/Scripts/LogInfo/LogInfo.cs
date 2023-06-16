using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using LitJson;
using System.Web.WebPages;

[Serializable]
public class Info : ScriptableObject
{
    public Sprite m_sprite;
    public string spritePath = string.Empty;
    public string m_name = string.Empty;
    public string m_info = string.Empty;
    public string m_unlock_condition = string.Empty;

    public virtual void InfoSetting(int index, JsonData data)
    {
        spritePath = data[index]["spritePath"].ToString();
        if (!spritePath.IsEmpty())//이미지 적용
        {
            byte[] bytes = File.ReadAllBytes(spritePath);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);

            Rect rect = new Rect(0, 0, texture.width, texture.height);
            m_sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        }
        m_name = data[index]["name"].ToString();
        m_info = data[index]["info"].ToString();
        m_unlock_condition = data[index]["m_unlock_condition"].ToString();
    }
}
