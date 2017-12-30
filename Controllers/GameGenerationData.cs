using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameGenerationLevel : ScriptableObject
{

    [Tooltip("How long does the level text last")]
    public float levelTextDuration;

    [Tooltip("Hong long it is the delay between level text disappearance and the game starting")]
    public float asteroidSpawnDelay;

}
