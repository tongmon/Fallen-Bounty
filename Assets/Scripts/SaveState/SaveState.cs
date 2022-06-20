using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum eCharacter
{
    Berserker,
    Paladin,
    Hunter,
    Archer,
    Pirate,
    Assassin,
    Thief,
    Saint,
    Dark_Mage,
    Elementalist,
    Mercenary,
    Count
}
public class SaveState 
{
    public float playtime; //총 플레이 시간
    public DateTime last_playtime; //최근 플레이시간 , 숫자로 안보임
    public int clear_count;//총 클리어 횟수

    public List<eCharacter> unlock_character;

    public SaveState()
    {
        playtime = 0.0f;
        last_playtime = DateTime.Now;
        clear_count = 0;

        unlock_character = new List<eCharacter>() { eCharacter.Berserker };
    }
}
