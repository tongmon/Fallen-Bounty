using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save
{

    public enum eCharacter
    {
        Berserker,
        Paladin,
        Hunter,
        Archer,
        Pirate,
        Assassin,
        Ninja,
        Saint,
        BlackMage,
        Elementor
    }
    public class SaveStat
    {
        public string time = null;
        public int stage = 0;
        public int map =0;
        public int clear_count = 0;
        public List<eCharacter> unlock_character = new List<eCharacter>() {eCharacter.Berserker };
    }
}
