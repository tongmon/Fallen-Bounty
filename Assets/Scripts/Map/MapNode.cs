using LitJson;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Web.WebPages;
using UnityEngine;


public enum eMapType
{
    Common,
    Elite,
    Town,
    Train,
    Battle,
    Store,
    Random,
    Boss
}

[Serializable]
public class MapNode : ScriptableObject
{
    public int m_num;
    public float m_position_x;//위치, 선위치.
    public float m_position_y;//위치, 선위치.
    public float m_position_z;//위치, 선위치.
    public Sprite m_sprite;
    public string spritePath;//이미지 경로인데 아마 종류마다 따로 정리할 듯.
    public eMapType m_mapType;
    public List <int> m_parent_num;
    public List <int> m_child_num;

    public MapNode()
    {
        m_mapType = eMapType.Common;
        m_parent_num = new List<int>();
        m_child_num = new List<int>();
        this.spritePath = string.Empty;
        this.m_position_x = 0;
        this.m_position_y = 0;
        this.m_position_z = 0;
        this.m_sprite = null;
    }
    
    public void MapSetting(int index, JsonData data)
    {
        #region m_num
        m_num = int.Parse(data[index]["m_num"].ToString());
        #endregion
        #region m_position
        m_position_x = float.Parse(data[index]["m_position_x"].ToString());
        m_position_y = float.Parse(data[index]["m_position_y"].ToString());
        m_position_z = float.Parse(data[index]["m_position_z"].ToString());
        #endregion
        #region Sprite
        spritePath = data[index]["spritePath"].ToString();
        if (!spritePath.IsEmpty())//이미지 적용
        {
            byte[] bytes = File.ReadAllBytes(spritePath);
            Texture2D texture = new Texture2D(0,0);
            texture.LoadImage(bytes);

            Rect rect = new Rect(0,0, texture.width, texture.height);
            m_sprite = Sprite.Create(texture, rect, new Vector2(0.5f,0.5f));
        }
        #endregion
        #region m_maptype
        switch (data[index]["m_mapType"].ToString())
        {
            case "Common": m_mapType = eMapType.Common; break;
            case "Elite": m_mapType = eMapType.Elite; break;
            case "Town": m_mapType = eMapType.Town; break;
            case "Train": m_mapType = eMapType.Train; break;
            case "Battle": m_mapType = eMapType.Battle; break;
            case "Store": m_mapType = eMapType.Store; break;
            case "Random": m_mapType = eMapType.Random; break;
            case "Boss": m_mapType = eMapType.Boss; break;
        }
        #endregion
        #region m_parent_num
        for (int i = 0; i< data[index]["m_parent_num"].Count; i++)
        {
            m_parent_num.Add(int.Parse(data[index]["m_parent_num"].ToString()));
        }
        #endregion
        #region m_child_num
        for (int i = 0; i < data[index]["m_child_num"].Count; i++)
        {
            m_child_num.Add(int.Parse(data[index]["m_child_num"].ToString()));
        }
        #endregion
    }
}
