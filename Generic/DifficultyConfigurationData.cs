using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DifficultyConfigurationData : ScriptableObject {

    [Tooltip("Number of levels before adding one asteroid")]
    public int extraAsteroidFrequency;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    public float hardLevel;

    [Tooltip("Needed to calculate the asteroid increase-in-speed factor")]
    public float hardAsteroidSpeed;

}
