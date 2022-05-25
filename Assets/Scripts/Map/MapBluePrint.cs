using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public enum m_MapType
    {
        Common,
        Common1,
        Common2,
        Common3,
        Common4,
        Common5,
        Elite,
        Elite1,
        Elite2,
        Store,
        Town,
        Train,
        Battle,
        Cursed,
        Boss
    }

}
namespace Map { 
    [CreateAssetMenu]
    public class MapBluePrint : ScriptableObject
    {
        public bool[] m_is_exist = new bool[15]; //사용 유무 검사용
        public Sprite m_sprite;
        public m_MapType m_MapType;
    }
}

