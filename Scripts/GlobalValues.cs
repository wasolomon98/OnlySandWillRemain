using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalValues
{

    // Graded scores are used by Unit Attributes to attain a special multiplier in both combat and during training.
    public enum GradedScore { F, D, C, B, A }
    public static readonly Dictionary<GradedScore, float> score_values = new Dictionary<GradedScore, float>
    {
        {GradedScore.F, 0.5f},
        {GradedScore.D, 0.5f},
        {GradedScore.C, 0.8f},
        {GradedScore.B, 0.9f},
        {GradedScore.A, 1.0f}
    };

    public enum ExperienceType { Attribute, Stat, Weapon }

    public enum Rank { Novice, Rookie, Champion, Ultimate }






    public enum EngagementStyle
    {
        Aggressive,
        Defensive,
        Supportive        
    }


}
