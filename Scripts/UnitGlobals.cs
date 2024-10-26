using System;
using System.Collections.Generic;
using UnityEngine;

public static class UnitGlobals
{

    #region Enums
    public enum Personality
    {
        Adventurous, 
        Ambitious, 
        Bold, 
        Boring, 
        Brash, 
        Carefree, 
        Impish, 
        Sassy, 
        Timid
    }  

    public enum Attribute 
    { 
        Strength, 
        Agility, 
        Toughness, 
        Intelligence, 
        Spirit, 
        Luck 
    }
        
    public enum AttributeTier 
    { 
        Novice, 
        Expert, 
        Master 
    };

    public enum AttributeRank
    {
        Untrained = 0,
        Capable = 1,
        Practiced = 2,
        Exceptional = 3,
        Remarkable = 4,
        Masterful = 5,
        Superhuman = 6,
        Supernatural = 7,
        Mythical = 8
    }

    public enum Stat
    {
        Health,
        Mana,
        PhysicalAttack,
        PhysicalDefense,
        SpecialAttack,
        SpecialDefense
    }

    public enum WeaponSkillRank 
    { 
        Apprentice, 
        Initiate, 
        Adept, 
        Specialist, 
        Virtuoso, 
        Elite, 
        Grandmaster, 
        Sovereign, 
        Transcendant 
    }
    #endregion

    #region constants
    private const int BASE_XP_TO_NEXT_LEVEL = 100;
    public const int NUMBER_OF_ATTRIBUTES = 6;
    #endregion

    #region Default Values
    public static class Defaults
    {
        public const GlobalValues.GradedScore attribute_grade = GlobalValues.GradedScore.C;
        public const AttributeTier attribute_tier = AttributeTier.Novice;
        public const AttributeRank attribute_rank = AttributeRank.Untrained;
        public const int attribute_level = 1;
        public const int attribute_experience = 0;
        public const int stat_value = 1;      
    }
    #endregion

    #region Readonly Dictionaries
    private static readonly IReadOnlyDictionary<AttributeRank, (int MinLevel, int MaxLevel)> _attributeRankLevelRanges;

    static UnitGlobals()
    {
        _attributeRankLevelRanges = new Dictionary<AttributeRank, (int MinLevel, int MaxLevel)>
        {
            { AttributeRank.Untrained, (1, 9) },
            { AttributeRank.Capable, (10, 27) },
            { AttributeRank.Practiced, (28, 81) },
            { AttributeRank.Exceptional, (82, 121) },
            { AttributeRank.Remarkable, (122, 181) },
            { AttributeRank.Masterful, (182, 243) },
            { AttributeRank.Superhuman, (244, 363) },
            { AttributeRank.Supernatural, (364, 543) },
            { AttributeRank.Mythical, (544, 729) }
        };
    }

    // Dictionary pairing each Personality with a Favored and Unfavorable attribute pairing PersonalityData
    public static readonly Dictionary<Personality, PersonalityData> personality_data = new Dictionary<Personality, PersonalityData>
    {
        {Personality.Adventurous, new PersonalityData(Attribute.Luck, Attribute.Toughness)},
        {Personality.Ambitious, new PersonalityData(Attribute.Intelligence, Attribute.Luck)},
        {Personality.Bold, new PersonalityData(Attribute.Spirit, Attribute.Luck)},
        {Personality.Boring, new PersonalityData(Attribute.Luck, Attribute.Agility)},
        {Personality.Brash, new PersonalityData(Attribute.Strength, Attribute.Intelligence)},
        {Personality.Carefree, new PersonalityData(Attribute.Toughness, Attribute.Agility)},
        {Personality.Impish, new PersonalityData(Attribute.Intelligence, Attribute.Spirit)},
        {Personality.Sassy, new PersonalityData(Attribute.Agility, Attribute.Intelligence)},
        {Personality.Timid, new PersonalityData(Attribute.Luck, Attribute.Spirit)}
    };
    #endregion

    #region Public Methods
    public static AttributeRank GetAttributeRankForLevel(int level)
    {
        if (level < 1)
            throw new ArgumentException("Level must be greater than 0", nameof(level));

        foreach (var kvp in _attributeRankLevelRanges)
        {
            if (level >= kvp.Value.MinLevel && level <= kvp.Value.MaxLevel)
                return kvp.Key;
        }

        return AttributeRank.Mythical; // Default to highest rank if above all ranges
    }

    public static int GetAttributeRankTier(int level)
    {
        if (level <= 81) return level <= 9 ? 0 : level <= 27 ? 1 : 2;
        if (level <= 243) return level <= 121 ? 0 : level <= 181 ? 1 : 2;
        return level <= 363 ? 0 : level <= 543 ? 1 : 2;
    }

    public static int CalculateExperienceToNextLevel(int currentLevel, int currentExperience)
    {
        if (currentLevel < 1)
            throw new ArgumentException("Current level must be greater than 0", nameof(currentLevel));
        if (currentExperience < 0)
            throw new ArgumentException("Current experience cannot be negative", nameof(currentExperience));

        int currentTier = currentLevel <= 81 ? 1 : currentLevel <= 243 ? 2 : 3;
        int currentRank = GetAttributeRankTier(currentLevel);
        
        return (int)(BASE_XP_TO_NEXT_LEVEL * (currentTier * currentTier) * (1 + currentRank / 3f));
    }

    public static PersonalityData GetPersonalityData(Personality personality)
    {
        return personality_data[personality];
    }

    public static (int MinLevel, int MaxLevel) GetAttributeRankRange(AttributeRank rank)
    {
        return _attributeRankLevelRanges[rank];
    }    
    #endregion

}
