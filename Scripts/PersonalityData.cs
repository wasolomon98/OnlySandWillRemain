using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Local imports. May introduce separate global class lists later. 
using UG = UnitGlobals;

[System.Serializable]
public class PersonalityData
{
    public UG.Attribute favorable_attribute;
    public UG.Attribute unfavorable_attribute;

    public PersonalityData(UG.Attribute favorable_attribute, UG.Attribute unfavorable_attribute)
    {
        this.favorable_attribute = favorable_attribute;
        this.unfavorable_attribute = unfavorable_attribute;
    }
}
