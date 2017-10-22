using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCountdown : MonoBehaviour {

    public float remainingTime;
	
	// Update is called once per frame
	void Update () {
        remainingTime -= Time.deltaTime;

        if(remainingTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
