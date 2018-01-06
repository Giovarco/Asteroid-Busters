using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldControllerData : ScriptableObject {

    public float capacity;
    public float consumption;
    public float rechargeSpeed;
    public float overheatDuration;
    [Tooltip("When should the shield start flashing.")]
    public float stroboscopicSpriteFlashStart;

}
