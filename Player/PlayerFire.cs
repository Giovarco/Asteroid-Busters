using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    ProjectileFactory projectileFactory;

    void Awake()
    {
        projectileFactory = GameObject.Find("Factories").GetComponent<ProjectileFactory>();
    }

	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            projectileFactory.instantiate("RedBullet1");
        }
	}
}
