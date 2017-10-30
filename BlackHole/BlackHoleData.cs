using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BlackHoleData : ScriptableObject {

    [Tooltip("How long does the black hole persist (in seconds)?")]
    public int duration;

    [Tooltip("How often does the black hole spawn (in seconds)?")]
    public int spawnFrequency;

}
