using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructionCountdown : MonoBehaviour {

    public float seconds;
	
	void Update () {
        Destroy(gameObject, seconds);
	}
}
