using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructionOnCollision : MonoBehaviour {

	void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
