using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpaceDistortionEffectData : ScriptableObject
{

    [Tooltip("How long does the asteroid take to change direction in seconds")]
    public float turningSpeed;

    [Tooltip("How long does the asteroid employ to light up or get back to its normal color")]
    public float flashDuration;

}
