using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    [SerializeField]
    Vector3 randomDirection;
    AsteroidInformation asteroidInfo;

    void Awake () {
        asteroidInfo = GetComponent<AsteroidInformation>();
    }
	
    void Start()
    {
        randomDirection = new Vector3(asteroidInfo.xSpeed, asteroidInfo.ySpeed, 0);
    }

    void Update () {
        transform.Translate( randomDirection , Space.World );
	}
}
