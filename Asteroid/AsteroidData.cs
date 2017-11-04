using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AsteroidData : ScriptableObject {

    public float baseSpeed;

    [Tooltip("2 = 50%, 4 = 25% etc.")]
    public float sizeReductionFactor;

    [Tooltip("This variable prevents all the asteroid to have all the same speed (not intended to exceed maximum speed)")]
    public float speedVariance;

}
