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
    public float playtime; //�� �÷��� �ð�
    public DateTime last_playtime; //�ֱ� �÷��̽ð� , ���ڷ� �Ⱥ���
    public int clear_count;//�� Ŭ���� Ƚ��

    public List<eCharacter> unlock_character;

    public SaveState()
    {
        playtime = 0.0f;
        last_playtime = DateTime.Now;
        clear_count = 0;

        unlock_character = new List<eCharacter>() { eCharacter.Berserker };
    }
}
