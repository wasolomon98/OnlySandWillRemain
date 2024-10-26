// Unit.cs
using UnityEngine;
using UG = UnitGlobals;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Game/Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] UG.Personality personality;

    // Attribute Data
    [System.Serializable]
    public class AttributeStats
    {
        public int level = UG.Defaults.attribute_level;
        public int experience = UG.Defaults.attribute_experience;
        public UG.AttributeRank rank = UG.Defaults.attribute_rank;
        public UG.AttributeTier tier = UG.Defaults.attribute_tier;
    }

    [SerializeField] private AttributeStats[] attributes = new AttributeStats[UG.NUMBER_OF_ATTRIBUTES];

    // Base Stats
    [System.Serializable]
    public class UnitStats
    {
        public int health = UG.Defaults.stat_value;
        public int mana = UG.Defaults.stat_value;
        public int physicalAttack = UG.Defaults.stat_value;
        public int physicalDefense = UG.Defaults.stat_value;
        public int specialAttack = UG.Defaults.stat_value;
        public int specialDefense = UG.Defaults.stat_value;
    }

    [SerializeField] private UnitStats stats = new UnitStats();

    private void OnEnable()
    {
        // Initialize attributes array if empty
        if (attributes == null || attributes.Length != UG.NUMBER_OF_ATTRIBUTES)
        {
            attributes = new AttributeStats[UG.NUMBER_OF_ATTRIBUTES];
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i] = new AttributeStats();
            }
        }
    }

    public UG.Personality GetPersonality() => personality;
    public void SetPersonality(UG.Personality newPersonality) => personality = newPersonality;

    public AttributeStats GetAttribute(UG.Attribute attribute) => attributes[(int)attribute];

    public void SetAttribute(UG.Attribute attribute, int level, int experience)
    {
        var stats = attributes[(int)attribute];
        stats.level = level;
        stats.experience = experience;
        stats.rank = UG.GetAttributeRankForLevel(level);
        stats.tier = (UG.AttributeTier)UG.GetAttributeRankTier(level);
        
        // Recalculate base stats when attributes change
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        // Example stat calculation based on attributes
        stats.health = CalculateHealth();
        stats.mana = CalculateMana();
        stats.physicalAttack = CalculatePhysicalAttack();
        stats.physicalDefense = CalculatePhysicalDefense();
        stats.specialAttack = CalculateSpecialAttack();
        stats.specialDefense = CalculateSpecialDefense();
    }

    // Stat calculation methods
    private int CalculateHealth() =>
        (GetAttribute(UG.Attribute.Toughness).level * 10) + 
        (GetAttribute(UG.Attribute.Spirit).level * 5);

    private int CalculateMana() =>
        (GetAttribute(UG.Attribute.Spirit).level * 10) + 
        (GetAttribute(UG.Attribute.Intelligence).level * 5);

    private int CalculatePhysicalAttack() =>
        (GetAttribute(UG.Attribute.Strength).level * 8) + 
        (GetAttribute(UG.Attribute.Agility).level * 2);

    private int CalculatePhysicalDefense() =>
        (GetAttribute(UG.Attribute.Toughness).level * 7) + 
        (GetAttribute(UG.Attribute.Strength).level * 3);

    private int CalculateSpecialAttack() =>
        (GetAttribute(UG.Attribute.Intelligence).level * 8) + 
        (GetAttribute(UG.Attribute.Spirit).level * 2);

    private int CalculateSpecialDefense() =>
        (GetAttribute(UG.Attribute.Spirit).level * 7) + 
        (GetAttribute(UG.Attribute.Intelligence).level * 3);
}