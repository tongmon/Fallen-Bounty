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
public enum eItem
{
    item1,
    item2,
    item3,
    item4,
    item5,
    item6,
    item7,
    item8,
    item9,
    item10,
    item11,
    item12,
    item13,
    item14,
    item15,
    item16,
    item17,
    item18,
    item19,
    item20,
    item21,
    item22,
    item23,
    item24,
}
public enum eStage
{
    Woods,
    SnowMountain,
    Cave,
    Swamp,
    Ruins,
    SulkyWoods,
    Villa
}

public enum eChallenges
{
    Challenge1,
    Challenge2,
    Challenge3,
    Challenge4,
    Challenge5,
    Challenge6,
    Challenge7,
    Challenge8,
    Challenge9,
    Challenge10,
    Challenge11,
    Challenge12,
    Challenge13,
    Challenge14,
    Challenge15,
    Challenge16,
    Challenge17,
    Challenge18,
    Challenge19,
    Challenge20,
    Challenge21,
    Challenge22,
    Challenge23,
    Challenge24,
    Challenge25,
    Challenge26,
    Challenge27,
    Challenge28,
    Challenge29,
    Challenge30,
    Challenge31,
    Challenge32,
    Challenge33,
    Challenge34,
    Challenge35,
    Challenge36,
    Challenge37,
    Challenge38,
    Challenge39,
    Challenge40,
}
public class SaveState 
{
    public float playtime; //총 플레이 시간
    public DateTime last_playtime; //최근 플레이시간 , 숫자로 안보임
    public int clear_count;//총 클리어 횟수

    public List<eItem> unlock_item;
    public List<eCharacter> unlock_character;
    public List<eStage> unlock_stage;
    public List<eChallenges> unlock_challenges;
    public string[] clear_log;

    public List<StageInfo> stage_info;
    public List<ItemInfo> item_info;
    public List<ChallengeInfo> chanllenge_Info;

    /// <summary>
    ///히어로 정보도 필요함
    /// </summary>


    public SaveState()
    {
        playtime = 0.0f;
        last_playtime = DateTime.Now;
        clear_count = 0;

        unlock_item = new List<eItem>();
        unlock_character = new List<eCharacter>() { eCharacter.Berserker };
        unlock_stage = new List<eStage>() { eStage.Woods, eStage.SnowMountain, eStage.Cave };
        unlock_challenges = new List<eChallenges>();
        clear_log = new string[10];//10개만 저장

        stage_info = new List<StageInfo>();
        item_info = new List<ItemInfo>();
        chanllenge_Info = new List<ChallengeInfo>();
    }
}
