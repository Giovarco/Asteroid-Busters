using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfigurationData : ScriptableObject {

    [Tooltip("If it equals zero, then the first level will be one")]
    public int startingLevel;

}
