using UnityEngine;
using GV = GlobalValues;
using UG = UnitGlobals;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private Unit defaultUnit;

    public Unit SpawnUnit()
    {
        Unit newUnit = Instantiate(defaultUnit);
        
        // Generate personality
        UG.Personality unitPersonality = GeneratePersonality();
        newUnit.SetPersonality(unitPersonality);

        // Generate attributes
        foreach (UG.Attribute attribute in System.Enum.GetValues(typeof(UG.Attribute)))
        {
            (int level, int experience) = GenerateAttributeValues(unitPersonality, attribute);
            newUnit.SetAttribute(attribute, level, experience);
        }

        return newUnit;
    }

    private UG.Personality GeneratePersonality()
    {
        UG.Personality[] personalities = (UG.Personality[])System.Enum.GetValues(typeof(UG.Personality));
        return personalities[Random.Range(0, personalities.Length)];
    }

    private (int level, int experience) GenerateAttributeValues(UG.Personality personality, UG.Attribute attribute)
    {
        var personalityData = UG.GetPersonalityData(personality);
        int baseLevel;

        // Determine base level based on personality favorability
        if (attribute == personalityData.favorable_attribute)
            baseLevel = Random.Range(5, 28); // Capable to Practiced range
        else if (attribute == personalityData.unfavorable_attribute)
            baseLevel = Random.Range(1, 10); // Untrained range
        else
            baseLevel = Random.Range(1, 15); // Untrained to low Capable range

        // Calculate experience
        int experience = 0;
        if (baseLevel > 1)
        {
            experience = UG.CalculateExperienceToNextLevel(baseLevel - 1, 0);
        }

        return (baseLevel, experience);
    }
}