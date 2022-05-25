using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace Map
{
    public class Map : MonoBehaviour
    {
        public m_MapType m_map_type;
        public Sprite m_sprite;
        public List <MapBluePrint> m_map_List;
        int i = 0;
        private void Awake()
        {
            while (true)//할당할때까지 반복
            {
                i = Random.Range(0, 14);
                Debug.Log(i);
                ///if (!m_map_blue_print.m_is_exist[i])
                {
                    //m_map_blue_print.m_is_exist[i] = true;
                    m_map_type = (m_MapType)i;
                    //m_sprite = m_map_blue_print.m_sprite;
                    break;
                }
            }
        }
    }
}