using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor;


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
public enum eItem//후에 명칭 지정필
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

public enum eChallenges//후에 명칭 지정필
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
[Serializable]
public class SaveState : ScriptableObject
{
    public float playtime; //총 플레이 시간
    public DateTime last_playtime; //최근 플레이시간 , 숫자로 안보임
    public int clear_count;//총 클리어 횟수

    public List<eItem> unlock_item;
    public List<eCharacter> unlock_character;
    public List<eStage> unlock_stage;
    public List<eChallenges> unlock_challenges;
    public string[] clear_log;

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
    }
    public void JsonSetting(JsonData data)
    {
        playtime = float.Parse(data["playtime"].ToString());
        clear_count = int.Parse(data["clear_count"].ToString());
        #region Item
        for (int i = 0; i < data["unlock_item"].Count; i++)
        {
            eItem item = eItem.item1;
            switch (data["unlock_item"][i].ToString())
            {
                case "item1": item = eItem.item1; break;
                case "item2": item = eItem.item2; break;
                case "item3": item = eItem.item3; break;
                case "item4": item = eItem.item4; break;
                case "item5": item = eItem.item5; break;
                case "item6": item = eItem.item6; break;
                case "item7": item = eItem.item7; break;
                case "item8": item = eItem.item8; break;
                case "item9": item = eItem.item9; break;
                case "item10": item = eItem.item10; break;
                case "item11": item = eItem.item11; break;
                case "item12": item = eItem.item12; break;
                case "item13": item = eItem.item13; break;
                case "item14": item = eItem.item14; break;
                case "item15": item = eItem.item15; break;
                case "item16": item = eItem.item16; break;
                case "item17": item = eItem.item17; break;
                case "item18": item = eItem.item18; break;
                case "item19": item = eItem.item19; break;
                case "item20": item = eItem.item20; break;
                case "item21": item = eItem.item21; break;
                case "item22": item = eItem.item22; break;
                case "item23": item = eItem.item23; break;
                case "item24": item = eItem.item24; break;
            }
            unlock_item.Add(item);
        }
        #endregion
        #region Character
        for (int i = 0; i < data["unlock_character"].Count; i++)
        {
            eCharacter character = eCharacter.Berserker;
            switch (data["unlock_character"][i].ToString())
            {
                case "Berserker": character = eCharacter.Berserker; break;
                case "Paladin": character = eCharacter.Paladin; break;
                case "Hunter": character = eCharacter.Hunter; break;
                case "Archer": character = eCharacter.Archer; break;
                case "Pirate": character = eCharacter.Pirate; break;
                case "Assassin": character = eCharacter.Assassin; break;
                case "Thief": character = eCharacter.Thief; break;
                case "Saint": character = eCharacter.Saint; break;
                case "Dark_Mage": character = eCharacter.Dark_Mage; break;
                case "Elementalist": character = eCharacter.Elementalist; break;
                case "Mercenary": character = eCharacter.Mercenary; break;
                case "Count": character = eCharacter.Count; break;
            }
            unlock_character.Add(character);
        }
        #endregion
        #region Stage
        for (int i = 0; i < data["unlock_stage"].Count; i++)
        {
            eStage stage = eStage.Cave;
            switch (data["unlock_stage"][i].ToString())
            {
                case "Woods": stage = eStage.Woods; break;
                case "SnowMountain": stage = eStage.SnowMountain; break;
                case "Cave": stage = eStage.Cave; break;
                case "Swamp": stage = eStage.Swamp; break;
                case "Ruins": stage = eStage.Ruins; break;
                case "SulkyWoods": stage = eStage.SulkyWoods; break;
                case "Villa": stage = eStage.Villa; break;
            }
            unlock_stage.Add(stage);
        }
        #endregion
        #region Challenge
        for (int i = 0; i < data["unlock_challenges"].Count; i++)
        {
            eChallenges challenges = eChallenges.Challenge1;
            switch (data["unlock_challenges"][i].ToString())
            {
                case "Challenge1": challenges = eChallenges.Challenge1; break;
                case "Challenge2": challenges = eChallenges.Challenge2; break;
                case "Challenge3": challenges = eChallenges.Challenge3; break;
                case "Challenge4": challenges = eChallenges.Challenge4; break;
                case "Challenge5": challenges = eChallenges.Challenge5; break;
                case "Challenge6": challenges = eChallenges.Challenge6; break;
                case "Challenge7": challenges = eChallenges.Challenge7; break;
                case "Challenge8": challenges = eChallenges.Challenge8; break;
                case "Challenge9": challenges = eChallenges.Challenge9; break;
                case "Challenge10": challenges = eChallenges.Challenge10; break;
                case "Challenge11": challenges = eChallenges.Challenge11; break;
                case "Challenge12": challenges = eChallenges.Challenge12; break;
                case "Challenge13": challenges = eChallenges.Challenge13; break;
                case "Challenge14": challenges = eChallenges.Challenge14; break;
                case "Challenge15": challenges = eChallenges.Challenge15; break;
                case "Challenge16": challenges = eChallenges.Challenge16; break;
                case "Challenge17": challenges = eChallenges.Challenge17; break;
                case "Challenge18": challenges = eChallenges.Challenge18; break;
                case "Challenge19": challenges = eChallenges.Challenge19; break;
                case "Challenge20": challenges = eChallenges.Challenge20; break;
                case "Challenge21": challenges = eChallenges.Challenge21; break;
                case "Challenge22": challenges = eChallenges.Challenge22; break;
                case "Challenge23": challenges = eChallenges.Challenge23; break;
                case "Challenge24": challenges = eChallenges.Challenge24; break;
                case "Challenge25": challenges = eChallenges.Challenge25; break;
                case "Challenge26": challenges = eChallenges.Challenge26; break;
                case "Challenge27": challenges = eChallenges.Challenge27; break;
                case "Challenge28": challenges = eChallenges.Challenge28; break;
                case "Challenge29": challenges = eChallenges.Challenge29; break;
                case "Challenge30": challenges = eChallenges.Challenge30; break;
                case "Challenge31": challenges = eChallenges.Challenge31; break;
                case "Challenge32": challenges = eChallenges.Challenge32; break;
                case "Challenge33": challenges = eChallenges.Challenge33; break;
                case "Challenge34": challenges = eChallenges.Challenge34; break;
                case "Challenge35": challenges = eChallenges.Challenge35; break;
                case "Challenge36": challenges = eChallenges.Challenge36; break;
                case "Challenge37": challenges = eChallenges.Challenge37; break;
                case "Challenge38": challenges = eChallenges.Challenge38; break;
                case "Challenge39": challenges = eChallenges.Challenge39; break;
                case "Challenge40": challenges = eChallenges.Challenge40; break;
            }
            unlock_challenges.Add(challenges);
        }
        #endregion
        #region ClearLog
        for (int i = 0; i < data["clear_log"].Count; i++)
        {
            clear_log[i] = data["clear_log"].ToString();
        }
        #endregion
    }
}
