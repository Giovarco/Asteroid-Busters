using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTrigger : MonoBehaviour {

	void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
