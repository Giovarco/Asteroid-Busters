using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    Factory factory;

    void Start()
    {
        factory = GameObject.Find("Orchestrator").GetComponent<Factory>();
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            factory.produce("Bullet");
        }
	}
}
