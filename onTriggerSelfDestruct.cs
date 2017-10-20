using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerSelfDestruct : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
